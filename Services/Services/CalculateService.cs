using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.CalculateModel;
using Repositories.ResponseModel.PersonGroupModel;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;
using System.Collections.Generic;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CalculateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<ResponseShortTermModel> CalculateShortTerm(CalculateShortTermModel model)
        {
            List<CalculatingShortTermModel> pair = new();
            List<PersonResponseModel> p = new();
            List<PersonResponseModel> pNeg = new();
            List<PersonResponseModel> pPos = new();

            //add person into p
            foreach (var item in model.Persons)
            {
                p.Add(new PersonResponseModel(item));
            }
            //set pair
            int n = model.Persons.Count;
            SetPair(n, pair, p);
            //verify psub
            foreach (var item in model.CalculatedPersonModels)
            {
                double amountEachPair = 0;
                double amount = 0;
                bool isShared = false;
                List<PersonResponseModel> pSub = new();
                foreach (var item1 in item.SubPersons)
                {
                    PersonResponseModel personFromP = p.Where(p => p.Name == item1).FirstOrDefault()!;
                    foreach (var item2 in item.PersonShortTerms)
                    {
                        if (personFromP!.Name == item2.Name)
                        {
                            amount = item2.Amount;
                            isShared = item2.IsShared;
                        }
                    }
                    PersonResponseModel newPerson = new PersonResponseModel(personFromP.Name, amount);
                    newPerson.Shared = amount;
                    newPerson.IsShared = isShared;
                    pSub.Add(newPerson);
                    amountEachPair += amount;
                }
                int count = 0;
                count = pSub.Where(p => p.IsShared == false).Count();
                foreach (var item3 in pSub)
                {
                    if (item3.IsShared == false)
                    {
                        item3.Diff = item3.Shared;
                    }
                    else
                    {
                        item3.Diff = (-amountEachPair / (item.SubPersons.Count - count) + item3.Shared);
                    }
                    for (int i = 0; i < p.Count; i++)
                    {
                        if (p[i].Name.Equals(item3.Name))
                        {
                            p[i].Diff = p[i].Diff + item3.Diff;
                        }
                    }
                }                
            }
            SetP(n, p, pPos, pNeg);
            CalculateExpense(pPos.Count, pNeg.Count, pPos, pNeg, pair);
            return Result(pair);
        }
        public ResponseLongTermModel CalculateLongTerm(string reportId)
        {
            var report = _unitOfWork.GetRepository<Report>().GetById(reportId);
            var expenseQuery = _unitOfWork.GetRepository<Expense>().Entities.Where(f => f.ReportId == reportId && !f.DeletedTime.HasValue).ToList();
            //var fixedExpense = expenseQuery.Where(f => f.Type!.Equals("0")).ToList();
            //var flexExpense = expenseQuery.Where(f => f.Type!.Equals("1")).ToList();
            //var sharedExpense = expenseQuery.Where(f => f.Type!.Equals("2")).ToList();
            var groupId = report.GroupId;
            var personGroupQuery = _unitOfWork.GetRepository<PersonGroup>()
                                   .Entities.Where(pg => pg.GroupId == groupId)
                                   .Include(pg => pg.Person)
                                   .AsQueryable(); 
            //lay toan bo nguoi trong nhom 
            var groupPersonGroup = personGroupQuery.GroupBy(pg => pg.GroupId).ToList();

            var persons = groupPersonGroup.SelectMany(pg => pg.Select(p => p.Person)).ToList();
            List<ResponseShortTermModel> responseShortTerm = new();
            CalculateShortTermModel input = new();
            input.Persons = persons.Select(p => p.Name).ToList();
            input.CalculatedPersonModels = new();
            /*
            foreach (var fe in fixedExpense)
            {
                //lay ra nhung nguoi share chi tieu fe 
                var personExpenseQuery = _unitOfWork.GetRepository<PersonExpense>()
                                   .Entities.Where(pg => pg.ExpenseId == fe.Id)
                                   .Include(pg => pg.Person)
                                   .AsQueryable();
                var groupPersonExpense = personExpenseQuery.GroupBy(pg => pg.ExpenseId).ToList();

                var pSub = groupPersonExpense.Select(pg => pg.Select(p => p.Person).ToList());


                Console.WriteLine(JsonSerializer.Serialize(pSub));

            }
            foreach (var fle in flexExpense)
            {
                //lay ra nhung nguoi share chi tieu fle

                var personExpenseQuery = _unitOfWork.GetRepository<PersonExpense>()
                                    .Entities.Where(pg => pg.ExpenseId == fle.Id)
                                    .Include(pg => pg.Person)
                                    .AsQueryable();
                var groupPersonExpense = personExpenseQuery.GroupBy(pg => pg.ExpenseId).ToList();

                var pSub = groupPersonExpense.Select(pg => pg.Select(p => p.Person).ToList());
                input.Persons = (List<string>)persons.SelectMany(p => p.Name);
                Console.WriteLine(JsonSerializer.Serialize(input.Persons));
                Console.WriteLine(JsonSerializer.Serialize("------------------------"));
                //Console.WriteLine(JsonSerializer.Serialize(pSub));

            }*/
            List<CalculatedPersonModel> calculatedPersonModels = new();
            foreach (var se in expenseQuery)
            {
                //lay ra nhung nguoi share chi tieu se
                CalculatedPersonModel calculatedPersonModel = new();
                var personExpenseQuery = _unitOfWork.GetRepository<PersonExpense>()
                                   .Entities.Where(pg => pg.ExpenseId == se.Id)
                                   .Include(pg => pg.Person)
                                   .AsQueryable();
                var groupPersonExpense = personExpenseQuery.GroupBy(pg => pg.ExpenseId).ToList();

                List<string>? pSub = groupPersonExpense.SelectMany(pg => pg.Select(p => p.Person.Name)).ToList();
                List<PersonShortTermModel> personShortTermModel = groupPersonExpense.SelectMany(item => item.Select(p =>
                {
                    return new PersonShortTermModel()
                    {
                        Id = p.PersonId,
                        Amount = p.Amount,
                        Name = p.Person.Name
                    };
                })).ToList();
                calculatedPersonModel.SubPersons = pSub;
                calculatedPersonModel.PersonShortTerms = personShortTermModel;
                calculatedPersonModels.Add(calculatedPersonModel);
            }
            input.CalculatedPersonModels = calculatedPersonModels;
            
            //Console.WriteLine(JsonSerializer.Serialize(persons));
            return new ResponseLongTermModel()
            {
                ResponseShortTerm = CalculateShortTerm(input)
            };
        }
        public void SetPair(int n, List<CalculatingShortTermModel> pair, List<PersonResponseModel> p)
        {
            pair.EnsureCapacity(n * (n - 1) / 2);
            int a = 0;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    // Add new elements to the list if necessary
                    if (a >= pair.Count)
                    {
                        pair.Add(new CalculatingShortTermModel(p[i], p[j], 0.0));
                    }
                    else
                    {
                        pair[a].Person1 = p[i];
                        pair[a].Person2 = p[j];
                    }
                    a++;
                }
            }
        }
        public void SetP(int nSub, List<PersonResponseModel> pSub, List<PersonResponseModel> pPosSub, List<PersonResponseModel> pNegSub)
        {//chia 2 mang de tinh toan
            int n1 = 0;
            int n2 = 0;
            for (int i = 0; i < nSub; i++)
            {
                if (pSub[i].Diff > 0)
                {
                    pPosSub.Add(pSub[i]); //pPos bao gom nhung thanh vien duoc nhan tien
                    n1++;
                }
                else if (pSub[i].Diff < 0)
                {
                    pNegSub.Add(pSub[i]);//pNeg bao gom nhung thanh vien phai tra tien
                    n2++;
                }
            }
            while (n1 != n2)
            {//truong hop 2 mang chenh lech do so nguoi le
                if (n1 > n2)
                {
                    pNegSub.Add(new PersonResponseModel("newname", 0, false, false, 0, 0, 0, 0));
                    n2++;
                }
                else if (n1 < n2)
                {
                    pPosSub.Add(new PersonResponseModel("newname", 0, false, false, 0, 0, 0, 0));
                    n1++;
                }
            }
        }
        public void CalculateExpense(int n1, int n2, List<PersonResponseModel> pPosSub, List<PersonResponseModel> pNegSub, List<CalculatingShortTermModel> pairSub)
        {//tinh tien se cho ra ket qua no giua tat ca cac cap bao gom: tien cung, tien mem, tien shared va tien no
            for (int j = 0; j < n2; j++)
            {
                for (int i = 0; i < n1; i++)
                {
                    if (pPosSub[j].Diff <= Math.Abs(pNegSub[i].Diff))
                    {//check neu 1 trong 2 nguoi trong cap dang xet co so tien check lech = 0 se skip
                        if (pPosSub[j].Diff == 0 || pNegSub[i].Diff == 0)
                        {
                            continue;
                        }
                        for (int k = 0; k < pairSub.Count; k++)
                        {
                            if (pNegSub[i].Equals(pairSub[k].Person1))
                            {//chieu thuan
                                if (pPosSub[j].Equals(pairSub[k].Person2))
                                {//check cap dang xet = cap trong mang pair
                                    pairSub[k].Debt = pPosSub[j].Diff + pairSub[k].Debt;//set tien no + them tien check lech 
                                    break;
                                }
                            }
                            else if (pPosSub[j].Equals(pairSub[k].Person1))
                            {//chieu nguoc 
                                if (pNegSub[i].Equals(pairSub[k].Person2))
                                {//check cap dang xet = cap trong mang pair
                                    pairSub[k].Debt = -pPosSub[j].Diff + pairSub[k].Debt;//set tien no + them tien check lech
                                    break;
                                }
                            }
                        }
                        pNegSub[i].Diff = pNegSub[i].Diff + pPosSub[j].Diff;
                        pPosSub[j].Diff = 0;
                    }
                    else
                    {
                        if (pPosSub[j].Diff == 0 || pNegSub[i].Diff == 0)
                        {
                            continue;
                        }
                        for (int k = 0; k < pairSub.Count; k++)
                        {
                            if (pNegSub[i].Equals(pairSub[k].Person1))
                            {//chieu thuan 
                                if (pPosSub[j].Equals(pairSub[k].Person2))
                                {
                                    pairSub[k].Debt = Math.Abs(pNegSub[i].Diff + pairSub[k].Debt);
                                    break;
                                }
                            }
                            else if (pPosSub[j].Equals(pairSub[k].Person1))
                            {//chieu nguoc
                                if (pNegSub[i].Equals(pairSub[k].Person2))
                                {
                                    pairSub[k].Debt = pNegSub[i].Diff + pairSub[k].Debt;
                                    break;
                                }
                            }
                        }
                        pPosSub[j].Diff = pNegSub[i].Diff + pPosSub[j].Diff;
                        pNegSub[i].Diff = 0;
                    }
                }
            }
        }
        public List<ResponseShortTermModel> Result(List<CalculatingShortTermModel> pairSub)
        {
            List<ResponseShortTermModel> response = new();
            for (int i = 0; i < pairSub.Count; i++)
            {
                if (pairSub[i].Debt > 0)
                {
                    response.Add(new ResponseShortTermModel(pairSub[i].Person1.Name, pairSub[i].Person2.Name, Math.Round(pairSub[i].Debt, 2)));
                    //Console.WriteLine($"{pairSub[i].Person1.Name} will pay {pairSub[i].Person2.Name}: {pairSub[i].Debt}k VND");
                }
                else if (pairSub[i].Debt < 0)
                {
                    response.Add(new ResponseShortTermModel(pairSub[i].Person2.Name, pairSub[i].Person1.Name, Math.Round(Math.Abs(pairSub[i].Debt), 2)));
                    //Console.WriteLine($"{pairSub[i].Person2.Name} will pay {pairSub[i].Person1.Name}: {Math.Abs(pairSub[i].Debt)}k VND");
                }
            }
            return response;
        }
    }
}

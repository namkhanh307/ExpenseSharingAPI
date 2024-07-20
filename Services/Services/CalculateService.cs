using Repositories.Entities;
using Repositories.ResponseModel.CalculateModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CalculateService : ICalculateService
    {
        public List<ResponseShortTermModel>? CalculateShortTerm(CalculateShortTermModel model)
        {
            List<ResponseShortTermModel> pair = new();
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
            pair.EnsureCapacity(n * (n - 1) / 2);  
            int a = 0;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    // Add new elements to the list if necessary
                    if (a >= pair.Count)
                    {
                        pair.Add(new ResponseShortTermModel(p[i], p[j], 0.0));
                    }
                    else
                    {
                        pair[a].PersonPay = p[i];
                        pair[a].PersonReceive = p[j];
                    }
                    a++;
                }
            }
            //verify psub
            foreach (var item in model.CalculatedPersonModels)
            {
                double amountEachPair = 0;
                double amount = 0;
                List<PersonResponseModel> pSub = new();
                foreach (var item1 in item.SubPersons)
                {
                    PersonResponseModel personFromP = p.Where(p => p.Name == item1).FirstOrDefault();
                    foreach (var item2 in item.PersonShortTerms)
                    {
                        if(personFromP.Name == item2.Name)
                        {
                            amount = item2.Amount;
                        }
                    }
                    PersonResponseModel newPerson = new PersonResponseModel(personFromP.Name, amount);
                    newPerson.Shared = amount;
                    pSub.Add(newPerson);
                    amountEachPair += amount;
                }
                foreach (var item3 in pSub)
                {
                    item3.Diff = (-amountEachPair / item.SubPersons.Count + item3.Flex);
                }
                Console.WriteLine(JsonSerializer.Serialize(pSub));
                Console.WriteLine("Next");
            }
            Console.WriteLine("p here");
            Console.WriteLine(JsonSerializer.Serialize(p));

            Console.WriteLine("pair here");
            Console.WriteLine(JsonSerializer.Serialize(pair));

            return null;
        }
    }
}

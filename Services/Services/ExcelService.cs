using Services.IServices;
using ClosedXML.Excel;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repositories.IRepositories;
using Repositories.ResponseModel.CalculateModel;
using Repositories.ResponseModel.PersonExpenseModel;


namespace Services.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICalculateService _calculateService;
        private readonly IPersonExpenseService _personExpenseService;

        public ExcelService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICalculateService calculateService, IPersonExpenseService personExpenseService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _calculateService = calculateService;
            _personExpenseService = personExpenseService;
        }

        public async Task<byte[]> LongTermExcel(string reportId)
        {
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("LongTerm");
                GetPersonExpenseModel result = await _personExpenseService.GetPersonExpenses(reportId, null);
                worksheet.Cell("A1").Value = result.ReportName;
                worksheet.Column("A").Width = 20;
                worksheet.Cell("A3").Value = "ExpenseName";
                worksheet.Cell("B3").Value = "Amount";
                worksheet.Cell("C3").Value = "Paid By";
                worksheet.Cell("D3").Value = "Created Date";
                int currentRow = 4;
                string currentColumn = "E";
                int row = 3;
                //if (string.IsNullOrEmpty(worksheet.Cell("E3").GetValue<string>()))
                //{
                foreach (var item in result.Persons!)//in ra tat ca cac thanh vien trong nhom
                {
                    worksheet.Cell($"{currentColumn}{row}").Value = item.Name;//E3
                    currentColumn = GetNextColumn(currentColumn);
                }
                foreach (var item in result.PersonSubs!)//lap qua tung chi tieu
                {
                    worksheet.Cell($"A{currentRow}").Value = item.ExpenseName;
                    worksheet.Cell($"B{currentRow}").Value = item.ExpenseAmount;
                    string paidByNames = string.Join(", ", item.ExpenesePaidBy!.Select(name => name.Trim()));
                    worksheet.Cell($"C{currentRow}").Value = paidByNames;
                    worksheet.Cell($"D{currentRow}").Value = item.ExpenseCreatedTime;
                    currentColumn = "E";
                    double amount = item.ExpenseAmount!.Value;//so tien cua chi tieu 
                    int countIsShared = item.PersonExpenseSub!.Where(i => i.IsShared == true).Count(); //lay ra so thanh vien se chia
                    foreach (var item1 in item.PersonExpenseSub!)//lap qua tung thanh vien trong chi tieu nay
                    {
                        if(item1.Amount > 0 && item1.IsShared == true)//neu nguoi nay tra va co chia
                        {
                            worksheet.Cell($"{currentColumn}{currentRow}").Value = Math.Round(amount - (amount / countIsShared), 2);
                        } else if(item1.Amount > 0 && item1.IsShared == false)//neu nguoi nay tra va khong chia
                        {
                            worksheet.Cell($"{currentColumn}{currentRow}").Value = item1.Amount;
                        }
                        else if (item1.Amount == 0 && item1.IsShared == true)//neu nguoi nay khong tra va co chia
                        {
                            worksheet.Cell($"{currentColumn}{currentRow}").Value = Math.Round(-amount / countIsShared, 2);
                        }
                        else
                        {
                            worksheet.Cell($"{currentColumn}{currentRow}").Value = "";
                        }
                        currentColumn = GetNextColumn(currentColumn);                          
                    }
                    currentRow++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }

            }
        }
        string GetNextColumn(string column)
        {
            char lastChar = column[column.Length - 1];
            string nextColumn;

            if (lastChar == 'Z')
            {
                // If last character is Z, append an A (going to the next letter combination)
                nextColumn = column + "A";
            }
            else
            {
                // Increment the last character
                nextColumn = column.Substring(0, column.Length - 1) + (char)(lastChar + 1);
            }

            return nextColumn;
        }
    }
}
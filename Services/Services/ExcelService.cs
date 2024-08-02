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
                GetPersonExpenseModel result = _personExpenseService.GetPersonExpenses(reportId, null);
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
                foreach (var item in result.Persons)
                {
                    worksheet.Cell($"{currentColumn}{row}").Value = item.Name;//E3
                    currentColumn = GetNextColumn(currentColumn);
                }
                foreach (var item in result.PersonSubs)
                {
                    worksheet.Cell($"A{currentRow}").Value = item.ExpenseName;
                    worksheet.Cell($"B{currentRow}").Value = item.ExpenseAmount;
                    worksheet.Cell($"C{currentRow}").Value = "NOT HANDLING!";
                    worksheet.Cell($"D{currentRow}").Value = item.ExpenseCreatedTime;             
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
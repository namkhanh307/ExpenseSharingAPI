﻿using Services.IServices;
using ClosedXML.Excel;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repositories.IRepositories;
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
                worksheet.Range("A1:B1").Merge();
                worksheet.Range("A1:B1").Style.Font.FontSize = 12; // Tăng kích thước chữ cho tiêu đề
                worksheet.Column("A").Width = 20;
                worksheet.Cell("A3").Value = "ExpenseName";
                worksheet.Cell("B3").Value = "Amount";
                worksheet.Cell("C3").Value = "Paid By";
                worksheet.Cell("D3").Value = "Created Date";
                var dataRangeTitle = worksheet.Range("A1:R28");
                dataRangeTitle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                dataRangeTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                int currentRow = 4;
                string currentColumn = "E";
                int row = 3;
                //if (string.IsNullOrEmpty(worksheet.Cell("E3").GetValue<string>()))
                //{
                worksheet.Range("A3:D3").Style.Fill.BackgroundColor = XLColor.LightGray;
                foreach (var item in result.Persons!)//in ra tat ca cac thanh vien trong nhom
                {
                    worksheet.Cell($"{currentColumn}{row}").Style.Fill.BackgroundColor = XLColor.LightGray;
                    worksheet.Cell($"{currentColumn}{row}").Value = item.Name;//E3
                    worksheet.Column($"{currentColumn}").Width = 11;
                    currentColumn = GetNextColumn(currentColumn);
                }
                foreach (var item in result.PersonSubs!)//lap qua tung chi tieu
                {
                    worksheet.Cell($"A{currentRow}").Value = item.ExpenseName;
                    worksheet.Cell($"B{currentRow}").Value = item.ExpenseAmount;
                    string paidByNames = string.Join(", ", item.ExpenesePaidBy!.Select(name => name.Trim()));
                    worksheet.Cell($"C{currentRow}").Value = paidByNames;
                    worksheet.Cell($"C{currentRow}").Style.Alignment.WrapText = true;
                    worksheet.Cell($"D{currentRow}").Value = item.ExpenseCreatedTime;
                    var dataRange = worksheet.Range($"A{currentRow}:D{currentRow}");
                    dataRangeTitle.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    dataRangeTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    currentColumn = "E";
                    double amount = item.ExpenseAmount!.Value;//so tien cua chi tieu 
                    int countIsShared = item.PersonExpenseSub!.Where(i => i.IsShared == true).Count(); //lay ra so thanh vien se chia
                    foreach (var item1 in item.PersonExpenseSub!)//lap qua tung thanh vien trong chi tieu nay
                    {
                        if (item1.Amount > 0 && item1.IsShared == true)//neu nguoi nay tra va co chia
                        {
                            worksheet.Cell($"{currentColumn}{currentRow}").Value = Math.Round(item1.Amount  - (amount / countIsShared), 2);
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
                string sumRow = (currentRow + 1).ToString();
                int startRow = 3; // Starting row for your data
                currentColumn = "E";

                while (true) // Loop through columns until an empty column is encountered
                {
                    string columnLetter = currentColumn;
                    if (worksheet.Cell($"{columnLetter}{startRow}").IsEmpty())
                    {
                        break;
                    }

                    string range = $"{columnLetter}{startRow+1}:{columnLetter}{currentRow - 1}";
                    worksheet.Cell($"{columnLetter}{sumRow}").FormulaA1 = $"SUM({range})";
                    currentColumn = GetNextColumn(currentColumn);
                }
                worksheet.Cell($"D{sumRow}").Value = "Total";
                worksheet.Column("D").Width = 15;
                worksheet.Column("C").Width = 15;
                worksheet.Cell($"D{sumRow}").Style.Fill.BackgroundColor = XLColor.LightGray;
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
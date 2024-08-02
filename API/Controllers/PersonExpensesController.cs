﻿using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Repositories.ResponseModel.GroupModel;
using Repositories.ResponseModel.PersonExpenseModel;
using Services.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonExpensesController : ControllerBase
    {
        private readonly IPersonExpenseService _personExpenseService;
        public PersonExpensesController(IPersonExpenseService personExpenseService)
        {
            _personExpenseService = personExpenseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonExpenses(string? reportId, string? expenseId)
        {
            var result = await _personExpenseService.GetPersonExpenses(reportId, expenseId);
            return Ok(new BaseResponseModel<GetPersonExpenseModel>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: result));
        }
        [HttpPost("forApp")]
        public async Task<IActionResult> PostPersonExpense(PostPersonExpenseModel model)
        {
            await _personExpenseService.PostPersonExpense(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Tao chi tieu cho thanh vien thanh cong"));
        }
        [HttpPost("forDeveloping")]
        public async Task<IActionResult> PostPersonExpenseForDeveloping(PostPersonExpenseForDevModel model)
        {
            await _personExpenseService.PostPersonExpenseForDeveloping(model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Tao chi tieu cho thanh vien thanh cong"));
        }
        [HttpPut]
        public async Task<IActionResult> PutPersonExpense(string expenseId, PutPersonExpenseModel model)
        {
            await _personExpenseService.PutPersonExpense(expenseId, model);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Chinh sua chi tieu cho thanh vien thanh cong"));
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePersonExpense(string expenseId, string personId)
        {
            _personExpenseService.DeletePersonExpense(expenseId, personId);
            return Ok(new BaseResponseModel<string>(
               statusCode: StatusCodes.Status200OK,
               code: ResponseCodeConstants.SUCCESS,
               data: "Xoa chi tieu cho thanh vien thanh cong"));
        }
    }
}

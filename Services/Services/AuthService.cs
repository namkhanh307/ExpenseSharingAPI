using AutoMapper;
using Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.ResponseModel.AuthModel;
using Repositories.ResponseModel.PersonModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AuthService(IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService) : IAuthService
    {
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<GetPersonModel> GetInfo()
        {
            var idUser = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            return _mapper.Map<GetPersonModel>(await _unitOfWork.GetRepository<Person>().GetByIdAsync(idUser));
        }

        public async Task<GetLogInModel> LogIn(PostLogInModel request)
        {
            Person? person = _unitOfWork.GetRepository<Person>().Entities.FirstOrDefault(p => p.Phone == request.Phone);
            if (person == null) 
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ErrorCode.UnAuthorized, "You don't have an account, please sign up!");
            }
            if (person.Password != request.Password)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Phone number or password are incorrect!");
            }
            var token = _tokenService.GenerateTokens(person);
            return new GetLogInModel()
            {
                Person = _mapper.Map<GetPersonModel>(person),
                Token = token
            };
        }

        public async Task SignUp(PostSignUpModel model)
        {
            Person? person = _unitOfWork.GetRepository<Person>().Entities.FirstOrDefault(p => p.Phone == model.Phone);
            if (person != null)
            {
                throw new ErrorException(StatusCodes.Status409Conflict, ErrorCode.Conflicted, "Số tài khoản này đã được đăng ký!");
            }
            if(model.Password != model.ConfirmedPassword)
            {
                throw new ErrorException(StatusCodes.Status409Conflict, ErrorCode.Conflicted, "");
            }
            var newPerson = new Person()
            {
                Name = model.Name,
                Phone = model.Phone,
                Password = model.Password,
                CreatedTime =  DateTime.Now
            };
            await _unitOfWork.GetRepository<Person>().InsertAsync(newPerson);
            await _unitOfWork.SaveAsync();
        }
    }
}

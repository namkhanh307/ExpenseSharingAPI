﻿using AutoMapper;
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
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AuthService(IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;

        }
        public GetPersonModel GetInfo()
        {
            var idUser = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            Guid.TryParse(idUser, out var id);
            return _mapper.Map<GetPersonModel>(_unitOfWork.GetRepository<Person>().GetById(id));
        }

        public GetLogInModel LogIn(PostLogInModel request)
        {
            var person = _unitOfWork.GetRepository<Person>().Entities.FirstOrDefault(p => p.Phone == request.Phone);
            if (person == null) 
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ErrorCode.UnAuthorized, "Bạn chưa có tài khoản, hãy đăng ký ngay!");
            }
            if (person.Password != request.Password)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Số điện thoại hoặc mật khẩu không đúng!");
            }
            var token = _tokenService.GenerateTokens(person);
            return new GetLogInModel()
            {
                Person = _mapper.Map<GetPersonModel>(person),
                Token = token
            };
        }

        public void SignUp(PostSignUpModel model)
        {
            var person = _unitOfWork.GetRepository<Person>().Entities.FirstOrDefault(p => p.Phone == model.Phone);
            if (person != null)
            {
                throw new ErrorException(StatusCodes.Status409Conflict, ErrorCode.Conflicted, "Số tài khoản này đã được đăng ký!");
            }
            if(model.Password != model.ConfirmedPassword)
            {
                throw new ErrorException(StatusCodes.Status409Conflict, ErrorCode.Conflicted, "");
            }         
            _unitOfWork.GetRepository<Person>().Insert(_mapper.Map<Person>(model));
            _unitOfWork.Save();
        }
    }
}

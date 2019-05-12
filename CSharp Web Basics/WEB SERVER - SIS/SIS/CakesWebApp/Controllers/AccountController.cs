﻿using CakesWebApp.Models;
using CakesWebApp.Services;
using CakesWebApp.Services.Contracts;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System;
using System.Linq;

namespace CakesWebApp.Controllers
{
    public class AccountController : BaseController
    {
        private IHashService hashService;
        private IUserCookieService userCookieService;

        public AccountController()
        {
            this.hashService = new HashService();
            this.userCookieService = new UserCookieService();
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View("Register");
        }

        public IHttpResponse DoRegister(IHttpRequest request)
        {
            //catch input data
            var userName = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmPassword"].ToString();

            //validate
            if (string.IsNullOrEmpty(userName) || userName.Length < 4)
            {
                return this.BadRequestError("Please provide valid username with length of 4 or more characters");
            }

            if (this.CakesDb.Users.Any(x => x.Username == userName))
            {
                return this.BadRequestError("a user with the same name already exists");
            }

            if (string.IsNullOrEmpty(password) || password.Length < 5)
            {
                return this.BadRequestError("password must be more than 5 characters");
            }

            if (password != confirmPassword)
            {
                return this.BadRequestError("passwords don't match");
            }

            //hash password
            var hashedPass = this.hashService.Hash(password);

            //create user
            var user = new User
            {
                Name = userName,
                Username = userName,
                Password = hashedPass
            };

            this.CakesDb.Users.Add(user);

            try
            {
                this.CakesDb.SaveChanges();
            }
            catch (Exception e)
            {
                //TODO: Log error
                return this.ServereError(e.Message);
            }

            //TODO: login

            //Redirect
            return new RedirectResult("/");
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View("Login");
        }

        public IHttpResponse DoLogin(IHttpRequest request)
        {
            var userName = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();

            var hashedPass = this.hashService.Hash(password);

            var user = this.CakesDb.Users
                .FirstOrDefault(x => x.Username == userName && x.Password == hashedPass);

            if (user == null)
            {
                return this.BadRequestError("Invalid username or password!");
            }

            var cookieContent = this.userCookieService.GetUserCookie(user.Username);

            var response = new RedirectResult("/");
            response.Cookies.Add(new HttpCookie(".auth-cakes", cookieContent, 7));
            return response;
        }
    }
}

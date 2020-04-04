using System;
using System.Collections.Generic;
using System.Net;
using WebApi.Api.User.response;
using WebApi.Api.Users.request;
using WebApi.Api.Users.response;

namespace WebApi.Api.Users
{
    class ReadUsersController : AbstractController<ReadUsersRequest, ReadUsersResponse>
    {
        public ReadUsersController(HttpListenerRequest req, HttpListenerResponse res, string reqBody) : base(req, res, reqBody)
        {
        }

        protected override void CreateResponse()
        {
            this.resObj.Count = 2;
            this.resObj.List = new List<ReadUserResponse>
            {
                new ReadUserResponse
                {
                    UserId = "1",
                    UserName = "田中太郎",
                    Age = 25,
                    Gender = 0
                },
                new ReadUserResponse
                {
                    UserId = "2",
                    UserName = "田中花子",
                    Age = 18,
                    Gender = 1
                }
            };
        }

        protected override void ExecuteCore()
        {
            log.Debug("UserName=" + this.reqObj.UserName);
            log.Debug("CreateDate=" + this.reqObj.CreateDate);
        }
    }
}

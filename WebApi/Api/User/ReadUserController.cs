using System;
using System.Net;
using WebApi.Api.User.request;
using WebApi.Api.User.response;

namespace WebApi.Api.User
{
    class ReadUserController : AbstractController<ReadUserRequest, ReadUserResponse>
    {
        public ReadUserController(HttpListenerRequest req, HttpListenerResponse res, string reqBody) : base(req, res, reqBody)
        {
        }

        protected override void CreateResponse()
        {
            this.resObj.UserId = "1";
            this.resObj.UserName = "田中太郎";
            this.resObj.Age = 25;
            this.resObj.Gender = 0;
        }

        protected override void ExecuteCore()
        {
            log.Debug("UserId=" + this.reqObj.UserId);
        }
    }
}

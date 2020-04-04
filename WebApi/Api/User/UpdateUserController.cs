using System;
using System.Net;
using WebApi.Api.User.request;
using WebApi.Api.User.response;

namespace WebApi.Api.User
{
    class UpdateUserController : AbstractController<UpdateUserRequest, UpdateUserResponse>
    {
        public UpdateUserController(HttpListenerRequest req, HttpListenerResponse res, string reqBody) : base(req, res, reqBody)
        {
        }

        protected override void CreateResponse()
        {
            this.resObj.UserId = "1";
        }

        protected override void ExecuteCore()
        {
            log.Debug("UserId=" + this.reqObj.UserId);
        }
    }
}

using System.Net;
using WebApi.Api.User.request;
using WebApi.Api.User.response;

namespace WebApi.Api.User
{
    class CreateUserController : AbstractController<CreateUserRequest, CreateUserResponse>
    {
        public CreateUserController(HttpListenerRequest req, HttpListenerResponse res, string reqBody) : base(req, res, reqBody)
        {
        }

        protected override void CreateResponse()
        {
            this.resObj.UserId = "1";
        }

        protected override void ExecuteCore()
        {
            log.Debug("UserName=" + this.reqObj.UserName);
            log.Debug("Age=" + this.reqObj.Age);
            log.Debug("Gender=" + this.reqObj.Gender);
        }
    }
}

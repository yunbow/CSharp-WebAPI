using System.Net;
using WebApi.Api.User.request;
using WebApi.Api.User.response;

namespace WebApi.Api.User
{
    class DeleteUserController : AbstractController<DeleteUserRequest, DeleteUserResponse>
    {
        public DeleteUserController(HttpListenerRequest req, HttpListenerResponse res, string reqBody) : base(req, res, reqBody)
        {
        }

        protected override void CreateResponse()
        {
        }

        protected override void ExecuteCore()
        {
            log.Debug("UserId=" + this.reqObj.UserId);
        }
    }
}

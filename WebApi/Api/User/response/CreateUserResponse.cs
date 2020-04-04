using Newtonsoft.Json;
using WebApi.Common;

namespace WebApi.Api.User.response
{
    /// <summary>
    /// USER生成のレスポンス
    /// </summary>
    [JsonObject]
    class CreateUserResponse : Response
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}

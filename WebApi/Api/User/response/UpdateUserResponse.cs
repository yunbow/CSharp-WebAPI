using Newtonsoft.Json;
using WebApi.Common;

namespace WebApi.Api.User.response
{
    /// <summary>
    /// USER更新のレスポンス
    /// </summary>
    [JsonObject]
    class UpdateUserResponse : Response
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}

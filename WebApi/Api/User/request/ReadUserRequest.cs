using Newtonsoft.Json;
using WebApi.Validator;

namespace WebApi.Api.User.request
{
    /// <summary>
    /// USER参照のリクエスト
    /// </summary>
    [JsonObject]
    class ReadUserRequest : Request
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Required]
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}

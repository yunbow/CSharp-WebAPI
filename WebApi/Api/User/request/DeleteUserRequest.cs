using Newtonsoft.Json;
using WebApi.Validator;

namespace WebApi.Api.User.request
{
    /// <summary>
    /// USER削除のリクエスト
    /// </summary>
    [JsonObject]
    class DeleteUserRequest : Request
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Required]
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}

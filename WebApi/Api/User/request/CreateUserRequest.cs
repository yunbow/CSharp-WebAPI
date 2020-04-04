using Newtonsoft.Json;
using WebApi.Validator;

namespace WebApi.Api.User.request
{
    /// <summary>
    /// USER生成のリクエスト
    /// </summary>
    [JsonObject]
    class CreateUserRequest : Request
    {
        /// <summary>
        /// ユーザー名
        /// </summary>
        [Required]
        [StringLength(max = 30)]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        [Required]
        [ClassValue(new int[] { 0, 1 })]
        [JsonProperty("gender")]
        public int Gender { get; set; }

        /// <summary>
        /// 年齢
        /// </summary>
        [Required]
        [NumberRange(from = 0, to = 120)]
        [JsonProperty("age")]
        public int Age { get; set; }
    }
}

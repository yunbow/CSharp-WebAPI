using Newtonsoft.Json;
using WebApi.Common;

namespace WebApi.Api.User.response
{
    /// <summary>
    /// USER参照のレスポンス
    /// </summary>
    [JsonObject]
    class ReadUserResponse : Response
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        [JsonProperty("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        [JsonProperty("gender")]
        public int Gender { get; set; }

        /// <summary>
        /// 年齢
        /// </summary>
        [JsonProperty("age")]
        public int Age { get; set; }
    }
}

using Newtonsoft.Json;
using WebApi.Validator;

namespace WebApi.Api.Users.request
{
    /// <summary>
    /// USERS参照のリクエスト
    /// </summary>
    class ReadUsersRequest : Request
    {
        /// <summary>
        /// ユーザー名
        /// </summary>
        [JsonProperty("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        [StringDateFormat("yyyyMMdd")]
        [JsonProperty("createDate")]
        public string CreateDate { get; set; }
    }
}

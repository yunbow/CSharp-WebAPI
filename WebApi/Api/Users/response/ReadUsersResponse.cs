using Newtonsoft.Json;
using System.Collections.Generic;
using WebApi.Api.User.response;
using WebApi.Common;

namespace WebApi.Api.Users.response
{
    class ReadUsersResponse : Response
    {
        /// <summary>
        /// 一覧
        /// </summary>
        [JsonProperty("list")]
        public List<ReadUserResponse> List { get; set; }

        /// <summary>
        /// 件数
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}

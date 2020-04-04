using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApi.Common
{
    /// <summary>
    /// レスポンスパラメータ共通
    /// </summary>
    [JsonObject]
    class Response
    {
        /// <summary>
        /// 結果種別
        /// </summary>
        [JsonProperty("resultType")]
        public int ResultType { get; set; }

        /// <summary>
        /// 結果詳細リスト
        /// </summary>
        [JsonProperty("resultDetalList")]
        public List<ResultDetail> ResultDetailList { get; set; }
    }

    /// <summary>
    /// レスポンス詳細
    /// </summary>
    [JsonObject]
    class ResultDetail
    {
        /// <summary>
        /// コード
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// メッセージ
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

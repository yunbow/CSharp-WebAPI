using Newtonsoft.Json;
using System.Linq;
using System.Net;
using WebApi.Common;
using WebApi.Validator;

namespace WebApi.Api
{
    /// <summary>
    /// コントローラー共通
    /// </summary>
    abstract class AbstractController<TReq, TRes> where TReq : Request where TRes : Response, new()
    {
        protected static Logger log = Logger.GetInstance();
        protected HttpListenerRequest req;
        protected HttpListenerResponse res;
        protected TReq reqObj;
        protected TRes resObj;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="req">リクエスト情報</param>
        /// <param name="res">レスポンス情報</param>
        /// <param name="reqBody">リクエストボディ</param>
        public AbstractController(HttpListenerRequest req, HttpListenerResponse res, string reqBody)
        {
            this.req = req;
            this.res = res;

            if ("GET".Equals(req.HttpMethod))
            {
                if (0 < req.QueryString.Count)
                {
                    // クエリパラメータ -> JSONオブジェクト
                    string json = JsonConvert.SerializeObject(req.QueryString.Cast<string>().ToDictionary(k => k, v => req.QueryString[v]));
                    this.reqObj = JsonConvert.DeserializeObject<TReq>(json, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Populate });
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(reqBody))
                {
                    // JSON文字列 -> JSONオブジェクト
                    this.reqObj = JsonConvert.DeserializeObject<TReq>(reqBody, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Populate });
                }
            }

            // リクエストパラメータのバリデーション
            log.Debug("validate --> Start");
            ValidatorMapper validator = new ValidatorMapper(this.reqObj);
            validator.Execute();
            log.Debug("validate <-- End");
        }

        /// <summary>
        /// 実行する
        /// </summary>
        /// <returns>レスポンス文字列</returns>
        public string Execute()
        {
            // 本処理を実行する
            ExecuteCore();

            // レスポンスを生成する
            this.resObj = new TRes
            {
                ResultType = (int)ResultType.SUCCESS
            };

            CreateResponse();

            // JSONオブジェクト -> JSON文字列
            return JsonConvert.SerializeObject(this.resObj, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        /// <summary>
        /// 本処理を実行する
        /// </summary>
        protected abstract void ExecuteCore();

        /// <summary>
        /// レスポンスを生成する
        /// </summary>
        protected abstract void CreateResponse();
    }
}

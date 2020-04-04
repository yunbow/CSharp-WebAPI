using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WebApi.Api.User;
using WebApi.Api.Users;
using WebApi.Common;
using WebApi.Properties;

namespace WebApi.Api
{
    class ControllerMapper
    {
        private const string CONTENT_TYPE_JSON = "application/json";
        private const string KEY_RESPONSE = "Response";

        private static Logger log = Logger.GetInstance();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControllerMapper()
        {
        }

        /// <summary>
        /// 実行する
        /// </summary>
        /// <param name="req">リクエスト情報</param>
        /// <param name="res">レスポンス情報</param>
        public void Execute(HttpListenerRequest req, HttpListenerResponse res)
        {
            StreamReader reader = null;
            StreamWriter writer = null;
            string reqBody = null;
            string resBoby = null;

            try
            {
                res.StatusCode = (int)HttpStatusCode.OK;
                res.ContentType = CONTENT_TYPE_JSON;
                res.ContentEncoding = Encoding.UTF8;

                reader = new StreamReader(req.InputStream);
                writer = new StreamWriter(res.OutputStream);
                reqBody = reader.ReadToEnd();

                LogStart(req, reqBody);
                resBoby = ExecuteController(req, res, reqBody);
            }
            catch (ApiException ex)
            {
                // APIエラー
                resBoby = CreateApiErrorResponse(ex);
            }
            catch (JsonReaderException ex)
            {
                // JSON構文エラー
                resBoby = CreateErrorResponse(ErrorCode.ERROR_JSON_SYNTAX, String.Format(Resources.ErrorJsonSyntax, ex.Message));
                res.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                // システムエラー
                resBoby = CreateErrorResponse(ErrorCode.SYSTEM_ERROR, String.Format(Resources.ErrorUnexpected, ex.Message));
                res.StatusCode = (int)HttpStatusCode.InternalServerError;
                log.Error(ex.ToString());
            }
            finally
            {
                try
                {
                    writer.Write(resBoby);
                    writer.Flush();

                    if (null != reader)
                    {
                        reader.Close();
                    }
                    if (null != writer)
                    {
                        writer.Close();
                    }
                    LogEnd(res, resBoby);
                }
                catch (Exception clsEx)
                {
                    log.Error(clsEx.ToString());
                }
            }
        }

        /// <summary>
        /// リクエストログを出力する
        /// </summary>
        /// <param name="req">リクエスト情報</param>
        /// <param name="body">リクエストボディ文字列</param>
        private void LogStart(HttpListenerRequest req, string body)
        {
            log.Info("########## Request [start] ##########");
            log.Info(String.Format(">> {0} {1}", req.HttpMethod, GetApiPath(req.RawUrl)));
            log.Info(">> IP: " + req.UserHostAddress);
            log.Info(">> UserAgent: " + req.UserAgent);
            log.Info(">> Header: " + ToNameValueString(req.Headers));
            if ("GET".Equals(req.HttpMethod))
            {
                if (0 < req.QueryString.Count)
                {
                    log.Info(">> Query: " + ToNameValueString(req.QueryString));

                }
            }
            else
            {
                if (!string.IsNullOrEmpty(body))
                {
                    log.Info(">> Body: " + body);
                }
            }
            log.Info("########## Request [end] ##########");
        }

        /// <summary>
        /// レスポンスログを出力する
        /// </summary>
        /// <param name="res">レスポンス情報</param>
        /// <param name="body">レスポンスボディ文字列</param>
        private void LogEnd(HttpListenerResponse res, string body)
        {
            log.Info("########## Response [start] ##########");
            log.Info(">> HTTP Status: " + res.StatusCode);
            log.Info(">> Header: " + ToNameValueString(res.Headers));
            if (!string.IsNullOrEmpty(body))
            {
                log.Info(">> Body: " + body);
            }
            log.Info("########## Response [end] ##########");
        }

        /// <summary>
        /// Name-Value文字列を取得する
        /// </summary>
        /// <param name="nvc">nvc</param>
        /// <returns>文字列</returns>
        private string ToNameValueString(NameValueCollection nvc)
        {
            return String.Join(", ", Array.ConvertAll(nvc.AllKeys, key => String.Format("{0}:{1}", key, nvc[key])));
        }

        /// <summary>
        /// APIエラーレスポンスを生成する
        /// </summary>
        /// <param name="ex">例外オブジェクト</param>
        /// <returns>レスポンス文字列</returns>
        private string CreateApiErrorResponse(ApiException ex)
        {

            if (ex.ExceptionList != null && 0 < ex.ExceptionList.Count)
            {
                // API例外が2件以上ある場合
                Response res = new Response
                {
                    ResultType = (int)ResultType.ERROR,
                    ResultDetailList = new List<ResultDetail>()
                };
                foreach (ApiException exItem in ex.ExceptionList)
                {
                    ResultDetail detail = new ResultDetail
                    {
                        Code = (int)exItem.ErrorCode,
                        Message = exItem.Message
                    };
                    res.ResultDetailList.Add(detail);

                    log.Error(String.Format("code={0}, message={1}", detail.Code, detail.Message));
                }
                return JsonConvert.SerializeObject(res, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            else
            {
                // API例外が1件である場合
                return CreateErrorResponse(ex.ErrorCode, ex.Message);
            }
        }

        /// <summary>
        /// エラーレスポンスを生成する
        /// </summary>
        /// <param name="code">エラーコード</param>
        /// <param name="msg">メッセージ</param>
        /// <returns>レスポンス文字列</returns>
        private string CreateErrorResponse(ErrorCode code, string msg)
        {
            Response res = new Response
            {
                ResultType = (int)ResultType.ERROR,
                ResultDetailList = new List<ResultDetail>()
            };
            ResultDetail detail = new ResultDetail
            {
                Code = (int)code,
                Message = msg
            };
            res.ResultDetailList.Add(detail);
            log.Error(String.Format("code={0}, message={1}", detail.Code, detail.Message));

            return JsonConvert.SerializeObject(res, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        /// <summary>
        /// APIパスを取得する
        /// </summary>
        /// <param name="srcPath">URLパス</param>
        /// <returns>APIパス</returns>
        private string GetApiPath(string srcPath)
        {
            string[] path = srcPath.Split('?');
            string condition = String.Format(@"^/{0}", Settings.Default.API_PATH);
            return Regex.Replace(path[0], condition, "");
        }

        /// <summary>
        /// APIコントローラを実行する
        /// </summary>
        /// <param name="req">リクエスト情報</param>
        /// <param name="res">レスポンス情報</param>
        /// <param name="reqBody">リクエストボディ</param>
        /// <returns>レスポンス文字列</returns>
        private string ExecuteController(HttpListenerRequest req, HttpListenerResponse res, string reqBody)
        {
            string path = GetApiPath(req.RawUrl);

            if ("/user/".Equals(path))
            {
                switch (req.HttpMethod)
                {
                    case "GET":
                        return (new ReadUserController(req, res, reqBody)).Execute();
                    case "POST":
                        return (new CreateUserController(req, res, reqBody)).Execute();
                    case "PUT":
                        return (new UpdateUserController(req, res, reqBody)).Execute();
                    case "DELETE":
                        return (new DeleteUserController(req, res, reqBody)).Execute();
                }
            }
            if ("/users/".Equals(path) && "GET".Equals("GET"))
            {
                return (new ReadUsersController(req, res, reqBody)).Execute();
            }
            return "";
        }
    }
}

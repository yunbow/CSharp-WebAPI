using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using WebApi.Api;
using WebApi.Properties;

namespace WebApi.Common
{
    class ApiService
    {
        private static Logger log = Logger.GetInstance();
        private HttpListener listener;
        private ControllerMapper mapper = new ControllerMapper();

        /// <summary>
        /// APIサービスを起動する
        /// </summary>
        public void Start()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string strSystemName = asm.GetName().Name;

            log.Info("########## HTTP Server [start] ##########");
            log.Info(">> System Name: " + strSystemName);
            log.Info(">> System Version: " + asm.GetName().Version);

            try
            {
                // HTTPサーバーを起動する
                this.listener = new HttpListener();
                this.listener.Prefixes.Add(String.Format("http://+:{0}/{1}/", Settings.Default.API_PORT, Settings.Default.API_PATH));
                this.listener.Start();

                log.Info(Resources.StartServer);
                EventLog.WriteEntry(GetSystemName(), Resources.StartServer, EventLogEntryType.Information, (int)ErrorCode.SUCCESS);

                while (this.listener.IsListening)
                {
                    IAsyncResult result = this.listener.BeginGetContext(OnRequested, this.listener);
                    result.AsyncWaitHandle.WaitOne();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                EventLog.WriteEntry(GetSystemName(), ex.ToString(), EventLogEntryType.Error, (int)ErrorCode.ERROR_START);
            }
        }

        /// <summary>
        /// APIサービスを停止する
        /// </summary>
        public void Stop()
        {
            try
            {
                // HTTPサーバーを停止する
                this.listener.Stop();
                this.listener.Close();

                log.Info(Resources.StopServer);
                EventLog.WriteEntry(GetSystemName(), Resources.StopServer, EventLogEntryType.Information, (int)ErrorCode.SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());

                Assembly clsAsm = Assembly.GetExecutingAssembly();
                string strSystemName = clsAsm.GetName().Name;

                EventLog.WriteEntry(GetSystemName(), ex.ToString(), EventLogEntryType.Error, (int)ErrorCode.ERROR_STOP);
            }

            log.Info("########## HTTP Server [end] ##########");
        }

        /// <summary>
        /// リクエスト時の処理を実行する
        /// </summary>
        /// <param name="result">結果</param>
        private void OnRequested(IAsyncResult result)
        {
            HttpListener clsListener = (HttpListener)result.AsyncState;
            if (!clsListener.IsListening)
            {
                return;
            }

            HttpListenerContext context = clsListener.EndGetContext(result);
            HttpListenerRequest req = context.Request;
            HttpListenerResponse res = context.Response;

            try
            {
                mapper.Execute(req, res);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            finally
            {
                try
                {
                    if (null != res)
                    {
                        res.Close();
                    }
                }
                catch (Exception clsEx)
                {
                    log.Error(clsEx.ToString());
                }
            }
        }

        /// <summary>
        /// システム名を取得する
        /// </summary>
        /// <returns>システム名</returns>
        private string GetSystemName()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetName().Name;
        }
    }
}

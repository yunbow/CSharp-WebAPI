using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using WebApi.Common;

namespace WebApi.Validator
{
    /// <summary>
    /// バリデータ共通
    /// </summary>
    abstract class AbstractValidator<TAttr> where TAttr : class
    {
        protected static Logger log = Logger.GetInstance();
        protected object srcObj;
        protected PropertyInfo prop;
        protected List<ApiException> exceptionList;
        protected TAttr attr;
        protected JsonPropertyAttribute jsonProp;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AbstractValidator()
        {
        }

        /// <summary>
        /// 実行する
        /// </summary>
        /// <param name="srcObj">オブジェクト</param>
        /// <param name="prop">プロパティ</param>
        /// <param name="lstEx">エラーリスト</param>
        /// <returns>実行結果</returns>
        public bool Execute(object srcObj, PropertyInfo prop, List<ApiException> exList)
        {
            this.srcObj = srcObj;
            this.prop = prop;
            this.exceptionList = exList;
            this.attr = Attribute.GetCustomAttribute(prop, typeof(TAttr)) as TAttr;
            this.jsonProp = Attribute.GetCustomAttribute(prop, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;

            if (this.attr != null)
            {
                // バリデーションの属性が指定されている場合のみ実行する
                return ExecuteCore();
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 例外を追加する
        /// </summary>
        /// <param name="msg">メッセージ</param>
        protected void AddException(string msg)
        {
            this.exceptionList.Add(new ApiException(ResultType.ERROR, ErrorCode.ERROR_VALIDATION, msg));
        }

        /// <summary>
        /// 本処理を実行する
        /// </summary>
        /// <returns>実行結果</returns>
        abstract public bool ExecuteCore();
    }
}

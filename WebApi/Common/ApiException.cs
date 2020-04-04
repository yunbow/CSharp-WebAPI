using System;
using System.Collections.Generic;

namespace WebApi.Common
{
    class ApiException : Exception
    {
        /// <summary>
        /// 結果種別
        /// </summary>
        public ResultType ResultType { get; }

        /// <summary>
        /// エラーコード
        /// </summary>
        public ErrorCode ErrorCode { get; }

        /// <summary>
        /// 例外リスト
        /// </summary>
        public List<ApiException> ExceptionList { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type">結果種別</param>
        /// <param name="code">エラーコード</param>
        /// <param name="msg">メッセージ</param>
        public ApiException(ResultType type, ErrorCode code, string msg) : base(msg)
        {
            this.ResultType = type;
            this.ErrorCode = code;
        }

        /// <summary>
        /// コンストラクタ（主にバリデーションで使用する）
        /// </summary>
        /// <param name="exList">例外リスト</param>
        public ApiException(List<ApiException> exList)
        {
            this.ExceptionList = exList;
        }
    }
}

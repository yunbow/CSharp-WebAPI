namespace WebApi.Common
{
    /// <summary>
    /// 結果種別
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 不明
        /// </summary>
        UNKNOWN = -1,

        /// <summary>
        /// 成功
        /// </summary>
        SUCCESS = 0,

        /// <summary>
        /// 失敗
        /// </summary>
        ERROR = 1,
    }

    /// <summary>
    /// エラーコード
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 正常終了
        /// </summary>
        SUCCESS = 0,

        /// <summary>
        /// 起動エラー
        /// </summary>
        ERROR_START = 1001,

        /// <summary>
        /// 停止エラー
        /// </summary>
        ERROR_STOP = 1002,

        /// <summary>
        /// JSON構文エラー
        /// </summary>
        ERROR_JSON_SYNTAX = 1003,

        /// <summary>
        /// バリデーションエラー
        /// </summary>
        ERROR_VALIDATION = 1004,

        /// <summary>
        /// システムエラー
        /// </summary>
        SYSTEM_ERROR = 9999,
    }
}

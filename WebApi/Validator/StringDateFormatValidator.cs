using System;
using System.Globalization;
using WebApi.Properties;

namespace WebApi.Validator
{
    /// <summary>
    /// 文字列日付フォーマットバリデーションの属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class StringDateFormatAttribute : Attribute
    {
        private string format;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StringDateFormatAttribute(string format)
        {
            this.format = format;
        }

        /// <summary>
        /// フォーマット
        /// </summary>
        public string Format
        {
            get
            {
                return this.format;
            }
        }
    }

    /// <summary>
    /// 文字列日付フォーマットバリデーション
    /// </summary>
    class StringDateFormatValidator : AbstractValidator<StringDateFormatAttribute>
    {
        /// <summary>
        /// 本処理を実行する
        /// </summary>
        /// <returns>実行結果</returns>
        public override bool ExecuteCore()
        {
            object value = this.prop.GetValue(this.srcObj, null);
            Type type = this.prop.PropertyType;

            if (value != null && type == typeof(string))
            {
                string strValue = value.ToString();
                if (!DateTime.TryParseExact(strValue, this.attr.Format, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateTime))
                {
                    AddException(string.Format(Resources.ErrorValidationStringDateFormat, this.jsonProp.PropertyName, value, this.attr.Format));
                    return false;
                }
            }
            return true;
        }
    }

}

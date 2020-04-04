using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Properties;

namespace WebApi.Validator
{
    /// <summary>
    /// 文字列長バリデーションの属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class StringLengthAttribute : Attribute
    {
        public int max = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StringLengthAttribute()
        {
        }
    }

    /// <summary>
    /// 文字列長バリデーション
    /// </summary>
    class StringLengthValidator : AbstractValidator<StringLengthAttribute>
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
                if (this.attr.max < strValue.Length)
                {
                    AddException(string.Format(Resources.ErrorValidationStringLength, this.jsonProp.PropertyName, value, this.attr.max));
                    return false;
                }
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Properties;

namespace WebApi.Validator
{
    /// <summary>
    /// 必須バリデーションの属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class RequiredAttribute : Attribute
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RequiredAttribute()
        {
        }
    }

    /// <summary>
    /// 必須バリデーション
    /// </summary>
    class RequiredValidator : AbstractValidator<RequiredAttribute>
    {
        /// <summary>
        /// 本処理を実行する
        /// </summary>
        /// <returns>実行結果</returns>
        override public bool ExecuteCore()
        {
            // nullチェック
            object value = this.prop.GetValue(this.srcObj, null);
            if (value == null)
            {
                AddException(string.Format(Resources.ErrorValidationRequired, this.jsonProp.PropertyName));
                return false;
            }

            Type type = this.prop.PropertyType;
            if (type == typeof(string))
            {
                // 空文字チェック
                string strValue = value.ToString();
                if (string.IsNullOrEmpty(strValue))
                {
                    AddException(string.Format(Resources.ErrorValidationRequired, this.jsonProp.PropertyName));
                    return false;
                }
            }
            return true;
        }
    }

}

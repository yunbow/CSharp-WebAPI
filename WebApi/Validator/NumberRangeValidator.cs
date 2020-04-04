using System;
using WebApi.Properties;

namespace WebApi.Validator
{
    /// <summary>
    /// 数字範囲バリデーションの属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class NumberRangeAttribute : Attribute
    {
        public int from = 0;
        public int to = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NumberRangeAttribute()
        {
        }
    }

    /// <summary>
    /// 数字範囲バリデーション
    /// </summary>
    class NumberRangeValidator : AbstractValidator<NumberRangeAttribute>
    {
        /// <summary>
        /// 本処理を実行する
        /// </summary>
        /// <returns>実行結果</returns>
        public override bool ExecuteCore()
        {
            object value = this.prop.GetValue(this.srcObj, null);
            Type type = this.prop.PropertyType;

            if (value != null && type == typeof(int))
            {
                int intValue = (int)value;
                if (intValue < this.attr.from || this.attr.to < intValue)
                {
                    AddException(string.Format(Resources.ErrorValidationNumberRange, this.jsonProp.PropertyName, intValue, this.attr.from, this.attr.to));
                    return false;
                }
            }
            return true;
        }
    }
}

using System;
using System.Collections;
using WebApi.Properties;

namespace WebApi.Validator
{
    /// <summary>
    /// 区分値バリデーションの属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class ClassValueAttribute : Attribute
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClassValueAttribute(object value)
        {
            this.Value = value;
        }

        /// <summary>
        /// オブジェクト
        /// </summary>
        public object Value { get; }
    }

    /// <summary>
    /// 区分値バリデーション
    /// </summary>
    class ClassValueValidator : AbstractValidator<ClassValueAttribute>
    {
        /// <summary>
        /// 本処理を実行する
        /// </summary>
        /// <returns>実行結果</returns>
        public override bool ExecuteCore()
        {
            object value = this.prop.GetValue(this.srcObj, null);

            if (value != null)
            {
                if (this.attr.Value != null)
                {
                    // オブジェクト
                    Type classValueType = this.attr.Value.GetType();
                    if (classValueType.IsArray)
                    {
                        bool isExist = false;
                        var classValueList = (IEnumerable)this.attr.Value;
                        foreach (var classValue in classValueList)
                        {
                            if (value.Equals(classValue))
                            {
                                isExist = true;
                            }
                        }
                        if (!isExist)
                        {
                            AddException(string.Format(Resources.ErrorValidationClass, this.jsonProp.PropertyName, value));
                            return false;
                        }
                    }
                    else
                    {
                        if (!value.Equals(this.attr.Value))
                        {
                            AddException(string.Format(Resources.ErrorValidationClass, this.jsonProp.PropertyName, value));
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebApi.Common;

namespace WebApi.Validator
{
    /// <summary>
    /// バリデータ振り分け
    /// </summary>
    class ValidatorMapper
    {
        private static Logger log = Logger.GetInstance();
        private readonly object srcObj;
        private readonly List<ApiException> exceptionList;
        private readonly RequiredValidator requiredValidator = new RequiredValidator();
        private readonly StringLengthValidator stringLengthValidator = new StringLengthValidator();
        private readonly StringDateFormatValidator stringDateFormatValidator = new StringDateFormatValidator();
        private readonly NumberRangeValidator numberRangeValidator = new NumberRangeValidator();
        private readonly ClassValueValidator classValueValidator = new ClassValueValidator();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="srcObj">オブジェクト</param>
        public ValidatorMapper(object srcObj)
        {
            this.srcObj = srcObj;
            this.exceptionList = new List<ApiException>();
        }

        /// <summary>
        /// 実行する
        /// </summary>
        public void Execute()
        {
            foreach (PropertyInfo clsProp in this.srcObj.GetType().GetProperties())
            {
                Validate(this.srcObj, clsProp);
            }

            if (this.exceptionList != null && 0 < this.exceptionList.Count)
            {
                // 例外リストが1件以上の場合は、API例外を投げる
                throw new ApiException(this.exceptionList);
            }
        }

        /// <summary>
        /// バリデーションを実行する
        /// </summary>
        /// <param name="targetObj">オブジェクト</param>
        /// <param name="targetProp">プロパティ</param>
        private void Validate(object targetObj, PropertyInfo targetProp)
        {
            Type targetPropType = targetProp.PropertyType;
            string targetPropName = targetProp.Name;
            bool result = false;

            if (targetPropType.IsGenericType && targetPropType.GetGenericTypeDefinition() == typeof(List<>))
            {
                // リスト型
                var subPropValList = (IEnumerable)targetProp.GetValue(targetObj, null);
                int count = 0;

                // 必須チェック
                result = this.requiredValidator.Execute(targetObj, targetProp, exceptionList);

                log.Debug("type=[lst] key=[" + targetPropName + "] ---> Start");
                if (result && subPropValList != null)
                {
                    foreach (var objSubPropVal in subPropValList)
                    {
                        log.Debug("type=[lst] key=[" + targetPropName + "][" + count + "] ---> Start");
                        foreach (PropertyInfo subProp in objSubPropVal.GetType().GetProperties())
                        {
                            Validate(objSubPropVal, subProp);
                        }
                        log.Debug("type=[lst] key=[" + targetPropName + "][" + count + "] <--- End");
                        count++;
                    }
                }
                log.Debug("type=[lst] key=[" + targetPropName + "] <--- End");
            }
            else if (targetPropType.IsArray)
            {
                // 配列型
                var subPropValList = (IEnumerable)targetProp.GetValue(targetObj, null);

                // 必須チェック
                result = this.requiredValidator.Execute(targetObj, targetProp, exceptionList);

                log.Debug("type=[arr] key=[" + targetPropName + "] ---> Start");
                if (result && subPropValList != null)
                {
                    foreach (var subPropVal in subPropValList)
                    {
                        Validate(subPropVal, targetProp);
                    }
                }
                log.Debug("type=[arr] key=[" + targetPropName + "] <--- End");
            }
            else
            {
                if (!(targetPropType.IsClass || targetPropType.IsPrimitive))
                {
                    // null許容型
                    targetPropType = Nullable.GetUnderlyingType(targetPropType);
                }

                object propVal = targetProp.GetValue(targetObj, null);

                if (targetPropType == typeof(string))
                {
                    // string型
                    log.Debug("type=[str] key=[" + targetPropName + "], val=[" + propVal + "]");

                    // 必須チェック
                    result = this.requiredValidator.Execute(targetObj, targetProp, exceptionList);

                    if (result)
                    {
                        // 文字列長チェック
                        this.stringLengthValidator.Execute(targetObj, targetProp, exceptionList);

                        // 区分値チェック
                        this.classValueValidator.Execute(targetObj, targetProp, exceptionList);

                        // 日付フォーマットチェック
                        this.stringDateFormatValidator.Execute(targetObj, targetProp, exceptionList);
                    }
                }
                else if (targetPropType == typeof(int))
                {
                    // int型
                    log.Debug("type=[int] key=[" + targetPropName + "], val=[" + propVal + "]");

                    // 必須チェック
                    result = this.requiredValidator.Execute(targetObj, targetProp, exceptionList);

                    if (result)
                    {
                        // 範囲チェック
                        this.numberRangeValidator.Execute(targetObj, targetProp, exceptionList);

                        // 区分値チェック
                        this.classValueValidator.Execute(targetObj, targetProp, exceptionList);
                    }
                }
                else if (targetPropType == typeof(double))
                {
                    // double型
                    log.Debug("type=[dbl] key=[" + targetPropName + "], val=[" + propVal + "]");

                    // 必須チェック
                    result = this.requiredValidator.Execute(targetObj, targetProp, exceptionList);

                    if (result)
                    {
                        // 範囲チェック
                        this.numberRangeValidator.Execute(targetObj, targetProp, exceptionList);
                    }
                }
                else if (targetPropType == typeof(bool))
                {
                    // bool型
                    log.Debug("type=[bol] key=[" + targetPropName + "], val=[" + propVal + "]");

                    // 必須チェック
                    this.requiredValidator.Execute(targetObj, targetProp, exceptionList);
                }
                else
                {
                    // オブジェクト型                        
                    // 必須チェック
                    result = this.requiredValidator.Execute(targetObj, targetProp, exceptionList);

                    log.Debug("type=[obj] key=[" + targetPropName + "] ---> Start");
                    if (result && propVal != null)
                    {
                        foreach (PropertyInfo subProp in propVal.GetType().GetProperties())
                        {
                            Validate(propVal, subProp);
                        }
                    }
                    log.Debug("type=[obj] key=[" + targetPropName + "] <--- End");
                }
            }
        }
    }
}

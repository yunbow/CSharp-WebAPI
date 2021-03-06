﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApi.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApi.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   A JSON syntax error occurred.:{0} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ErrorJsonSyntax {
            get {
                return ResourceManager.GetString("ErrorJsonSyntax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   An unexpected error has occurred.:{0} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ErrorUnexpected {
            get {
                return ResourceManager.GetString("ErrorUnexpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &quot;{0}&quot;({1}) is an unavailable value. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ErrorValidationClass {
            get {
                return ResourceManager.GetString("ErrorValidationClass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &quot;{0}&quot;({1}) is from {2} to {3}. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ErrorValidationNumberRange {
            get {
                return ResourceManager.GetString("ErrorValidationNumberRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &quot;{0}&quot; is a required field. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ErrorValidationRequired {
            get {
                return ResourceManager.GetString("ErrorValidationRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &quot;{0}&quot;({1}) should be entered in the form {2}. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ErrorValidationStringDateFormat {
            get {
                return ResourceManager.GetString("ErrorValidationStringDateFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &quot;{0}&quot;({1}) exceeds the number of characters {2} digits. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ErrorValidationStringLength {
            get {
                return ResourceManager.GetString("ErrorValidationStringLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The server has been successfully started. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string StartServer {
            get {
                return ResourceManager.GetString("StartServer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   The server has been successfully shut down. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string StopServer {
            get {
                return ResourceManager.GetString("StopServer", resourceCulture);
            }
        }
    }
}

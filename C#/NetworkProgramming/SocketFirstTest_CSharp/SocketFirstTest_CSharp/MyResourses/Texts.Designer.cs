﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocketFirstTest_CSharp.MyResourses {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Texts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Texts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SocketFirstTest_CSharp.MyResourses.Texts", typeof(Texts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Off.
        /// </summary>
        public static string ClientOff {
            get {
                return ResourceManager.GetString("ClientOff", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Отключился.
        /// </summary>
        public static string ClientOffServerLogMessage {
            get {
                return ResourceManager.GetString("ClientOffServerLogMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Сервер обнаружил подключение:.
        /// </summary>
        public static string ConnectionFound {
            get {
                return ResourceManager.GetString("ConnectionFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка.
        /// </summary>
        public static string Error {
            get {
                return ResourceManager.GetString("Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не найден локальный IP для сокета сервера!.
        /// </summary>
        public static string LocalIpNotFound {
            get {
                return ResourceManager.GetString("LocalIpNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MutexForSocketTestServer.
        /// </summary>
        public static string MutexName {
            get {
                return ResourceManager.GetString("MutexName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Сервер.
        /// </summary>
        public static string Server {
            get {
                return ResourceManager.GetString("Server", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Сервер уже запущен!.
        /// </summary>
        public static string ServerAlreadyRunning {
            get {
                return ResourceManager.GetString("ServerAlreadyRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ready.
        /// </summary>
        public static string ServerAnswer {
            get {
                return ResourceManager.GetString("ServerAnswer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Сервер принял сообщение от:.
        /// </summary>
        public static string ServerGetMessage {
            get {
                return ResourceManager.GetString("ServerGetMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Сервер отправил сообщение клиенту с именем.
        /// </summary>
        public static string ServerSendMessage {
            get {
                return ResourceManager.GetString("ServerSendMessage", resourceCulture);
            }
        }
    }
}

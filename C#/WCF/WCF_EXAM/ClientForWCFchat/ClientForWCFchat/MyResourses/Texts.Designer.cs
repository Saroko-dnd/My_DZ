﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientForWCFchat.MyResourses {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
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
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ClientForWCFchat.MyResourses.Texts", typeof(Texts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
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
        ///   Ищет локализованную строку, похожую на Сейчас в чате:.
        /// </summary>
        public static string ClientInChatLabel {
            get {
                return ResourceManager.GetString("ClientInChatLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя клиента не может быть пустым!.
        /// </summary>
        public static string ClientNameEmptyError {
            get {
                return ResourceManager.GetString("ClientNameEmptyError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя клиента:.
        /// </summary>
        public static string ClientNameLabel {
            get {
                return ResourceManager.GetString("ClientNameLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Ошибка.
        /// </summary>
        public static string Error {
            get {
                return ResourceManager.GetString("Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя вашего клиента уже используется!.
        /// </summary>
        public static string InvalidClientNameError {
            get {
                return ResourceManager.GetString("InvalidClientNameError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Подключиться к чату.
        /// </summary>
        public static string JoinChatButton {
            get {
                return ResourceManager.GetString("JoinChatButton", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Личные сообщения.
        /// </summary>
        public static string PrivateChatLabel {
            get {
                return ResourceManager.GetString("PrivateChatLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Личное сообщение.
        /// </summary>
        public static string PrivateMessageTabItem {
            get {
                return ResourceManager.GetString("PrivateMessageTabItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Общий чат.
        /// </summary>
        public static string PublicChatLabel {
            get {
                return ResourceManager.GetString("PublicChatLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Публичное сообщение.
        /// </summary>
        public static string PublicMessageTabItem {
            get {
                return ResourceManager.GetString("PublicMessageTabItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Получатель:.
        /// </summary>
        public static string ReceiverLabel {
            get {
                return ResourceManager.GetString("ReceiverLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Отправить сообщение.
        /// </summary>
        public static string SendMessageButton {
            get {
                return ResourceManager.GetString("SendMessageButton", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Нельзя дважы подключить один клиент к чату!.
        /// </summary>
        public static string YouAlreadyInChatError {
            get {
                return ResourceManager.GetString("YouAlreadyInChatError", resourceCulture);
            }
        }
    }
}

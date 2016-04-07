﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientForWCFduplex.DateTimeInfoWCFduplex {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DateTimeInfoWCFduplex.IDateTimeInfo", CallbackContract=typeof(ClientForWCFduplex.DateTimeInfoWCFduplex.IDateTimeInfoCallback))]
    public interface IDateTimeInfo {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDateTimeInfo/ReturnTime")]
        void ReturnTime(int Period, int Number);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDateTimeInfo/ReturnTime")]
        System.Threading.Tasks.Task ReturnTimeAsync(int Period, int Number);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDateTimeInfoCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDateTimeInfo/ReceiveTime")]
        void ReceiveTime(System.DateTime CurrentTime);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDateTimeInfoChannel : ClientForWCFduplex.DateTimeInfoWCFduplex.IDateTimeInfo, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DateTimeInfoClient : System.ServiceModel.DuplexClientBase<ClientForWCFduplex.DateTimeInfoWCFduplex.IDateTimeInfo>, ClientForWCFduplex.DateTimeInfoWCFduplex.IDateTimeInfo {
        
        public DateTimeInfoClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public DateTimeInfoClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public DateTimeInfoClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public DateTimeInfoClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public DateTimeInfoClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void ReturnTime(int Period, int Number) {
            base.Channel.ReturnTime(Period, Number);
        }
        
        public System.Threading.Tasks.Task ReturnTimeAsync(int Period, int Number) {
            return base.Channel.ReturnTimeAsync(Period, Number);
        }
    }
}

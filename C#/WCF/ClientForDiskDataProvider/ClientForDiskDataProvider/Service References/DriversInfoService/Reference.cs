﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientForDiskDataProvider.DriversInfoService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MainDriveInfo", Namespace="http://schemas.datacontract.org/2004/07/WCFdriversInfoServiceDLL")]
    [System.SerializableAttribute()]
    public partial class MainDriveInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AvailableSpaceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TotalSpaceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AvailableSpace {
            get {
                return this.AvailableSpaceField;
            }
            set {
                if ((object.ReferenceEquals(this.AvailableSpaceField, value) != true)) {
                    this.AvailableSpaceField = value;
                    this.RaisePropertyChanged("AvailableSpace");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TotalSpace {
            get {
                return this.TotalSpaceField;
            }
            set {
                if ((object.ReferenceEquals(this.TotalSpaceField, value) != true)) {
                    this.TotalSpaceField = value;
                    this.RaisePropertyChanged("TotalSpace");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DriversInfoService.IDiskInfo")]
    public interface IDiskInfo {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiskInfo/GetDriversData", ReplyAction="http://tempuri.org/IDiskInfo/GetDriversDataResponse")]
        string GetDriversData();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiskInfo/GetDriversData", ReplyAction="http://tempuri.org/IDiskInfo/GetDriversDataResponse")]
        System.Threading.Tasks.Task<string> GetDriversDataAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiskInfo/GetOneDriveData", ReplyAction="http://tempuri.org/IDiskInfo/GetOneDriveDataResponse")]
        ClientForDiskDataProvider.DriversInfoService.MainDriveInfo GetOneDriveData(string DriveName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiskInfo/GetOneDriveData", ReplyAction="http://tempuri.org/IDiskInfo/GetOneDriveDataResponse")]
        System.Threading.Tasks.Task<ClientForDiskDataProvider.DriversInfoService.MainDriveInfo> GetOneDriveDataAsync(string DriveName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiskInfo/GetDriversNames", ReplyAction="http://tempuri.org/IDiskInfo/GetDriversNamesResponse")]
        System.Collections.Generic.List<string> GetDriversNames();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiskInfo/GetDriversNames", ReplyAction="http://tempuri.org/IDiskInfo/GetDriversNamesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<string>> GetDriversNamesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDiskInfoChannel : ClientForDiskDataProvider.DriversInfoService.IDiskInfo, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DiskInfoClient : System.ServiceModel.ClientBase<ClientForDiskDataProvider.DriversInfoService.IDiskInfo>, ClientForDiskDataProvider.DriversInfoService.IDiskInfo {
        
        public DiskInfoClient() {
        }
        
        public DiskInfoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DiskInfoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DiskInfoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DiskInfoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetDriversData() {
            return base.Channel.GetDriversData();
        }
        
        public System.Threading.Tasks.Task<string> GetDriversDataAsync() {
            return base.Channel.GetDriversDataAsync();
        }
        
        public ClientForDiskDataProvider.DriversInfoService.MainDriveInfo GetOneDriveData(string DriveName) {
            return base.Channel.GetOneDriveData(DriveName);
        }
        
        public System.Threading.Tasks.Task<ClientForDiskDataProvider.DriversInfoService.MainDriveInfo> GetOneDriveDataAsync(string DriveName) {
            return base.Channel.GetOneDriveDataAsync(DriveName);
        }
        
        public System.Collections.Generic.List<string> GetDriversNames() {
            return base.Channel.GetDriversNames();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<string>> GetDriversNamesAsync() {
            return base.Channel.GetDriversNamesAsync();
        }
    }
}

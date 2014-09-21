﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SubstringCounterConsoleClient.SubstringCounterServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SubstringCounterServiceReference.ISubstringCounter")]
    public interface ISubstringCounter {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISubstringCounter/GetSubstringCount", ReplyAction="http://tempuri.org/ISubstringCounter/GetSubstringCountResponse")]
        int GetSubstringCount(string target, string substr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISubstringCounter/GetSubstringCount", ReplyAction="http://tempuri.org/ISubstringCounter/GetSubstringCountResponse")]
        System.Threading.Tasks.Task<int> GetSubstringCountAsync(string target, string substr);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISubstringCounterChannel : SubstringCounterConsoleClient.SubstringCounterServiceReference.ISubstringCounter, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SubstringCounterClient : System.ServiceModel.ClientBase<SubstringCounterConsoleClient.SubstringCounterServiceReference.ISubstringCounter>, SubstringCounterConsoleClient.SubstringCounterServiceReference.ISubstringCounter {
        
        public SubstringCounterClient() {
        }
        
        public SubstringCounterClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SubstringCounterClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SubstringCounterClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SubstringCounterClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int GetSubstringCount(string target, string substr) {
            return base.Channel.GetSubstringCount(target, substr);
        }
        
        public System.Threading.Tasks.Task<int> GetSubstringCountAsync(string target, string substr) {
            return base.Channel.GetSubstringCountAsync(target, substr);
        }
    }
}
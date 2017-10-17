using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;

namespace WinServiceCalculator
{
    [RunInstaller(true)]
    public partial class CalculatorServiceInstaller : System.Configuration.Install.Installer
    {
        public CalculatorServiceInstaller()
        {
            //InitializeComponent();
            serviceProcessInstaller1 = new ServiceProcessInstaller();
            //serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;
            serviceInstaller1 = new ServiceInstaller();
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            serviceInstaller1.ServiceName = "WinCalculatorService";
            serviceInstaller1.DisplayName = "WinCalculatorService";
            serviceInstaller1.Description = "WCF Calculator Service Hosted by Windows Service";
            serviceInstaller1.StartType = ServiceStartMode.Automatic;
            Installers.Add(serviceProcessInstaller1);
            Installers.Add(serviceInstaller1);
        }
    }
}

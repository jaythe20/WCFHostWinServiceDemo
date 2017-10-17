using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;

namespace WinServiceCalculator
{
    public partial class Calculator : ServiceBase
    {

        ServiceHost myHost;
        public Calculator()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();
            myHost = new ServiceHost(typeof(WCFHost.CalculatorService));

            Uri address = new Uri("http://localhost:7097/CalculatorService");

            WSHttpBinding binding = new WSHttpBinding();

            Type contract = typeof(WCFHost.ICalculatorService);

            myHost.AddServiceEndpoint(contract, binding, address);

            myHost.Open();
        }

        protected override void OnStop()
        {

            if (myHost != null)
            {
                myHost.Close();
                myHost = null;
            }
        }
    }
}

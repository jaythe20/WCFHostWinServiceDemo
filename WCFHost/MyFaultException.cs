using System;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFHost
{
    public class MyFaultException
    {
        private string _reason;

        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
    }
}
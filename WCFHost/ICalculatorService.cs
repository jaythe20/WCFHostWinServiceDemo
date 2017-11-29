using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCFHost
{
    [ServiceContract]
    public interface ICalculatorService
    {

        [OperationContract]
        double Add(double dblNum1, double dblNum2);
        [OperationContract]
        double Subtract(double dblNum1, double dblNum2);
        [OperationContract]
        double Multiply(double dblNum1, double dblNum2);
        [OperationContract]
        [FaultContract(typeof(MyFaultException))]
        double Divide(double dblNum1, double dblNum2);
        [OperationContract]
        string RunAsAdmin(string code);

    }
}

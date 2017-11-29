using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WCFHost
{
    public class CalculatorService : ICalculatorService
    {
        public double Add(double dblNum1, double dblNum2)
        {
            return (dblNum1 + dblNum2);
        }

        public double Divide(double dblNum1, double dblNum2)
        {
            double output = 0.0;
            try
            {
                output = 10.00 / 0.0;
            }
            catch (Exception exp)
            {
                MyFaultException theFault = new MyFaultException();
                theFault.Reason = "Some Error " + exp.Message.ToString();
                throw new FaultException<MyFaultException>(theFault);
            }
            return output;
        }

        public double Multiply(double dblNum1, double dblNum2)
        {
            return (dblNum1 * dblNum2);
        }

        public double Subtract(double dblNum1, double dblNum2)
        {
            return (dblNum1 - dblNum2);
        }

        public string RunAsAdmin(string code)
        {
            //var ex = new CodeExecutor(code);
            Process.Start(code);
            return "";
            //return ex.Execute();
        }
    }
}

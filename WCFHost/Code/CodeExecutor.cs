using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace WCFHost
{
    public class CodeExecutor
    {
        private string _source = null;

        private string _usingStatements =
            @"using System;
                using System.Collections.Generic;
                using System.Text;
                using System.IO;
                using System.Linq;
                using System.Security.AccessControl;
                using System.Security.Principal;
                using System.Management;
                using Microsoft.Win32;";

        public CodeExecutor()
        {
            
        }
        public CodeExecutor(string code, string usingStatements = null)
        {
            // enclose source code with the proper padding
           
            if (_usingStatements != null)
            {
                usingStatements = _usingStatements + usingStatements;
            }
            else
            {
                usingStatements = _usingStatements;
            }

            _source = FormatCode(code, usingStatements);
        }

        private string FormatCode(string code, string usingStatements)
        {
            return String.Format(
                @"{0}
                 namespace RuntimeCode
                 {{
                    public class RuntimeClass
                    {{
                        public void RuntimeFunc()
                        {{{1}}}
                    }}
                }}", usingStatements, code);
        }

        public string Execute(string code, string usingStatements)
        {
             if (_usingStatements != null)
            {
                usingStatements = _usingStatements + usingStatements;
            }
            else
            {
                usingStatements = _usingStatements;
            }

            var source = FormatCode(code, usingStatements);

            return Execute(source);
        }

        private string TryExecute(string source, string dotNetVersion)
        {
            try
            {
                var providerOptions = new Dictionary<string, string>
                {
                    {"CompilerVersion", dotNetVersion}
                };
                var provider = new CSharpCodeProvider(providerOptions);

                var compilerParams = new CompilerParameters
                {
                    GenerateInMemory = true,
                    GenerateExecutable = false,
                };

                compilerParams.ReferencedAssemblies.Add("System.dll");
                compilerParams.ReferencedAssemblies.Add("System.Core.dll");
                compilerParams.ReferencedAssemblies.Add("System.Management.dll");
                
                var results = provider.CompileAssemblyFromSource(compilerParams, _source);

                if (results.Errors.Count != 0)
                {
                    string errorMsg = String.Empty;
                    foreach (var err in results.Errors)
                    {
                        errorMsg += err;
                        errorMsg += "\n";
                    }
                    return "Compilation error: " + errorMsg;
                }

                object o = results.CompiledAssembly.CreateInstance("RuntimeCode.RuntimeClass");
                MethodInfo mi = o.GetType().GetMethod("RuntimeFunc");
                mi.Invoke(o, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return "Runtime error: " + ex.InnerException.Message;
                
                    return "Runtime error: " + ex.Message;
            }

            return null;
        }

        private string Execute(string source)
        {
            // try execute with .net v4.0
            // if that fails -> execute with v3.5

            var res = TryExecute(source, "v4.0");
            if (res != null)
            {
                res = TryExecute(source, "v3.5");
            }

            return res;
        }

        public string Execute()
        {
            if (_source != null)
            {
                return Execute(_source);
            }

            return null;
        }


    }
}

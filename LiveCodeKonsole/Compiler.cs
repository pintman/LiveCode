using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;

namespace LiveCodeKonsole
{
    class Compiler
    {
        CSharpCodeProvider provider;
        CompilerParameters compilerParams;
        CompilerResults compilerResults;

        public Compiler()
        {
            provider = new CSharpCodeProvider();

            compilerParams = new CompilerParameters();
            compilerParams.GenerateInMemory = false;
            compilerParams.GenerateExecutable = true;
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                compilerParams.ReferencedAssemblies.Add(asm.Location);
            }
        }

        public void CompileSource(String sourceCode, String fileName)
        {
            compilerParams.OutputAssembly = fileName;
            compilerResults = provider.CompileAssemblyFromSource(compilerParams, sourceCode);            
        }

        public string GetPathToAssembly()
        {
            return compilerResults.PathToAssembly;
        }

        public void CheckCompilerResultsAndRun(ICompilerCheckedRunner runner)
        {
            if (compilerResults.Errors.HasErrors)
            {
                System.Diagnostics.Trace.TraceInformation("Error during compulation: " + CheckCompilerResults());
                runner.FehlerAnzeigen(CheckCompilerResults());
            }
            else
            {
                runner.KeineCompilerfehlerAnzeigen();
                runner.Ausfuehren();
            }
        }
        
        public string CheckCompilerResults()
        {
            String sFehler = "";
            if (compilerResults.Errors.HasErrors)
            {
                foreach (CompilerError ce in compilerResults.Errors)
                {
                    sFehler += ce.Line + ": " + ce.ErrorText;
                }
            }
            return sFehler;
        }

        public object New(String klassenName)
        {
            return compilerResults.CompiledAssembly.CreateInstance(klassenName);
        }
    }

    public interface ICompilerCheckedRunner
    {
        void FehlerAnzeigen(string sFehler);
        void KeineCompilerfehlerAnzeigen();
        void Ausfuehren();
    }
}

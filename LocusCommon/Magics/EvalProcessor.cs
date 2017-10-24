using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LocusCommon.Magics
{
    public class EvalProcessor
    {
        // 非公開フィールド
        private string code;
        private Assembly virtualAssembly;

        
        // 公開プロパティ

        public string Code
        {
            get => this.code;
        }


        // コンストラクタ

        public EvalProcessor(string code)
        {
            this.code = code;
            this.virtualAssembly = null;

            try
            {
                this._createAssembly();
            }
            catch
            {
                throw new InvalidOperationException("コンパイルに失敗しました。");
            }
        }


        // 非公開メソッド

        private void _createAssembly()
        {
            string targetCode = @"
public class VirtualClass
{
    public static Object VirtualMethod(Object parameter)
    {
        {0}
    }
}
".Replace("{0}", this.code);
            CodeDomProvider cp = new Microsoft.CSharp.CSharpCodeProvider();
            ICodeCompiler icc = cp.CreateCompiler();
            CompilerParameters cps = new CompilerParameters() { GenerateInMemory = true };
            CompilerResults cres = icc.CompileAssemblyFromSource(cps, targetCode);
            this.virtualAssembly = cres.CompiledAssembly;
        }


        // 公開メソッド

        public Object Execute(Object parameter)
        {
            if (this.virtualAssembly == null)
                throw new InvalidOperationException("コードがコンパイルされていません。");

            Object result = null;
            try
            {
                Type t = this.virtualAssembly.GetType("VirtualClass");
                result = t.InvokeMember("VirtualMethod", BindingFlags.InvokeMethod, null, null, new object[] { parameter });
            }
            catch (Exception ex)
            {
                throw new Exception("コード実行中に例外が発生しました。", ex);
            }

            return result;
        }

        public Object Execute()
        {
            return this.Execute(null);
        }
    }
}

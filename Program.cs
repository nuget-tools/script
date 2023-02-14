using System;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text;
using JavaCommons;
//using JavaCommons.Windows;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;

namespace CUI;
public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            if (args.Length < 1)
            {
                Util.Log("Please specify script file name.");
                Environment.Exit(1);
            }
            string scriptName = args[0];
            ArraySegment<string> arySeg = new ArraySegment<string>(args, 1, args.Length - 1);
            string[] argsSlice = arySeg.ToArray();
            string text = File.ReadAllText(scriptName, Encoding.UTF8);
            using (var engine = JavaScriptEngine.CreateEngine())
            {
                engine.Script.args = argsSlice;
                engine.Execute(text);
            }
        }
        catch (Exception e)
        {
            Util.Log(e.ToString());
            Environment.Exit(1);
        }
    }
}

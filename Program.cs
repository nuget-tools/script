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
            string lang;
            string script = get_script_language(text, out lang);
            Util.Log(lang, "lang");
            //Util.Log(script, "script");
            using (var engine = JavaScriptEngine.CreateEngine())
            {
                engine.Script.args = argsSlice;
                engine.Execute(script);
            }
        }
        catch (Exception e)
        {
            Util.Log(e.ToString());
            Environment.Exit(1);
        }
    }
    internal static string get_script_language(string text, out string lang)
    {
        lang = "javascript";
        text = text.Replace("\r\n", "\n");
        text = text.Replace("\r", "\n");
        string script = "";
        foreach (var line in text.Split('\n'))
        {
            if (line.StartsWith("#lang "))
            {
                lang = line.Replace("#lang ", "");
                lang = lang.Trim().ToLower();
            }
            else if (line.StartsWith("//#lang "))
            {
                lang = line.Replace("//#lang ", "");
                lang = lang.Trim().ToLower();
            }
            else if (line.StartsWith("#"))
            {
                ;
            }
            else
            {
                script += (line + "\n");
            }
        }
        switch (lang)
        {
            case "js":
                lang = "javascript";
                break;
            case "cs":
                lang = "csharp";
                break;
        }
        return script;
    }
}

using System;
using System.IO;
using System.Text;
using JavaCommons;
public class CSharpGlobal
{
    public string[] args { get; }
    public CSharpGlobal(string[] args)
    {
        this.args = args;
    }
}
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
            if (lang == "javascript")
            {
                using (var engine = JavaScriptEngine.CreateEngine())
                {
                    engine.Script.args = argsSlice;
                    engine.Execute(script);
                }
            }
            else if (lang == "csharp")
            {
                var engine = new CSharpEngine(false, new string[] { "JavaCommons" }, typeof(JavaCommons.Util).Assembly);
                engine.Exec(script, new CSharpGlobal(argsSlice));
            }
            else
            {
                Util.Log(string.Format("Invalid language specifier: {}", lang));
                Environment.Exit(1);
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

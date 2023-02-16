using JavaCommons;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
public class JavaScriptEngine
{
    public static V8ScriptEngine CreateEngine()
    {
        var engine = new V8ScriptEngine();
        engine.AddHostObject("clr", new HostTypeCollection("mscorlib", "System.Core", "JavaCommons"));
        engine.AddHostType("Console", typeof(System.Console));
        engine.AddHostType("Util", typeof(JavaCommons.Util));
        //string script = Util.ResourceAsText(typeof(JavaScriptEngine).Assembly, "peggy.min.js");
        //engine.Execute(script);
        engine.Execute(Util.ResourceAsText(typeof(JavaScriptEngine).Assembly, "peggy.min.js"));
        engine.Execute(Util.ResourceAsText(typeof(JavaScriptEngine).Assembly, "miniMAL-web.js"));
        engine.Execute("""
function parse(syntax, text)
{
    try
    {
        let parser = peggy.generate(syntax);
        let result = parser.parse(text);
        return result;
    }
    catch(e)
    {
        //print(e);
        //print(e.message);
        return { "!": "error", "?": e.message };
    }
}
""");
        return engine;
    }
    public static dynamic parse(V8ScriptEngine engine, string syntax, string text)
    {
        return engine.Invoke("parse", syntax, text);
    }
}

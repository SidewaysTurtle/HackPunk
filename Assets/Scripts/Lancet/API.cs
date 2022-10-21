using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

using Console;

// In-game compiler
namespace Lancet
{
    public class API : MonoBehaviour
    {
        void Start()
        {
            // FIXME: make this the call for the console
            Script.DefaultOptions.DebugPrint = (x) => Debug.Log(x);

            // https://www.moonsharp.org/scriptloaders.html
            ((ScriptLoaderBase)Script.DefaultOptions.ScriptLoader).IgnoreLuaPathGlobal = true;
            ((ScriptLoaderBase)Script.DefaultOptions.ScriptLoader).ModulePaths = ScriptLoaderBase.UnpackStringPaths(System.IO.Path.Combine(Application.persistentDataPath,"?") + "?.lua");
            
            Script.DefaultOptions.ScriptLoader = new LancetScriptLoader();

            // I misundershood how this works
            
            // string folder = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\Lancet\";
            // string filter = "*.lua";
            // try{
            //     foreach(var elm in System.IO.Directory.GetFiles(folder, filter))
            //     {
            //         try
            //         {
            //             ((ScriptLoaderBase)Script.DefaultOptions.ScriptLoader).ModulePaths = ScriptLoaderBase.UnpackStringPaths(elm);
            //             Debug.Log($"Loaded module {elm}");
            //         }
            //         catch (System.Exception ex)
            //         {
            //             Debug.Log($"Error loading module{elm} \n{ex}");
            //         }
            //     }
            // }
            // catch(System.Exception ex)
            // {
            //     Debug.Log($"No modules loaded\n{ex}");
            // }
        }

        
        
        public static void RunCode(string code)
        {
            Script script = new Script();
            DynValue fn = script.LoadString(code);
            fn.Function.Call();
        }

        public static void RunCodeInConsole(Dictionary<string,string[]> code, ConsoleManager console, Inventory ConsoleCommands)
        {
            Script script = new Script();
            script.Options.DebugPrint = (x) => console.CreateResponse(x);
            
            TextIcon icon = new TextIcon();
            for(int i = 0; i < ConsoleCommands.GetLength();i++)
            {
                ConsoleCommands.GetIcon(i,out icon);
                if(code[icon.name] != null)
                {
                    break;
                }
            }

            DynValue fn = script.LoadString(icon.FileData, );
            fn.Function.Call();
        }
    }
}
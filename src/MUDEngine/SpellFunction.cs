using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using ModernMUD;

namespace MUDEngine
{
    public class SpellFunction
    {
        public static int GenerateWithMain(StringBuilder sb, string csSource, string nameSpace)
        {
            sb.Append("using System;\r\n");
            sb.Append("using MUDEngine;\r\n");
            sb.Append("using ModernMUD;\r\n");
            sb.Append("\r\n");
            sb.AppendFormat("namespace {0}\r\n", nameSpace);
            sb.Append("{\r\n");
            sb.Append("public class SpellScript\r\n");
            sb.Append("{\r\n");
            sb.Append("static void Main()\r\n");
            sb.Append("{\r\n");
            sb.Append("}\r\n");
            sb.Append("public void Execute(CharData ch, Spell spell, int level, Target target)\r\n");
            sb.Append("{\r\n");
            sb.Append(csSource);
            sb.Append("\r\n}\r\n}\r\n}");
            return 10;
        }

        public static bool CompileSpell(Spell spell)
        {
            CSharpCodeProvider prov = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            cp.GenerateExecutable = true;
            cp.GenerateInMemory = true;
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Xml.dll");
            cp.ReferencedAssemblies.Add("MUDEngine.dll");
            cp.ReferencedAssemblies.Add("ZoneData.dll");

            Log.Trace("Compiling spell '" + spell.Name + "'.");

            StringBuilder sb = new StringBuilder();
            int idx = spell.FileName.IndexOf('.');
            string file = spell.FileName;
            if (idx > 0)
            {
                file = spell.FileName.Substring(0, idx);
            }
            int lines = GenerateWithMain(sb, spell.Code, "MUDEngine");

            CompilerResults cr = prov.CompileAssemblyFromSource(cp,sb.ToString());

            if (cr.Errors.HasErrors)
            {
                StringBuilder sbErr = new StringBuilder("Compiling Spell: ");
                sbErr.AppendFormat("\"{0}\" in file \"{1}\".", spell.Name, spell.FileName);
                sbErr.Append("\n\n");
                foreach (CompilerError err in cr.Errors)
                {
                    // Add 13 lines to the line number to get us the *actual* line number of the code.
                    sbErr.AppendFormat("{0} at code line {1} column {2}.", err.ErrorText, err.Line - 13, err.Column);
                    sbErr.Append("\n");
                }
                Log.Error(sbErr.ToString());
                Log.Info("Spell code is:\n" + spell.Code);
                return false;
            }

            spell.CompiledCode = cr.CompiledAssembly;
            Log.Trace("Compile successful.");
            return true;
        }

    }
}

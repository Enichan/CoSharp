using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator {
    class Program {
        static string template =
@"        public static Func<{0}, TRet> MakeSynchronous<{0}, TRet>(Func<{0}, IEnumerable<TRet>> f) {{
            Func<{0}, TRet> wrapped = ({1}) => {{
                var co = new Coroutine<TRet>(f({1})) {{ ThrowErrors = true }};
                while (co.Resume().Status != CoStatus.Dead) {{ }}
                return co.Result.ReturnValue;
            }};
            return wrapped;
        }}";
        static string templateArgs =
@"        public static Func<object, {0}, TRet> MakeSyncWithArgs<{0}, TRet>(Func<{0}, IEnumerable<TRet>> f) {{
            Func<object, {0}, TRet> wrapped = (args, {1}) => {{
                var co = new Coroutine<TRet>(f({1})) {{ ThrowErrors = true }};
                while (co.Resume(args).Status != CoStatus.Dead) {{ }}
                return co.Result.ReturnValue;
            }};
            return wrapped;
        }}";

        static void Main() {
            var str = new StringBuilder();
            str.AppendLine("using System;");
            str.AppendLine("using System.Collections.Generic;");
            str.AppendLine();
            str.AppendLine("namespace Coroutines {");
            str.AppendLine("    partial class Coroutine {");

            for (var i = 1; i <= 16; i++) {
                var f0 = String.Join(", ", from index in Enumerable.Range(1, i) select "TArg" + index);
                var f1 = String.Join(", ", from index in Enumerable.Range(1, i) select "v" + index);
                str.AppendLine(string.Format(template, f0, f1));
                if (i < 16) {
                    str.AppendLine(string.Format(templateArgs, f0, f1));
                }
            }

            str.AppendLine("    }");
            str.AppendLine("}");

            File.WriteAllText("Coroutine.MakeSync.cs", str.ToString());
        }
    }
}

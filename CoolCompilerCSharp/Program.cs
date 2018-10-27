using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolCompilerCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lexer = new Lexer.Lexer();

            var tokens = lexer.Lex("examples/hello-world.cl");
        }
    }
}

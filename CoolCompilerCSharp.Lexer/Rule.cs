using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoolCompilerCSharp.Lexer
{
    public class Rule
    {
        public Regex Regex { get; set; }
        public LexState[] State { get; set; }
        public Func<string,Token> CreateToken { get; set; }
    }
}

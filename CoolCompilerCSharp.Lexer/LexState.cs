using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolCompilerCSharp.Lexer
{
    public enum LexState
    {
        yyinitial,
        yycomment,
        yystring,
        yystringNewLineErr,
        yystringNullErr,
        yyeofError,

    }
}

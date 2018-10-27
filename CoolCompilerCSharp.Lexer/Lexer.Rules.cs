using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoolCompilerCSharp.Lexer
{
    public partial class Lexer
    {
        public LexState CurrentState { private set; get; }
        public int CommentDepth { private set; get; } = 0;

        public List<Rule> Rules { get; }
        public string CurrentString { get; private set; }
        public int Value { get; }
        public int LineNr { get; private set; } = 1;

        private void BeginState(LexState state)
        {
            CurrentState = state;
        }

        private Token EmptyToken() => null;

        public Lexer()
        {
            Rules = new List<Rule>
            {
                new Rule
                {
                    State = new []{LexState.yycomment, LexState.yyinitial},
                    Regex = new Regex(@"\(\*"),
                    CreateToken = s => { BeginState(LexState.yycomment); CommentDepth++; return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yycomment},
                    Regex = new Regex(@"\*\)"),
                    CreateToken = s => {CommentDepth--; BeginState(CommentDepth == 0 ? LexState.yyinitial : CurrentState); return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\*\)"),
                    CreateToken = s => new ErrorToken("Unmatched *)")
                },
                new Rule
                {
                    State = new []{LexState.yycomment},
                    Regex = new Regex(@".|\r"),
                    CreateToken = s => EmptyToken()
                },
                new Rule
                {
                    State = new []{ LexState.yyinitial},
                    Regex = new Regex(@"[ \t\r\f\]"),
                    CreateToken = s => EmptyToken()
                },
                new Rule
                {
                    State = new []{ LexState.yyinitial},
                    Regex = new Regex(@"--[^\n]*"),
                    CreateToken = s => EmptyToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial },
                    Regex = new Regex(@"else", RegexOptions.IgnoreCase),
                    CreateToken = s => new ElseToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"if", RegexOptions.IgnoreCase),
                    CreateToken = s => new IfToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"fi", RegexOptions.IgnoreCase),
                    CreateToken = s => new FiToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"in", RegexOptions.IgnoreCase),
                    CreateToken = s => new InToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"inherits", RegexOptions.IgnoreCase),
                    CreateToken = s => new InheritsToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"isvoid", RegexOptions.IgnoreCase),
                    CreateToken = s => new IsVoidToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"let", RegexOptions.IgnoreCase),
                    CreateToken = s => new LetToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"loop", RegexOptions.IgnoreCase),
                    CreateToken = s => new LoopToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"pool", RegexOptions.IgnoreCase),
                    CreateToken = s => new PoolToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"then", RegexOptions.IgnoreCase),
                    CreateToken = s => new ThenToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"while", RegexOptions.IgnoreCase),
                    CreateToken = s => new WhileToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"case", RegexOptions.IgnoreCase),
                    CreateToken = s => new CaseToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"esac", RegexOptions.IgnoreCase),
                    CreateToken = s => new EsacToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"new", RegexOptions.IgnoreCase),
                    CreateToken = s => new NewToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"of", RegexOptions.IgnoreCase),
                    CreateToken = s => new OfToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"not", RegexOptions.IgnoreCase),
                    CreateToken = s => new NotToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"class", RegexOptions.IgnoreCase),
                    CreateToken = s => new ClassToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"t[Rr][Uu][Ee]"),
                    CreateToken = s => new BoolConstToken{ Value = true }
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"f[Aa][Ll][Ss][Ee]"),
                    CreateToken = s => new BoolConstToken{ Value = false }
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\*"),
                    CreateToken = s => new OperatorToken {OperatorType = OperatorType.multiplication}
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\."),
                    CreateToken = s => new DotToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@";"),
                    CreateToken = s => new SemicolonToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\/"),
                    CreateToken = s => new OperatorToken{OperatorType = OperatorType.divide}
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\\+"),
                    CreateToken = s => new OperatorToken{OperatorType = OperatorType.plus}
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"-"),
                    CreateToken = s => new OperatorToken{OperatorType = OperatorType.minus}
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex("~"),
                    CreateToken = s => new TildeToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\("),
                    CreateToken = s => new LeftParentToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\)"),
                    CreateToken = s => new RightParentToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\<"),
                    CreateToken = s => new OperatorToken(){OperatorType=OperatorType.lessThan}
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\<="),
                    CreateToken = s => new OperatorToken(){OperatorType=OperatorType.lessThanOrEquel}
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@","),
                    CreateToken = s => new CommaToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"="),
                    CreateToken = s => new OperatorToken(){OperatorType = OperatorType.equels}
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\<-"),
                    CreateToken = s => new OperatorToken(){OperatorType=OperatorType.assigin}
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\:"),
                    CreateToken = s => new ColonToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\{"),
                    CreateToken = s => new LeftCurlyBraceToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\}"),
                    CreateToken = s => new RightCurlyBraceToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"@"),
                    CreateToken = s => new AtToken()
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"[A-Z][A-z0-9_]*"),
                    CreateToken = s => new TypeIdentifierToken(){Value = s}
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"[a-z][A-z0-9_]*"),
                    CreateToken = s => new ObjectIdentifierToken(){ Value = s }
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex("\""), // start string
                    CreateToken = s => {
                        BeginState(LexState.yystring);
                        CurrentString = string.Empty;
                        return EmptyToken();
                    }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex(@"\x00"),
                    CreateToken = s => { BeginState(LexState.yystringNullErr); return new ErrorToken("String contains null character"); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex(@"\\b"),
                    CreateToken = s => { CurrentString += "\b"; return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex(@"\\f"),
                    CreateToken = s => { CurrentString += "\f"; return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex(@"\\t"),
                    CreateToken = s => { CurrentString += "\t"; return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex(@"\\\\n"),
                    CreateToken = s => { CurrentString += "\\n"; return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex(@"\\n"),
                    CreateToken = s => { CurrentString += "\n"; return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex(@"\\\n"),
                    CreateToken = s => { CurrentString += "\n"; return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex("\\\\\""),
                    CreateToken = s => { CurrentString += "\""; return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex(@"\\\\"),
                    CreateToken = s => { CurrentString += "\\"; return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex(@"\\"),
                    CreateToken = s => { return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex("[^\"\0\n\\\\]+"),// all legal sting chars
                    CreateToken = s => {CurrentString += s; return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex(@"\n"),// Not legal to put a string on multiple lines
                    CreateToken = s => {
                        CurrentString += string.Empty;
                        BeginState(LexState.yyinitial);
                        return new ErrorToken("Unterminated string constant");
                    }
                },
                new Rule
                {
                    State = new []{LexState.yystring},
                    Regex = new Regex("\""),// end of the string
                    CreateToken = s => {
                        BeginState(LexState.yyinitial);

                        var token = new StringConstantToken{ Value = CurrentString};
                        CurrentString = string.Empty;
                        return token;
                    }
                },
                //recover after null char
                new Rule
                {
                    State = new []{LexState.yystringNullErr},
                    Regex = new Regex(@"\n"),
                    CreateToken = s => {BeginState(LexState.yyinitial); return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystringNullErr},
                    Regex = new Regex("\""),
                    CreateToken = s => {BeginState(LexState.yyinitial); return EmptyToken(); }
                },
                new Rule
                {
                    State = new []{LexState.yystringNullErr},
                    Regex = new Regex("."),
                    CreateToken = s => { return EmptyToken(); }
                },
                // end recovery

                new Rule
                {
                    State = new LexState[]{},
                    Regex = new Regex("\n"),
                    CreateToken = s => { LineNr++; return EmptyToken(); }
                },

                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex("[0-9]+"),
                    CreateToken = s => { return new IntegerToken { Value = int.Parse(s) }; }
                },
                new Rule
                {
                    State = new []{LexState.yyinitial},
                    Regex = new Regex(@"\=\>"),
                    CreateToken = s => { return new DoubleArrowToken(); }
                },
            };
        }
    }
}


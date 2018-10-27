using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolCompilerCSharp.Lexer
{
    public abstract class Token
    {
        public int Line { get; set; }
        public int Col { get; set; }
    }

    public enum OperatorType
    {
        multiplication,
        minus,
        lessThan,
        plus,
        assigin,
        lessThanOrEquel,
        divide,
        equels
    }

    public class OperatorToken : Token
    {
        public OperatorType OperatorType { get; set; }
    }

    public class InheritsToken : Token { }

    public class PoolToken : Token { }

    public class CaseToken : Token { }

    public class LeftParentToken : Token { }

    public class RightParentToken : Token { }

    public class SemicolonToken : Token { }

    public class NotToken : Token { }

    public class TypeIdentifierToken: Token
    {
        public string Value { get; set; }
    }

    public class InToken: Token { }
    public class CommaToken: Token { }
    public class ClassToken : Token { }
    public class FiToken : Token { }
    public class LoopToken : Token { }
    public class IfToken : Token { }
    public class DotToken : Token { }
    public class OfToken : Token { }
    public class EOFToken : Token { }
    public class IntegerToken : Token
    {
        public int Value { get; set; }
    }
    public class NewToken : Token { }
    public class IsVoidToken : Token { }
    public class ColonToken : Token { }
    public class TildeToken : Token { }
    public class LeftCurlyBraceToken : Token { }
    public class RightCurlyBraceToken : Token { }
    public class ElseToken : Token { }

    /// <summary>
    ///  =>
    /// </summary>
    public class DoubleArrowToken: Token { }

    public class WhileToken : Token { }
    public class EsacToken : Token { }
    public class LetToken : Token { }
    public class ThenToken : Token { }
    public class BoolConstToken : Token
    {
        public bool Value { get; set; }
    }

    public class ObjectIdentifierToken : Token
    {
        public string Value { get; set; }
    }
    /// <summary>
    /// @
    /// </summary>
    public class AtToken : Token { }

    public class ErrorToken : Token
    {
        public ErrorToken(string error)
        {
            this.Message = error;
        }

        public string Message { get; }
    }

    public class StringConstantToken: Token
    {
        public string Value { get; set; }
    }


}

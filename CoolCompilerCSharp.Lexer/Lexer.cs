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

        public IEnumerable<Token> Lex(string filePath)
        {
            var tokens = this.lexFile(filePath);

            return tokens.Where(x => x != null);
        }

        private IEnumerable<Token> lexFile(string filePath)
        {
            var file = System.IO.File.ReadAllText(filePath);

            var result = new List<Token>();

            while (file != string.Empty)
            {
                var tokenMatch = this.Rules.Select(rule =>
                    {
                        if (rule.State.Length > 0 && !rule.State.Contains(CurrentState))
                            return null;

                        
                        var match = new Regex("^" + rule.Regex.ToString()).Match(file, 0);
                        if (match.Success)
                        {
                            return new TokenMatch
                            {
                                Rule = rule,
                                Value = match.Value
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                    )
                    .Where(x => x != null)
                    .OrderByDescending(match => match.Value.Length)
                    .First();
                var token = tokenMatch.Rule.CreateToken(tokenMatch.Value);
                if(token!= null)
                {
                    token.Line = LineNr;
                }
                result.Add( token );
                file = file.Substring(tokenMatch.Value.Length);

            }

            return result;
            
        }

    }
}

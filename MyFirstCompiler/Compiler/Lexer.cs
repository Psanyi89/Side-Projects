using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostic = new List<string>();
        public Lexer(string text)
        {
         
            _text = text;
        }
        public IEnumerable<string> Diagnostic
        {
            get { return _diagnostic; }
        }
        private char Current
        {
            get
            {
                if (_position>=_text.Length)
                {
                    return '\0';
                }
                return _text[_position];
            }
        }

        public void Next()
        {
            _position++;
        }

        public SyntaxToken NewToken()
        {
            // <numbers>
            // + - * /
            // whitespace

            if (_position >= _text.Length)
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
            }

            if (char.IsDigit(Current))
            {
                var start = _position;
                while (char.IsDigit(Current))
                {
                    Next();
                }

                var lenght = _position-start;
                var text = _text.Substring(start, lenght);
               if(! int.TryParse(text, out var value))
                {
                    _diagnostic.Add($"Error: The number {_text} isn't a valid int32");
                }
                return new SyntaxToken(SyntaxKind.NumberToken, start, text,value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;
                while (char.IsWhiteSpace(Current))
                {
                    Next();
                }

                var lenght = _position - start;
                var text = _text.Substring(start, lenght);

                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, " ");
            }
            if (Current == '+')
            {
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
            }
            else if (Current == '-')
            {
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
            }
            else if (Current == '*')
            {
                return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
            }
            else if (Current == '/')
            {
                return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
            }
            else if (Current == '(')
            {
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
            }
            else if (Current == ')')
            {
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);
            }
            _diagnostic.Add($"Error: Bad character in input: {Current} ");
            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }
    }
}

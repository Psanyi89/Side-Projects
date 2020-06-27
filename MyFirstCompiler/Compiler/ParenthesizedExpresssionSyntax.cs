using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public sealed class ParenthesizedExpresssionSyntax : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpresssion;

        public SyntaxToken OpenParenthesisToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken CloseParenthesisToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expression;
            yield return CloseParenthesisToken;

        }

        public ParenthesizedExpresssionSyntax(SyntaxToken openParenthesisToken, ExpressionSyntax expression,
           SyntaxToken closeParenthesisToken )
        {
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenthesisToken;
        }
    }
}

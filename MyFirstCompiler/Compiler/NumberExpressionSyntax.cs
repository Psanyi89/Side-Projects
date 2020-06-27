using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    sealed class NumberExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public SyntaxToken NumberToken { get; }

        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        } 
    }
}

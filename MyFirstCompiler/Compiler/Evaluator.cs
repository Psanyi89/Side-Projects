using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class Evaluator
    {
        private readonly ExpressionSyntax root;

        public Evaluator(ExpressionSyntax root)
        {
            this.root = root;
        }
        public int Evaluate()
        {
            return EvaluateExpression(root);
        }

        private int EvaluateExpression(ExpressionSyntax root)
        {
            //BinaryExpression
            //NumberExpression

            if (root is NumberExpressionSyntax n)
            {
                return (int)n.NumberToken.Value;
            }
            if (root is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);
                if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
                {
                    return left + right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
                {
                    return left - right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.StarToken)
                {
                    return left * right;
                }
                else if (b.OperatorToken.Kind == SyntaxKind.SlashToken)
                {
                    return left / right;
                }
                else throw new Exception($"Unexcepted binary operator {b.OperatorToken.Kind}");
            }
            if (root is ParenthesizedExpresssionSyntax p)
            {
                return EvaluateExpression(p.Expression);
            }
            throw new Exception($"Unexcepted node {root.Kind}");
        }
    }
}

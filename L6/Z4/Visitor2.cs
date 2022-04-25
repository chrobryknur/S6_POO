using System;
using System.Linq.Expressions;

namespace Visitor2
{
    public class PrintExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression expression)
        {
            Console.WriteLine("{0} {1} {2}",
                expression.Left, expression.NodeType, expression.Right);
            return base.VisitBinary(expression);
        }
        protected override Expression VisitLambda<T>(Expression<T> expression)
        {
            Console.WriteLine("{0} -> {1}",
                expression.Parameters.Aggregate(string.Empty, (a, e) => a += e),
                expression.Body);
            return base.VisitLambda<T>(expression);
        }

        protected override Expression VisitUnary(UnaryExpression expression)
        {
            Console.WriteLine("Unary: {0}",
                expression.ToString());
            return base.VisitUnary(expression);
        }

        protected override Expression VisitParameter(ParameterExpression expression)
        {
            Console.WriteLine("Parameter: {0} {1}",
                expression.Name, expression.IsByRef);
            return base.VisitParameter(expression);
        }
        protected override Expression VisitTry(TryExpression expression)
        {
            Console.WriteLine("Try: {0} {1} {2}",
                expression.Body, expression.Fault, expression.Finally);
            return base.VisitTry(expression);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using Visitor2;

namespace Visitor2Test
{
    [TestClass]
    public class Visitor2Test
    {
        [TestMethod]
        public void Unary_visitor()
        {
            PrintExpressionVisitor visitor = new();
            UnaryExpression negate = Expression.Negate(Expression.Constant(20));
            visitor.Visit(negate);
        }

        [TestMethod]
        public void Parameter_visitor()
        {
            PrintExpressionVisitor visitor = new();
            ParameterExpression parameter = Expression.Parameter(typeof(int));
            visitor.Visit(parameter);
        }

        [TestMethod]
        public void Try_visitor()
        {
            PrintExpressionVisitor visitor = new();
            UnaryExpression negate20 = Expression.Negate(Expression.Constant(20));
            UnaryExpression negate30 = Expression.Negate(Expression.Constant(30));
            TryExpression tryExpression = Expression.TryCatchFinally(negate20, negate30);
            visitor.Visit(tryExpression);
        }
    }
}
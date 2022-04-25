using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpreter;
using System;

namespace InterpreterTest
{
    [TestClass]
    public class InterpreterTest
    {
        [TestMethod]
        public void True_const_expression_should_be_interpreted_as_true()
        {
            Context context = new();
            AbstractExpression expression = new ConstExpression(true);
            Assert.IsTrue(expression.Interpret(context));
        }

        [TestMethod]
        public void False_const_expression_should_be_interpreted_as_false()
        {
            Context context = new();
            AbstractExpression expression = new ConstExpression(false);
            Assert.IsFalse(expression.Interpret(context));
        }

        [TestMethod]
        public void Variable_valuated_as_true_should_be_interpreted_as_true()
        {
            Context context = new();
            AbstractExpression expression = new Variable("x");
            Assert.IsTrue(context.SetValue("x", true));
            Assert.IsTrue(expression.Interpret(context));
        }

        [TestMethod]
        public void Variable_valuated_as_false_should_be_interpreted_as_false()
        {
            Context context = new();
            AbstractExpression expression = new Variable("x");
            Assert.IsTrue(context.SetValue("x", false));
            Assert.IsFalse(expression.Interpret(context));
        }

        [TestMethod]
        public void Variable_not_valuated_should_throw_exception_while_interpreting()
        {
            Context context = new();
            AbstractExpression expression = new Variable("x");
            Assert.ThrowsException<Exception>(() =>
            {
                expression.Interpret(context);
            });
        }

        [TestMethod]
        public void Negation_of_variable_valuated_as_false_should_be_interpreted_as_true()
        {
            Context context = new();
            AbstractExpression variable = new Variable("x");
            Assert.IsTrue(context.SetValue("x", false));
            AbstractExpression expression = new NegationExpression(variable);
            Assert.IsTrue(expression.Interpret(context));
        }

        [TestMethod]
        public void Negation_of_variable_valuated_as_true_should_be_interpreted_as_false()
        {
            Context context = new();
            AbstractExpression variable = new Variable("x");
            Assert.IsTrue(context.SetValue("x", true));
            AbstractExpression expression = new NegationExpression(variable);
            Assert.IsFalse(expression.Interpret(context));
        }

        [TestMethod]
        public void Disjunction_of_true_and_false_should_return_true()
        {
            Context context = new();
            AbstractExpression lhs = new ConstExpression(true);
            AbstractExpression rhs = new ConstExpression(false);
            AbstractExpression disjunction = new DisjunctionExpression(lhs, rhs);
            Assert.IsTrue(disjunction.Interpret(context));
        }

        [TestMethod]
        public void Disjunction_of_false_and_false_should_return_false()
        {
            Context context = new();
            AbstractExpression lhs = new ConstExpression(false);
            AbstractExpression rhs = new ConstExpression(false);
            AbstractExpression disjunction = new DisjunctionExpression(lhs, rhs);
            Assert.IsFalse(disjunction.Interpret(context));
        }

        [TestMethod]
        public void Conjunction_of_true_and_true_should_return_true()
        {
            Context context = new();
            AbstractExpression lhs = new ConstExpression(true);
            AbstractExpression rhs = new ConstExpression(true);
            AbstractExpression conjunction = new ConjunctionExpression(lhs, rhs);
            Assert.IsTrue(conjunction.Interpret(context));
        }

        [TestMethod]
        public void Conjunction_of_true_and_false_should_return_false()
        {
            Context context = new();
            AbstractExpression lhs = new ConstExpression(true);
            AbstractExpression rhs = new ConstExpression(false);
            AbstractExpression conjunction = new ConjunctionExpression(lhs, rhs);
            Assert.IsFalse(conjunction.Interpret(context));
        }

    }
}
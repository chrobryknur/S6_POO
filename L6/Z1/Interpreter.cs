using System;
using System.Collections.Generic;

namespace Interpreter
{
    public class Context
    {
        public bool GetValue(string VariableName)
        {
            if (valuation.TryGetValue(VariableName, out bool value))
            {
                return value;
            }
            throw new Exception("Variable " + VariableName + "value is not defined");
        }
        public bool SetValue(string VariableName, bool Value)
        {
            if(valuation.ContainsKey(VariableName))
            {
                return false;
            }
            valuation.Add(VariableName, Value);
            return true;
        }

        private Dictionary<string, bool> valuation = new();
    }

    public abstract class AbstractExpression
    {
        public abstract bool Interpret(Context context);
    }

    public class Variable : AbstractExpression
    {
        public Variable(string Name)
        {
            name = Name;
        }

        public override bool Interpret(Context context)
        {
            return context.GetValue(name);
        }

        private string name;
    }
    public class ConstExpression : AbstractExpression
    {
        public ConstExpression(bool Value)
        {
            value = Value;
        }

        public override bool Interpret(Context context)
        {
            return value;
        }

        private bool value;
    }
    public class BinaryExpression : AbstractExpression
    {
        public override bool Interpret(Context context)
        {
            switch(Operator)
            {
                case "+":
                {
                    return lhs.Interpret(context) || rhs.Interpret(context);
                }
                case "*":
                {
                    return lhs.Interpret(context) && rhs.Interpret(context);
                }
                default:
                {
                    throw new Exception("Undefined operator");
                }
            }
        }

        protected BinaryExpression(AbstractExpression lhs, AbstractExpression rhs, string Operator)
        {
            this.lhs = lhs; 
            this.rhs = rhs;
            this.Operator = Operator;
        }

        protected AbstractExpression lhs;
        protected AbstractExpression rhs;
        protected string Operator;
    }

    public class DisjunctionExpression : BinaryExpression
    {
        public DisjunctionExpression(AbstractExpression lhs, AbstractExpression rhs) : base(lhs, rhs, "+"){}
    }
    public class ConjunctionExpression : BinaryExpression
    {
        public ConjunctionExpression(AbstractExpression lhs, AbstractExpression rhs) : base(lhs, rhs, "*"){}
    }

    public class UnaryExpression : AbstractExpression
    {
        public override bool Interpret(Context context)
        {
            switch(Operator)
            {
                case "!":
                {
                    return !expression.Interpret(context);
                }
                default:
                {
                    throw new Exception("Operator undefined");
                }
            }
        }
        protected UnaryExpression(AbstractExpression expression, string Operator)
        {
            this.expression = expression;
            this.Operator = Operator;
        }

        protected AbstractExpression expression;
        protected string Operator;
    }

    public class NegationExpression : UnaryExpression
    {
        public NegationExpression(AbstractExpression expression) : base(expression, "!"){}
    }
}

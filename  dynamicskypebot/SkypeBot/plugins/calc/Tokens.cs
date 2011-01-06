using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bsn.GoldParser.Semantic;
using SkypeBot.plugins.calc;
using System.Globalization;
using log4net;

[assembly: RuleTrim("<Value> ::= '(' <Expression> ')'", "<Expression>", SemanticTokenType = typeof(MathToken))]
[assembly: RuleTrim("<Sign Exp> ::= '+' <Add Exp>", "<Add Exp>", SemanticTokenType = typeof(MathToken))]

namespace SkypeBot.plugins.calc {
    public static class RandomClass {
        public static readonly Random rand = new Random();
    }

    public enum ValueType {
        Integer, Double, Indifferent
    }

    [Terminal("(EOF)")]
    [Terminal("(Error)")]
    [Terminal("(Whitespace)")]
    [Terminal("(")]
    [Terminal(")")]
    [Terminal(",")]
    public class MathToken : SemanticToken {}

    // Operators:
    public abstract class Operator : MathToken {
        public abstract double CalculateDouble(double left, double right);
        public abstract int CalculateInt(int left, int right);
    }

    [Terminal("+")]
    public class OperatorPlus : Operator {
        public override double CalculateDouble(double left, double right) {
            return left + right;
        }

        public override int CalculateInt(int left, int right) {
            return left + right;
        }
    }

    [Terminal("-")]
    public class OperatorMinus : Operator {
        public override double CalculateDouble(double left, double right) {
            return left - right;
        }

        public override int CalculateInt(int left, int right) {
            return left - right;
        }
    }

    [Terminal("*")]
    public class OperatorTimes : Operator {
        public override double CalculateDouble(double left, double right) {
            return left * right;
        }

        public override int CalculateInt(int left, int right) {
            return left * right;
        }
    }

    [Terminal("/")]
    public class OperatorDivide : Operator {
        public override double CalculateDouble(double left, double right) {
            return left / right;
        }

        public override int CalculateInt(int left, int right) {
            return left / right;
        }
    }

    [Terminal("^")]
    public class OperatorPower : Operator {
        public override double CalculateDouble(double left, double right) {
            return Math.Pow(left, right);
        }

        // Yanked from http://stackoverflow.com/questions/383587/how-do-you-do-integer-exponentiation-in-c
        public override int CalculateInt(int x, int pow) {
            int ret = 1;
            while (pow != 0) {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }
    }

    public abstract class UnaryFunction : MathToken {
        public ValueType type;
        public abstract int EvalInteger(int value);
        public abstract double EvalDouble(double value);
    }

    [Terminal("cos")]
    public class CosFunction : UnaryFunction {
        public CosFunction() {
            this.type = ValueType.Double;
        }

        public override double EvalDouble(double value) {
            return Math.Cos(value);
        }

        public override int EvalInteger(int value) {
            return (int)EvalDouble((double)value);
        }
    }

    [Terminal("sin")]
    public class SinFunction : UnaryFunction {
        public SinFunction() {
            this.type = ValueType.Double;
        }

        public override double EvalDouble(double value) {
            return Math.Sin(value);
        }

        public override int EvalInteger(int value) {
            return (int)EvalDouble((double)value);
        }
    }

    [Terminal("tan")]
    public class TanFunction : UnaryFunction {
        public TanFunction() {
            this.type = ValueType.Double;
        }

        public override double EvalDouble(double value) {
            return Math.Tan(value);
        }

        public override int EvalInteger(int value) {
            return (int)EvalDouble((double)value);
        }
    }

    [Terminal("sqrt")]
    public class SqrtFunction : UnaryFunction {
        public SqrtFunction() {
            this.type = ValueType.Double;
        }

        public override double EvalDouble(double value) {
            return Math.Sqrt(value);
        }

        public override int EvalInteger(int value) {
            return (int)EvalDouble((double)value);
        }
    }

    [Terminal("exp")]
    public class ExpFunction : UnaryFunction {
        public ExpFunction() {
            this.type = ValueType.Double;
        }

        public override double EvalDouble(double value) {
            return Math.Exp(value);
        }

        public override int EvalInteger(int value) {
            return (int)EvalDouble((double)value);
        }
    }

    [Terminal("log")]
    public class LogFunction : UnaryFunction {
        public LogFunction() {
            this.type = ValueType.Double;
        }

        public override double EvalDouble(double value) {
            return Math.Log(value);
        }

        public override int EvalInteger(int value) {
            return (int)EvalDouble((double)value);
        }
    }

    public abstract class BinaryFunction : MathToken {
        public ValueType type;
        public abstract int EvalInteger(int param1, int param2);
        public abstract double EvalDouble(double param1, double param2);
    }

    [Terminal("random")]
    public class RandomFunction : BinaryFunction {
        public RandomFunction() {
            this.type = ValueType.Integer;
        }

        public override double EvalDouble(double param1, double param2) {
            return (double)EvalInteger((int)param1, (int)param2);
        }

        public override int EvalInteger(int param1, int param2) {
            return RandomClass.rand.Next(param1, param2);
        }
    }

    public abstract class Computable : MathToken {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ValueType type;
        public abstract int GetInteger();
        public abstract double GetDouble();
        public String GetValue() {
            log.Debug(type);
            if (type == ValueType.Double) {
                return GetDouble().ToString("G");
            } else {
                return GetInteger().ToString();
            }
        }
    }

    [Terminal("Integer")]
    public class IntegerValue : Computable {
        private readonly int value;

        public IntegerValue(string value) {
            this.type = ValueType.Integer;
            this.value = int.Parse(value);
        }

        public override int GetInteger() {
            return value;
        }

        public override double GetDouble() {
            return (double)value;
        }
    }

    [Terminal("Float")]
    public class DoubleValue : Computable {
        private readonly double value;

        public DoubleValue(string value) {
            this.type = ValueType.Double;
            this.value = Double.Parse(value, NumberFormatInfo.InvariantInfo);
        }

        public override int GetInteger() {
            return (int)Math.Round(value);
        }

        public override double GetDouble() {
            return (double)value;
        }
    }

    [Terminal("e")]
    public class EConstant : Computable {
        public EConstant() {
            this.type = ValueType.Double;
        }

        public override int GetInteger() {
            return (int)Math.Round(Math.E);
        }

        public override double GetDouble() {
            return Math.E;
        }
    }

    [Terminal("pi")]
    public class PiConstant : Computable {
        public PiConstant() {
            this.type = ValueType.Double;
        }

        public override int GetInteger() {
            return (int)Math.Round(Math.PI);
        }

        public override double GetDouble() {
            return Math.PI;
        }
    }

    public class Operation : Computable {
        private readonly Computable left;
        private readonly Operator op;
        private readonly Computable right;

        [Rule(@"<Add Exp> ::= <Add Exp> '+' <Mult Exp>")]
        [Rule(@"<Add Exp> ::= <Add Exp> '-' <Mult Exp>")]
        [Rule(@"<Mult Exp> ::= <Mult Exp> '*' <Pow Exp>")]
        [Rule(@"<Mult Exp> ::= <Mult Exp> '/' <Pow Exp>")]
        [Rule(@"<Pow Exp> ::= <Func Exp> '^' <Pow Exp>")]
        public Operation(Computable left, Operator op, Computable right) {
            this.left = left;
            this.op = op;
            this.right = right;
            this.type = left.type == ValueType.Double || right.type == ValueType.Double
                            ? ValueType.Double : ValueType.Integer;
        }

        public override double GetDouble() {
            return op.CalculateDouble(left.GetDouble(), right.GetDouble());
        }

        public override int GetInteger() {
            return op.CalculateInt(left.GetInteger(), right.GetInteger());
        }
    }

    public class UnaryFunctionCall : Computable {
        private readonly UnaryFunction f;
        private readonly Computable x;

        [Rule(@"<Func Exp> ::= <Unary Func> <Func Exp>")]
        public UnaryFunctionCall(UnaryFunction f, Computable x) {
            this.f = f;
            this.x = x;

            this.type = f.type == ValueType.Indifferent ? x.type : f.type;
        }

        public override double GetDouble() {
            return f.EvalDouble(x.GetDouble());
        }

        public override int GetInteger() {
            return f.EvalInteger(x.GetInteger());
        }
    }

    public class BinaryFunctionCall : Computable {
        private readonly BinaryFunction f;
        private readonly Computable x, y;

        [Rule(@"<Func Exp> ::= <Binary Func> ~'(' <Expression> ~',' <Expression> ~')'")]
        public BinaryFunctionCall(BinaryFunction f, Computable x, Computable y) {
            this.f = f;
            this.x = x;
            this.y = y;

            this.type = f.type != ValueType.Indifferent ? f.type :
                       (x.type == ValueType.Double || y.type == ValueType.Double ? ValueType.Double : ValueType.Integer);
        }

        public override double GetDouble() {
            return f.EvalDouble(x.GetDouble(), y.GetDouble());
        }

        public override int GetInteger() {
            return f.EvalInteger(x.GetInteger(), y.GetInteger());
        }
    }

    public class SignMinus : Computable {
        private readonly Computable comp;

        [Rule(@"<Sign Exp> ::= ~'-' <Add Exp>")]
        public SignMinus(Computable comp) {
            this.type = comp.type;
            this.comp = comp;
        }

        public override double GetDouble() {
            return -comp.GetDouble();
        }

        public override int GetInteger() {
            return -comp.GetInteger();
        }
    }
}

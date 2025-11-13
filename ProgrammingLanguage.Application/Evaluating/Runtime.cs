using ProgrammingLanguage.Application.Parsing;
using ProgrammingLanguage.Shared.Helpers;
using static System.Math;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Runtime
{
	private Class Global = default!;

	public Runtime()
	{
		Scope scope = new("@System");
		Range<Position> range = ~Position.Zero;

		Module module = new("@System", scope);
		{
			Class typeType = module.RegisterClass("Type", typeof(Type), range);
			{
			}
			Class typeFunction = module.RegisterClass("Function", typeof(OperationContent), range);
			{
			}
			Class typeNumber = module.RegisterClass("Number", typeof(double), range);
			{
				Operator opAdd = typeNumber.RegisterOperator("+", range);
				opAdd.RegisterOperation(["Number"], "Number", NumberPositive, range);
				opAdd.RegisterOperation(["Number", "Number"], "Number", NumberAdd, range);

				Operator opSub = typeNumber.RegisterOperator("-", range);
				opSub.RegisterOperation(["Number"], "Number", NumberNegate, range);
				opSub.RegisterOperation(["Number", "Number"], "Number", NumberSubtract, range);

				Operator opMul = typeNumber.RegisterOperator("*", range);
				opMul.RegisterOperation(["Number", "Number"], "Number", NumberMultiply, range);

				Operator opDiv = typeNumber.RegisterOperator("/", range);
				opDiv.RegisterOperation(["Number", "Number"], "Number", NumberDivide, range);

				Operator opEq = typeNumber.RegisterOperator("=", range);
				opEq.RegisterOperation(["Number", "Number"], "Boolean", NumberEquals, range);

				Operator opLess = typeNumber.RegisterOperator("<", range);
				opLess.RegisterOperation(["Number", "Number"], "Boolean", NumberLessThan, range);

				Operator opLessEq = typeNumber.RegisterOperator("<=", range);
				opLessEq.RegisterOperation(["Number", "Number"], "Boolean", NumberLessThanOrEqual, range);

				Operator opGreat = typeNumber.RegisterOperator(">", range);
				opGreat.RegisterOperation(["Number", "Number"], "Boolean", NumberGreaterThan, range);

				Operator opGreatEq = typeNumber.RegisterOperator(">=", range);
				opGreatEq.RegisterOperation(["Number", "Number"], "Boolean", NumberGreaterThanOrEqual, range);
			}
			Class typeBoolean = module.RegisterClass("Boolean", typeof(bool), range);
			{
			}
			Class typeString = module.RegisterClass("String", typeof(string), range);
			{
			}
			Class typeGlobal = module.RegisterClass("@Developer", typeof(Type), range);
			{
				typeGlobal.RegisterConstant("pi", "Number", PI, range);
				typeGlobal.RegisterConstant("e", "Number", E, range);

				Operator opWrite = typeGlobal.RegisterOperator("write", range);
				opWrite.RegisterOperation(["Number"], "Number", Write, range);
				opWrite.RegisterOperation(["Boolean"], "Number", Write, range);
				opWrite.RegisterOperation(["String"], "Number", Write, range);
			}
			Global = typeGlobal;
		}
	}

	public void Evaluate(IEnumerable<Node> trees)
	{
		Scope scope = new("@Main", Global.Scope);
		Evaluator evaluator = new();
		foreach (Node tree in trees) tree.Accept(evaluator, scope);
	}

	private static ValueNode NumberAdd(Scope location, ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Number", left + right, range);
	}

	private static ValueNode NumberSubtract(Scope location, ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Number", left - right, range);
	}

	private static ValueNode NumberMultiply(Scope location, ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Number", left * right, range);
	}

	private static ValueNode NumberDivide(Scope location, ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Number", left / right, range);
	}

	private static ValueNode NumberPositive(Scope location, ValueNode[] args, Range<Position> range)
	{
		double target = args[0].ValueAs<double>();
		return new ValueNode("Number", +target, range);
	}

	private static ValueNode NumberNegate(Scope location, ValueNode[] args, Range<Position> range)
	{
		double target = args[0].ValueAs<double>();
		return new ValueNode("Number", -target, range);
	}

	private static ValueNode NumberEquals(Scope location, ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Boolean", left == right, range);
	}

	private static ValueNode NumberLessThan(Scope location, ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Boolean", left < right, range);
	}

	private static ValueNode NumberLessThanOrEqual(Scope location, ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Boolean", left <= right, range);
	}

	private static ValueNode NumberGreaterThan(Scope location, ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Boolean", left > right, range);
	}

	private static ValueNode NumberGreaterThanOrEqual(Scope location, ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Boolean", left >= right, range);
	}

	private static ValueNode Write(Scope scope, ValueNode[] args, Range<Position> range)
	{
		Console.WriteLine(args[0].ToString());
		return ValueNode.NullableAt("Number", range);
	}
}

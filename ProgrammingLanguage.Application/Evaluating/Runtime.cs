using ProgrammingLanguage.Application.Parsing;
using ProgrammingLanguage.Shared.Helpers;
using static System.Math;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Runtime
{
	private readonly Module Module = new();
	private readonly Structure Global;

	public Runtime()
	{
		Global = Module.RegisterType("@Global", typeof(Type), ~Position.Zero);
		ImportCore(~Position.Zero);
	}

	public void Evaluate(IEnumerable<Node> trees)
	{
		Evaluator executor = new(Module);
		foreach (Node tree in trees) tree.Accept(executor);
	}

	private void ImportCore(Range<Position> range)
	{
		Module.RegisterType("Type", typeof(Type), range);
		Module.RegisterType("Function", typeof(OperationContent), range);

		ImportNumber(range);
		ImportBoolean(range);
		ImportString(range);
		ImportMath(range);
		ImportInOut(range);
	}

	private void ImportNumber(Range<Position> range)
	{
		Structure typeNumber = Module.RegisterType("Number", typeof(double), range);

		typeNumber.RegisterOperation("+", [new("left", "Number"), new("right", "Number")], "Number", NumberPlus, range);
		typeNumber.RegisterOperation("-", [new("left", "Number"), new("right", "Number")], "Number", NumberMinus, range);
		typeNumber.RegisterOperation("*", [new("left", "Number"), new("right", "Number")], "Number", NumberMultiplication, range);
		typeNumber.RegisterOperation("/", [new("left", "Number"), new("right", "Number")], "Number", NumberDivision, range);

		typeNumber.RegisterOperation("+", [new("target", "Number")], "Number", NumberUnaryPlus, range);
		typeNumber.RegisterOperation("-", [new("target", "Number")], "Number", NumberUnaryMinus, range);

		typeNumber.RegisterOperation("=", [new("left", "Number"), new("right", "Number")], "Boolean", NumberEqual, range);
		typeNumber.RegisterOperation("<", [new("left", "Number"), new("right", "Number")], "Boolean", NumberLess, range);
		typeNumber.RegisterOperation("<=", [new("left", "Number"), new("right", "Number")], "Boolean", NumberLessOrEqual, range);
		typeNumber.RegisterOperation(">", [new("left", "Number"), new("right", "Number")], "Boolean", NumberGreater, range);
		typeNumber.RegisterOperation(">=", [new("left", "Number"), new("right", "Number")], "Boolean", NumberGreaterOrEqual, range);
	}

	private void ImportBoolean(Range<Position> range)
	{
		Module.RegisterType("Boolean", typeof(bool), range);
	}

	private void ImportString(Range<Position> range)
	{
		Module.RegisterType("String", typeof(string), range);
	}

	private void ImportMath(Range<Position> range)
	{
		Global.RegisterConstant("pi", "Number", PI, range);
		Global.RegisterConstant("e", "Number", E, range);
	}

	private void ImportInOut(Range<Position> range)
	{
		Global.RegisterOperation("write", [new("target", "Number")], "Number", Write, range);
	}

	private static ValueNode NumberPlus(ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Number", left + right, range);
	}

	private static ValueNode NumberMinus(ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Number", left - right, range);
	}

	private static ValueNode NumberMultiplication(ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Number", left * right, range);
	}

	private static ValueNode NumberDivision(ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		// if (right == 0) throw new DivisionByZeroIssue(args[1].RangePosition);
		return new ValueNode("Number", left / right, range);
	}

	private static ValueNode NumberUnaryPlus(ValueNode[] args, Range<Position> range) => args[0];
	private static ValueNode NumberUnaryMinus(ValueNode[] args, Range<Position> range)
	{
		double target = args[0].ValueAs<double>();
		return new ValueNode("Number", -target, range);
	}

	private static ValueNode NumberEqual(ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Boolean", left == right, range);
	}

	private static ValueNode NumberLess(ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Boolean", left < right, range);
	}

	private static ValueNode NumberLessOrEqual(ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Boolean", left <= right, range);
	}

	private static ValueNode NumberGreater(ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Boolean", left > right, range);
	}

	private static ValueNode NumberGreaterOrEqual(ValueNode[] args, Range<Position> range)
	{
		double left = args[0].ValueAs<double>();
		double right = args[1].ValueAs<double>();
		return new ValueNode("Boolean", left >= right, range);
	}

	private static ValueNode Write(ValueNode[] args, Range<Position> range)
	{
		Console.WriteLine(args[0].ToString());
		return ValueNode.NullableAt("Number", range);
	}
}
using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Application.Parsing;
using ProgrammingLanguage.Shared.Helpers;
using static System.Math;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Evaluator
{
	private readonly Module Module = new();
	private readonly ValueResolver Valuator;
	private readonly IdentifierResolver Nominator;

	public Evaluator()
	{
		Valuator = new(Module);
		Nominator = new(Module);

		Valuator.Nominator = Nominator;
		Nominator.Valuator = Valuator;

		ImportCore(~Position.Zero);
	}

	private void ImportCore(Range<Position> range)
	{
		ImportType(range);
		ImportFunction(range);
		ImportNumber(range);
		ImportBoolean(range);
		ImportString(range);
		ImportMath(range);
		ImportInOut(range);
	}

	private void ImportType(Range<Position> range)
	{
		Module.RegisterType("Type", typeof(Type), range);
	}

	private void ImportFunction(Range<Position> range)
	{
		Module.RegisterType("Function", typeof(Function), range);
	}

	private void ImportNumber(Range<Position> range)
	{
		Structure typeNumber = Module.RegisterType("Number", typeof(double), range);
		typeNumber.RegisterOperation("+", NumberPlus, range);
		typeNumber.RegisterOperation("-", NumberMinus, range);
		typeNumber.RegisterOperation("*", NumberMultiplication, range);
		typeNumber.RegisterOperation("/", NumberDivision, range);
		typeNumber.RegisterOperation("=", NumberEqual, range);
		typeNumber.RegisterOperation("<", NumberLess, range);
		typeNumber.RegisterOperation("<=", NumberLessOrEqual, range);
		typeNumber.RegisterOperation(">", NumberGreater, range);
		typeNumber.RegisterOperation(">=", NumberGreaterOrEqual, range);
	}

	private ValueNode NumberPlus(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 0, range);
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new TypeMismatchIssue(nodeLeft.Tag, "Number", range);
		if (!enumerator.MoveNext()) return NumberPlus(nodeLeft, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new TypeMismatchIssue(nodeRight.Tag, "Number", range);
		return NumberPlus(nodeLeft, nodeRight, range);
	}

	private static ValueNode NumberPlus(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Number", left + right, range);
	}

	private static ValueNode NumberPlus(ValueNode nodeTarget, Range<Position> range)
	{
		double target = nodeTarget.ValueAs<double>();
		return new ValueNode("Number", +target, range);
	}

	private ValueNode NumberMinus(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 0, range);
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new TypeMismatchIssue(nodeLeft.Tag, "Number", range);
		if (!enumerator.MoveNext()) return NumberMinus(nodeLeft, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new TypeMismatchIssue(nodeRight.Tag, "Number", range);
		return NumberMinus(nodeLeft, nodeRight, range);
	}

	private static ValueNode NumberMinus(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Number", left - right, range);
	}

	private static ValueNode NumberMinus(ValueNode nodeTarget, Range<Position> range)
	{
		double target = nodeTarget.ValueAs<double>();
		return new ValueNode("Number", -target, range);
	}

	private ValueNode NumberMultiplication(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 0, range);
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new TypeMismatchIssue(nodeLeft.Tag, "Number", range);
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 1, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new TypeMismatchIssue(nodeRight.Tag, "Number", range);
		return NumberMultiplication(nodeLeft, nodeRight, range);
	}

	private static ValueNode NumberMultiplication(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Number", left * right, range);
	}

	private ValueNode NumberDivision(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 0, range);
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new TypeMismatchIssue(nodeLeft.Tag, "Number", range);
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 1, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new TypeMismatchIssue(nodeRight.Tag, "Number", range);
		return NumberDivision(nodeLeft, nodeRight, range);
	}

	private static ValueNode NumberDivision(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Number", left / right, range);
	}

	private ValueNode NumberEqual(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 0, range);
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new TypeMismatchIssue(nodeLeft.Tag, "Number", range);
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 1, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new TypeMismatchIssue(nodeRight.Tag, "Number", range);
		return NumberEqual(nodeLeft, nodeRight, range);
	}

	private static ValueNode NumberEqual(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Boolean", left == right, range);
	}

	private ValueNode NumberLess(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 0, range);
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new TypeMismatchIssue(nodeLeft.Tag, "Number", range);
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 1, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new TypeMismatchIssue(nodeRight.Tag, "Number", range);
		return NumberLess(nodeLeft, nodeRight, range);
	}

	private static ValueNode NumberLess(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Boolean", left < right, range);
	}

	private ValueNode NumberLessOrEqual(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 0, range);
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new TypeMismatchIssue(nodeLeft.Tag, "Number", range);
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 1, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new TypeMismatchIssue(nodeRight.Tag, "Number", range);
		return NumberLessOrEqual(nodeLeft, nodeRight, range);
	}

	private static ValueNode NumberLessOrEqual(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Boolean", left <= right, range);
	}

	private ValueNode NumberGreater(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 0, range);
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new TypeMismatchIssue(nodeLeft.Tag, "Number", range);
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 1, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new TypeMismatchIssue(nodeRight.Tag, "Number", range);
		return NumberGreater(nodeLeft, nodeRight, range);
	}

	private static ValueNode NumberGreater(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Boolean", left > right, range);
	}

	private ValueNode NumberGreaterOrEqual(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 0, range);
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new TypeMismatchIssue(nodeLeft.Tag, "Number", range);
		if (!enumerator.MoveNext()) throw new NoOverloadIssue(nodeOperand.Name, 1, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new TypeMismatchIssue(nodeRight.Tag, "Number", range);
		return NumberGreaterOrEqual(nodeLeft, nodeRight, range);
	}

	private static ValueNode NumberGreaterOrEqual(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Boolean", left >= right, range);
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
		Module.RegisterConstant("Number", "pi", PI, range);
		Module.RegisterConstant("Number", "e", E, range);
	}

	private void ImportInOut(Range<Position> range)
	{
		Module.RegisterOperation("write", Write, range);
	}

	private ValueNode Write(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		foreach (Node nodeArgument in arguments)
		{
			Console.WriteLine(nodeArgument.Accept(Valuator).Value);
		}
		return ValueNode.NullableAt("Number", range);
	}

	public void Evaluate(IEnumerable<Node> trees)
	{
		foreach (Node tree in trees) tree.Accept(Valuator);
	}
}

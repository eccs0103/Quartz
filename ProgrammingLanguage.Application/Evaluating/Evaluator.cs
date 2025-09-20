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
		ImportNumber(range);
		ImportBoolean(range);
		ImportString(range);
		ImportMath(range);

		Operation operationWrite = new(Write);
		Module.RegisterOperation("write", operationWrite, range);
	}

	private void ImportType(Range<Position> range)
	{
		Structure typeType = new(typeof(Type));
		Module.RegisterType("Type", typeType, range);
	}

	private void ImportNumber(Range<Position> range)
	{
		Structure typeNumber = new(typeof(double));

		Operation operationPlus = new(Plus);
		typeNumber.RegisterOperation("+", operationPlus, range);

		Operation operationMinus = new(Minus);
		typeNumber.RegisterOperation("-", operationMinus, range);

		Operation operationMultiplication = new(Multiplication);
		typeNumber.RegisterOperation("*", operationMultiplication, range);

		Operation operationDivision = new(Division);
		typeNumber.RegisterOperation("/", operationDivision, range);

		Module.RegisterType("Number", typeNumber, range);
	}

	private Node Plus(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new ArgumentException($"No overload for '{nodeOperand.Name}' that takes 0 arguments");
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new Exception($"Cannot apply '{nodeOperand.Name}' to types '{nodeLeft.Tag}'");
		if (!enumerator.MoveNext()) return UnaryPlus(nodeLeft, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new Exception($"Cannot apply '{nodeOperand.Name}' to types '{nodeLeft.Tag}' and '{nodeRight.Tag}'");
		return BinaryPlus(nodeLeft, nodeRight, range);
	}

	private ValueNode BinaryPlus(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Number", left + right, range);
	}

	private ValueNode UnaryPlus(ValueNode nodeTarget, Range<Position> range)
	{
		double target = nodeTarget.ValueAs<double>();
		return new ValueNode("Number", +target, range);
	}

	private Node Minus(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new ArgumentException($"No overload for '{nodeOperand.Name}' that takes 0 arguments");
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new Exception($"Cannot apply '{nodeOperand.Name}' to types '{nodeLeft.Tag}'");
		if (!enumerator.MoveNext()) return UnaryMinus(nodeLeft, range);
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new Exception($"Cannot apply '{nodeOperand.Name}' to types '{nodeLeft.Tag}' and '{nodeRight.Tag}'");
		return BinaryMinus(nodeLeft, nodeRight, range);
	}

	private ValueNode BinaryMinus(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Number", left - right, range);
	}

	private ValueNode UnaryMinus(ValueNode nodeTarget, Range<Position> range)
	{
		double target = nodeTarget.ValueAs<double>();
		return new ValueNode("Number", -target, range);
	}

	private Node Multiplication(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new ArgumentException($"No overload for '{nodeOperand.Name}' that takes 0 arguments");
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new Exception($"Cannot apply '{nodeOperand.Name}' to types '{nodeLeft.Tag}'");
		if (!enumerator.MoveNext()) throw new ArgumentException($"No overload for '{nodeOperand.Name}' that takes 1 arguments");
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new Exception($"Cannot apply '{nodeOperand.Name}' to types '{nodeLeft.Tag}' and '{nodeRight.Tag}'");
		return BinaryMultiplication(nodeLeft, nodeRight, range);
	}

	private ValueNode BinaryMultiplication(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Number", left * right, range);
	}

	private Node Division(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
	{
		IEnumerator<Node> enumerator = arguments.GetEnumerator();
		if (!enumerator.MoveNext()) throw new ArgumentException($"No overload for '{nodeOperand.Name}' that takes 0 arguments");
		ValueNode nodeLeft = enumerator.Current.Accept(Valuator);
		if (nodeLeft.Tag != "Number") throw new Exception($"Cannot apply '{nodeOperand.Name}' to types '{nodeLeft.Tag}'");
		if (!enumerator.MoveNext()) throw new ArgumentException($"No overload for '{nodeOperand.Name}' that takes 1 arguments");
		ValueNode nodeRight = enumerator.Current.Accept(Valuator);
		if (nodeRight.Tag != "Number") throw new Exception($"Cannot apply '{nodeOperand.Name}' to types '{nodeLeft.Tag}' and '{nodeRight.Tag}'");
		return BinaryDivision(nodeLeft, nodeRight, range);
	}

	private ValueNode BinaryDivision(ValueNode nodeLeft, ValueNode nodeRight, Range<Position> range)
	{
		double left = nodeLeft.ValueAs<double>();
		double right = nodeRight.ValueAs<double>();
		return new ValueNode("Number", left / right, range);
	}

	private void ImportBoolean(Range<Position> range)
	{
		Structure typeBoolean = new(typeof(bool));
		Module.RegisterType("Boolean", typeBoolean, range);
	}

	private void ImportString(Range<Position> range)
	{
		Structure typeString = new(typeof(string));
		Module.RegisterType("String", typeString, range);
	}

	private void ImportMath(Range<Position> range)
	{
		Datum datumPi = new("Number", PI);
		Module.RegisterDatum("pi", datumPi, range);

		Datum datumE = new("Number", E);
		Module.RegisterDatum("e", datumE, range);
	}

	private Node Write(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range)
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

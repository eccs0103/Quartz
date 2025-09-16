using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Application.Parsing;

namespace ProgrammingLanguage.Application.Evaluating;

internal class ValueResolver(Registry memory) : IResolverVisitor<ValueNode>
{
	public IdentifierResolver Nominator { get; set; } = default!;

	public ValueNode Visit(ValueNode node)
	{
		return node;
	}

	public ValueNode Visit(IdentifierNode node)
	{
		(string type, object? value) = memory.Read(node.Name);
		return new ValueNode(type, value, node.RangePosition);
	}

	public ValueNode Visit(DeclarationNode node)
	{
		IdentifierNode nodeIdentifier = node.Identifier;
		ValueNode nodeValue = node.Value.Accept(this);
		try
		{
			memory.DeclareVariable(nodeValue.Type, nodeIdentifier.Name, nodeValue.Value);
			return ValueNode.NullableAt("Number", node.RangePosition);
		}
		catch (InvalidOperationException)
		{
			throw new AlreadyExistsIssue($"Identifier '{nodeIdentifier.Name}'", node.RangePosition.Begin);
		}
	}

	public ValueNode Visit(InvokationNode node)
	{
		IdentifierNode nodeTarget = node.Target;
		switch (nodeTarget.Name)
		{
		case "write":
		{
			foreach (Node nodeArgument in node.Arguments)
			{
				Console.WriteLine(nodeArgument.Accept(this).Value);
			}
			return ValueNode.NullableAt("Number", node.RangePosition);
		}
		default: throw new NotExistIssue($"Function {nodeTarget.Name}", node.RangePosition);
		}

		// if (!Functions.TryGetValue(node.Target.Name, out Function? function)) throw new Issue($"Function '{node.Target.Name}' does not exist", node.Target.RangePosition);
		// IEnumerable<ValueNode> arguments = node.Arguments.Select(argument => argument.Accept(this));
		// return function.Invoke(arguments, node.RangePosition);
	}

	public ValueNode Visit(UnaryOperatorNode node)
	{
		ValueNode nodeTarget = node.Target.Accept(this);
		switch (node.Operator)
		{
		case "+": return new ValueNode("Number", +nodeTarget.ValueAs<double>(), node.RangePosition >> nodeTarget.RangePosition);
		case "-": return new ValueNode("Number", -nodeTarget.ValueAs<double>(), node.RangePosition >> nodeTarget.RangePosition);
		// case "import":
		// {
		// 	string address = nodeTarget.Accept(this).ValueAs<string>();
		// 	string input = Fetch(address) ?? throw new Issue($"Executable APL file in '{address}' doesn't exist", RangePosition.Begin);
		// 	interpreter.Run(input);
		// 	return ValueNode.NullableAt("Number", node.RangePosition >> nodeTarget.RangePosition);
		// }
		default: throw new UnidentifiedIssue($"'{node.Operator}' operator", node.RangePosition);
		}
	}

	public ValueNode Visit(BinaryOperatorNode node)
	{
		switch (node.Operator)
		{
		case "+":
		{
			ValueNode left = node.Left.Accept(this);
			ValueNode right = node.Right.Accept(this);
			return new ValueNode("Number", left.ValueAs<double>() + right.ValueAs<double>(), left.RangePosition >> right.RangePosition);
		}
		case "-":
		{
			ValueNode left = node.Left.Accept(this);
			ValueNode right = node.Right.Accept(this);
			return new ValueNode("Number", left.ValueAs<double>() - right.ValueAs<double>(), left.RangePosition >> right.RangePosition);
		}
		case "*":
		{
			ValueNode left = node.Left.Accept(this);
			ValueNode right = node.Right.Accept(this);
			return new ValueNode("Number", left.ValueAs<double>() * right.ValueAs<double>(), left.RangePosition >> right.RangePosition);
		}
		case "/":
		{
			ValueNode left = node.Left.Accept(this);
			ValueNode right = node.Right.Accept(this);
			return new ValueNode("Number", left.ValueAs<double>() / right.ValueAs<double>(), left.RangePosition >> right.RangePosition);
		}
		case ":":
		{
			IdentifierNode left = node.Left.Accept(Nominator);
			ValueNode right = node.Right.Accept(this);
			try
			{
				memory.Assign(left.Name, right.Value);
				return ValueNode.NullableAt("Number", node.RangePosition);
			}
			catch (NullReferenceException)
			{
				throw new NotExistIssue($"Identifier '{left.Name}'", left.RangePosition);
			}
			catch (InvalidOperationException)
			{
				throw new NotMutableIssue($"Identifier '{left.Name}'", left.RangePosition);
			}
		}
		default: throw new UnidentifiedIssue($"'{node.Operator}' operator", node.RangePosition);
		}
	}
}


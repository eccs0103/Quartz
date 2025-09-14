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
		try
		{
			object? value = memory.Read(node.Name);
			return new ValueNode(value, node.RangePosition);
		}
		catch (NullReferenceException)
		{
			throw new NotExistIssue($"Identifier '{node.Name}'", node.RangePosition);
		}
	}

	public ValueNode Visit(DeclarationNode node)
	{
		try
		{
			ValueNode value = node.Value.Accept(this);
			memory.DeclareVariable("Number", node.Identifier.Name, value.Value);
			return ValueNode.NullAt(node.RangePosition);
		}
		catch (InvalidOperationException)
		{
			throw new AlreadyExistsIssue($"Identifier '{node.Identifier.Name}'", node.RangePosition.Begin);
		}
	}

	public ValueNode Visit(InvokationNode node)
	{
		throw new NotImplementedException();
		// if (!Functions.TryGetValue(node.Target.Name, out Function? function)) throw new Issue($"Function '{node.Target.Name}' does not exist", node.Target.RangePosition);
		// IEnumerable<ValueNode> arguments = node.Arguments.Select(argument => argument.Accept(this));
		// return function.Invoke(arguments, node.RangePosition);
	}

	public ValueNode Visit(UnaryOperatorNode node)
	{
		// case "import":
		// {
		// 	string address = Target.Evaluate<ValueNode>(interpreter).GetValue<string>();
		// 	string input = Fetch(address) ?? throw new Issue($"Executable APL file in '{address}' doesn't exist", RangePosition.Begin);
		// 	interpreter.Run(input);
		// 	return (new ValueNode(null, RangePosition));
		// }

		ValueNode target = node.Target.Accept(this);
		switch (node.Operator)
		{
		case "+": return new ValueNode(+target.ValueAs<double>(), node.RangePosition >> target.RangePosition);
		case "-": return new ValueNode(-target.ValueAs<double>(), node.RangePosition >> target.RangePosition);
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
			return new ValueNode(left.ValueAs<double>() + right.ValueAs<double>(), left.RangePosition >> right.RangePosition);
		}
		case "-":
		{
			ValueNode left = node.Left.Accept(this);
			ValueNode right = node.Right.Accept(this);
			return new ValueNode(left.ValueAs<double>() - right.ValueAs<double>(), left.RangePosition >> right.RangePosition);
		}
		case "*":
		{
			ValueNode left = node.Left.Accept(this);
			ValueNode right = node.Right.Accept(this);
			return new ValueNode(left.ValueAs<double>() * right.ValueAs<double>(), left.RangePosition >> right.RangePosition);
		}
		case "/":
		{
			ValueNode left = node.Left.Accept(this);
			ValueNode right = node.Right.Accept(this);
			return new ValueNode(left.ValueAs<double>() / right.ValueAs<double>(), left.RangePosition >> right.RangePosition);
		}
		case ":":
		{
			IdentifierNode left = node.Left.Accept(Nominator);
			ValueNode right = node.Right.Accept(this);
			try
			{
				memory.Assign(left.Name, right.Value);
				return ValueNode.NullAt(node.RangePosition);
			}
			catch (NullReferenceException)
			{
				throw new NotExistIssue($"Identifier '{left.Name}'", left.RangePosition); //throw new Issue($"Identifier '{left.Name}' does not exist or is non-mutable", left.RangePosition);
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


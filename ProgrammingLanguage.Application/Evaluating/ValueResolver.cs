using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Application.Parsing;

namespace ProgrammingLanguage.Application.Evaluating;

internal class ValueResolver(Module module) : IResolverVisitor<ValueNode>
{
	public IdentifierResolver Nominator { get; set; } = default!;

	public ValueNode Visit(ValueNode node)
	{
		return node;
	}

	public ValueNode Visit(IdentifierNode node)
	{
		Datum datum = module.ReadDatum(node.Name, node.RangePosition);
		return new ValueNode(datum.Tag, datum.Value, node.RangePosition);
	}

	public ValueNode Visit(DeclarationNode node)
	{
		IdentifierNode nodeIdentifier = node.Identifier;
		ValueNode nodeValue = node.Value.Accept(this);
		Datum datum = new(nodeValue.Tag, nodeValue.Value, true);
		module.RegisterDatum(nodeIdentifier.Name, datum, ~node.RangePosition.Begin);
		return ValueNode.NullableAt("Number", node.RangePosition);
	}

	public ValueNode Visit(InvokationNode node)
	{
		IdentifierNode nodeTarget = node.Target;
		Operation operation = module.ReadOperation(nodeTarget.Name, nodeTarget.RangePosition);
		Node result = operation.Invoke(nodeTarget, node.Arguments, node.RangePosition >> nodeTarget.RangePosition);
		return result.Accept(this);

		// switch (nodeTarget.Name)
		// {
		// case "write":
		// {
		// 	foreach (Node nodeArgument in node.Arguments)
		// 	{
		// 		Console.WriteLine(nodeArgument.Accept(this).Value);
		// 	}
		// 	return ValueNode.NullableAt("Number", node.RangePosition);
		// }
		// default: throw new NotExistIssue($"Function {nodeTarget.Name}", node.RangePosition);
		// }

		// if (!Functions.TryGetValue(node.Target.Name, out Function? function)) throw new Issue($"Function '{node.Target.Name}' does not exist", node.Target.RangePosition);
		// IEnumerable<ValueNode> arguments = node.Arguments.Select(argument => argument.Accept(this));
		// return function.Invoke(arguments, node.RangePosition);
	}

	public ValueNode Visit(UnaryOperatorNode node)
	{
		IdentifierNode nodeOperator = node.Operator;
		ValueNode nodeTarget = node.Target.Accept(this);
		Structure type = module.ReadType(nodeTarget.Tag, nodeTarget.RangePosition);
		Operation operation = type.ReadOperation(nodeOperator.Name, nodeOperator.RangePosition);
		Node result = operation.Invoke(nodeOperator, [nodeTarget], node.RangePosition >> nodeTarget.RangePosition);
		return result.Accept(this);

		// 	string address = nodeTarget.Accept(this).ValueAs<string>();
		// 	string input = Fetch(address) ?? throw new Issue($"Executable APL file in '{address}' doesn't exist", RangePosition.Begin);
		// 	interpreter.Run(input);
		// 	return ValueNode.NullableAt("Number", node.RangePosition >> nodeTarget.RangePosition);
	}

	public ValueNode Visit(BinaryOperatorNode node)
	{
		IdentifierNode nodeOperator = node.Operator;
		ValueNode nodeLeft = node.Left.Accept(this);
		Structure type = module.ReadType(nodeLeft.Tag, nodeLeft.RangePosition);
		Operation operation = type.ReadOperation(nodeOperator.Name, nodeOperator.RangePosition);
		ValueNode nodeRight = node.Right.Accept(this);
		Node result = operation.Invoke(nodeOperator, [nodeLeft, nodeRight], nodeLeft.RangePosition >> nodeRight.RangePosition);
		return result.Accept(this);
	}
}

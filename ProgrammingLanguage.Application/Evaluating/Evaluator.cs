using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Application.Parsing;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Evaluator(Module module) : IAstVisitor<ValueNode>
{
	private readonly Structure Global = module.ReadType("@Global", ~Position.Zero);

	public ValueNode Visit(ValueNode node)
	{
		return node;
	}

	public ValueNode Visit(IdentifierNode node)
	{
		Property datum = Global.ReadProperty(node.Name, node.RangePosition);
		return new ValueNode(datum.Tag, datum.Value, node.RangePosition);
	}

	public ValueNode Visit(DeclarationNode node)
	{
		IdentifierNode nodeType = node.Type;
		IdentifierNode nodeIdentifier = node.Identifier;
		ValueNode nodeValue = node.Value.Accept(this);
		if (nodeType.Name != nodeValue.Tag) throw new TypeMismatchIssue(nodeValue.Tag, nodeType.Name, nodeValue.RangePosition);
		Global.RegisterVariable(nodeIdentifier.Name, nodeType.Name, nodeValue.Value!, nodeIdentifier.RangePosition);
		return ValueNode.NullableAt("Number", node.RangePosition);
	}

	public ValueNode Visit(AssignmentNode node)
	{
		IdentifierNode nodeIdentifier = node.Identifier;
		ValueNode nodeValue = node.Value.Accept(this);
		Global.WriteProperty(nodeIdentifier.Name, nodeValue.Tag, nodeValue.Value!, nodeIdentifier.RangePosition);
		return ValueNode.NullableAt("Number", node.RangePosition);
	}

	public ValueNode Visit(InvokationNode node)
	{
		IdentifierNode nodeTarget = node.Target;
		IEnumerable<ValueNode> arguments = node.Arguments.Select(argument => argument.Accept(this));
		Operation operation = Global.ReadOperation(nodeTarget.Name, arguments.Select(result => result.Tag), nodeTarget.RangePosition);
		return operation.Invoke(arguments, node.RangePosition);
	}

	public ValueNode Visit(UnaryOperatorNode node)
	{
		IdentifierNode nodeOperator = node.Operator;
		ValueNode nodeTarget = node.Target.Accept(this);
		IEnumerable<ValueNode> arguments = [nodeTarget];
		Structure type = module.ReadType(nodeTarget.Tag, nodeTarget.RangePosition);
		Operation operation = type.ReadOperation(nodeOperator.Name, arguments.Select(result => result.Tag), nodeOperator.RangePosition);
		return operation.Invoke(arguments, node.RangePosition);
	}

	public ValueNode Visit(BinaryOperatorNode node)
	{
		IdentifierNode nodeOperator = node.Operator;
		ValueNode nodeLeft = node.Left.Accept(this);
		ValueNode nodeRight = node.Right.Accept(this);
		IEnumerable<ValueNode> arguments = [nodeLeft, nodeRight];
		Structure type = module.ReadType(nodeLeft.Tag, nodeLeft.RangePosition);
		Operation operation = type.ReadOperation(nodeOperator.Name, arguments.Select(result => result.Tag), nodeOperator.RangePosition);
		return operation.Invoke(arguments, node.RangePosition);
	}

	public ValueNode Visit(BlockNode node)
	{
		foreach (Node statement in node.Statements) statement.Accept(this);
		return ValueNode.NullableAt("Number", node.RangePosition);
	}

	public ValueNode Visit(IfStatementNode node)
	{
		ValueNode condition = node.Condition.Accept(this);
		if (condition.Tag != "Boolean") throw new TypeMismatchIssue("Boolean", condition.Tag, node.Condition.RangePosition);
		(condition.ValueAs<bool>() ? node.Then : node.Else)?.Accept(this);
		return ValueNode.NullableAt("Number", node.RangePosition);
	}
}

using System.Text;
using Quartz.Domain.Evaluating;
using Quartz.Shared.Helpers;

namespace Quartz.Domain.Parsing;

public class IfStatementNode(Node condition, BlockNode then, Node? @else, Range<Position> range) : Node(range)
{
	public readonly Node Condition = condition;
	public readonly BlockNode Then = then;
	public readonly Node? Else = @else;

	public override string ToString()
	{
		StringBuilder builder = new();
		builder.Append($"if ({Condition}) ");
		builder.Append(Then);

		if (Else is not null)
		{
			builder.Append(" else ");
			builder.Append(Else);
		}

		return builder.ToString();
	}

	public override T Accept<T>(IAstVisitor<T> visitor, Scope location)
	{
		return visitor.Visit(location, this);
	}
}

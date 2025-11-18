using Quartz.Domain.Evaluating;
using Quartz.Shared.Helpers;

namespace Quartz.Domain.Parsing;

public class RepeatStatementNode(Node сount, Node body, Range<Position> range) : Node(range)
{
	public Node Count { get; } = сount;
	public Node Body { get; } = body;

	public override string ToString()
	{
		return $"repeat ({Count}) {Body}";
	}

	public override T Accept<T>(IAstVisitor<T> visitor, Scope location)
	{
		return visitor.Visit(location, this);
	}
}

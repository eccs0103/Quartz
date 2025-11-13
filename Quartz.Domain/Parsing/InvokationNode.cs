using Quartz.Domain.Evaluating;
using Quartz.Shared.Helpers;

namespace Quartz.Domain.Parsing;

public class InvokationNode(IdentifierNode target, IEnumerable<Node> arguments, Range<Position> range) : Node(range)
{
	public readonly IdentifierNode Target = target;
	public readonly IEnumerable<Node> Arguments = arguments;

	public override string ToString()
	{
		return $"{Target}({string.Join(", ", Arguments)})";
	}

	public override T Accept<T>(IAstVisitor<T> visitor, Scope location)
	{
		return visitor.Visit(location, this);
	}
}

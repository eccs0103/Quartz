using Quartz.Domain.Evaluating;
using Quartz.Shared.Helpers;

namespace Quartz.Domain.Parsing;

public abstract class Node(Range<Position> range)
{
	public readonly Range<Position> RangePosition = range;

	public abstract T Accept<T>(IAstVisitor<T> visitor, Scope location);
}

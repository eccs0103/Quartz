using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal abstract class Node(Range<Position> range)
{
	public readonly Range<Position> RangePosition = range;

	public abstract T Accept<T>(IResolverVisitor<T> visitor);
}

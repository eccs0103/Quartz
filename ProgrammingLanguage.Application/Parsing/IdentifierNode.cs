using ProgrammingLanguage.Application.Evaluating;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Parsing;

internal class IdentifierNode(string name, Range<Position> range) : Node(range)
{
	public readonly string Name = name;

	public override string ToString()
	{
		return $"{Name}";
	}

	public override T Accept<T>(IAstVisitor<T> visitor, Scope location)
	{
		return visitor.Visit(location, this);
	}
}

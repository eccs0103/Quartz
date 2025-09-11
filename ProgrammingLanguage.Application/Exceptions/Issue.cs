using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class Issue(string message, Range<Position> range) : Exception($"{message} {range}")
{
	public readonly Range<Position> Range = range;

	public Issue(string message, Position position): this(message, position >> position)
	{
	}
}

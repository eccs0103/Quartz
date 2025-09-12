using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class UnexpectedIssue(string value, Range<Position> range) : Issue($"Unexpected {value}", range)
{
	public UnexpectedIssue(string value, Position position) : this(value, position >> position)
	{
	}
}

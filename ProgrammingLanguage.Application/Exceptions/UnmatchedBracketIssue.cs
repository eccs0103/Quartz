using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class UnmatchedBracketIssue(string value, Range<Position> range) : Issue($"Unmatched bracket {value}", range)
{
	public UnmatchedBracketIssue(string value, Position position) : this(value, position >> position)
	{
	}
}

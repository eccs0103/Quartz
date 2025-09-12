using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class UnidentifiedIssue(string value, Range<Position> range) : Issue($"Unidentified {value}", range)
{
	public UnidentifiedIssue(string value, Position position) : this(value, position >> position)
	{
	}
}

using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class NotExistIssue(string value, Range<Position> range) : Issue($"{value} does not exist", range)
{
	public NotExistIssue(string value, Position position) : this(value, position >> position)
	{
	}
}

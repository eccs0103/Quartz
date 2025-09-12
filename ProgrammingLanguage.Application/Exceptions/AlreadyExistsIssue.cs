using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class AlreadyExistsIssue(string value, Range<Position> range) : Issue($"{value} already exists", range)
{
	public AlreadyExistsIssue(string value, Position position) : this(value, position >> position)
	{
	}
}

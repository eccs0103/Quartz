using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class NotMutableIssue(string value, Range<Position> range) : Issue($"{value} is non-mutable", range)
{
	public NotMutableIssue(string value, Position position) : this(value, position >> position)
	{
	}
}

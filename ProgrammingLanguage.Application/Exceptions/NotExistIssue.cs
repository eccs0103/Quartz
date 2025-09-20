using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class NotExistIssue(string value, Range<Position> range) : Issue($"{value} does not exist", range)
{
}

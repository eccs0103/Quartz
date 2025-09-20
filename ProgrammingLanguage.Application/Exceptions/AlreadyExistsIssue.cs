using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class AlreadyExistsIssue(string value, Range<Position> range) : Issue($"{value} already exists", range)
{
}

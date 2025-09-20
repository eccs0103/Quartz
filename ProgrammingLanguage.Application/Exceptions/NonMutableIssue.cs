using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class NotMutableIssue(string value, Range<Position> range) : Issue($"{value} is non-mutable", range)
{
}

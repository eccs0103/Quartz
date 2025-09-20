using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class ExpectedIssue(string value, Range<Position> range) : Issue($"Expected {value}", range)
{
}

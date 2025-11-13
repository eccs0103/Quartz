using Quartz.Shared.Helpers;

namespace Quartz.Domain.Exceptions;

public class TypeMismatchIssue(string from, string to, Range<Position> range) : Issue($"Cannot convert type '{from}' to '{to}'", range)
{
}

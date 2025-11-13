using Quartz.Shared.Helpers;

namespace Quartz.Domain.Exceptions;

public class UnexpectedIssue(string value, Range<Position> range) : Issue($"Unexpected {value}", range)
{
}

using Quartz.Shared.Helpers;

namespace Quartz.Domain.Exceptions;

public class ExpectedIssue(string value, Range<Position> range) : Issue($"Expected {value}", range)
{
}

using Quartz.Shared.Helpers;

namespace Quartz.Domain.Exceptions;

public class NotMutableIssue(string value, Range<Position> range) : Issue($"{value} is non-mutable", range)
{
}

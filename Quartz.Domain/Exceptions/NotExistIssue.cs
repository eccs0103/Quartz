using Quartz.Shared.Helpers;

namespace Quartz.Domain.Exceptions;

public class NotExistIssue(string value, Range<Position> range) : Issue($"{value} does not exist", range)
{
}

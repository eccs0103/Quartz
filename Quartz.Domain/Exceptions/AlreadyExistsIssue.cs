using Quartz.Shared.Helpers;

namespace Quartz.Domain.Exceptions;

public class AlreadyExistsIssue(string value, Range<Position> range) : Issue($"{value} already exists", range)
{
}

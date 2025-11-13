using Quartz.Shared.Helpers;

namespace Quartz.Domain.Exceptions;

public class UnidentifiedIssue(string value, Range<Position> range) : Issue($"Unidentified {value}", range)
{
}

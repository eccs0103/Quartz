using Quartz.Shared.Helpers;

namespace Quartz.Domain.Exceptions;

public class UnmatchedBracketIssue(string value, Range<Position> range) : Issue($"Unmatched bracket {value}", range)
{
}

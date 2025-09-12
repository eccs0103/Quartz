using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public abstract class Issue(string message, Range<Position> range) : Exception($"{message} at {range.Begin}")
{
	public readonly Range<Position> Range = range;
}

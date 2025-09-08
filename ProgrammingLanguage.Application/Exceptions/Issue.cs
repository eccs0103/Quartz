using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public abstract class Issue(string message, Range<Position> range) : Exception($"{message} {range}")
{
	public readonly Range<Position> Range = range;
}

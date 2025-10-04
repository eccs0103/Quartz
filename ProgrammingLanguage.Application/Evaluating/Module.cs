using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Module()
{
	private readonly Dictionary<string, Structure> Types = [];

	public Structure RegisterType(string name, Type equivalent, Range<Position> range)
	{
		Structure type = new(name, equivalent);
		if (!Types.TryAdd(name, type)) throw new AlreadyExistsIssue($"Type '{name}' at module", range);
		return type;
	}

	public Structure ReadType(string name, Range<Position> range)
	{
		if (!Types.TryGetValue(name, out Structure? type)) throw new NotExistIssue($"Type '{name}' at module", range);
		return type;
	}	
}

using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Structure(Type equivalent) : Datum("Type", equivalent), IOperationContainer
{
	private readonly Dictionary<string, Operation> Operations = [];

	public void RegisterOperation(string name, Operation operation, Range<Position> range)
	{
		if (!Operations.TryAdd(name, operation)) throw new AlreadyExistsIssue($"Operation '{name}'", range);
	}

	public Operation ReadOperation(string name, Range<Position> range)
	{
		if (!Operations.TryGetValue(name, out Operation? operation)) throw new NotExistIssue($"Operation '{name}'", range);
		return operation;
	}
}
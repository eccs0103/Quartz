using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Operator(string name, Scope location) : Symbol(name)
{
	private string Mangle(IEnumerable<string> tags)
	{
		return $"{Name}({string.Join(", ", tags)})";
	}

	public Operation RegisterOperation(IEnumerable<string> parameters, string result, OperationContent function, Range<Position> range)
	{
		string name = Mangle(parameters);
		Scope scope = new(name, location);
		Operation operation = new(name, parameters, result, function, scope);
		location.Register(name, operation, range);
		return operation;
	}

	public Operation ReadOperation(IEnumerable<string> parameters, Range<Position> range)
	{
		string name = Mangle(parameters);
		Symbol symbol = location.Read(name, range);
		if (symbol is not Operation operation) throw new NotExistIssue($"Operation '{name}' in {location}", range);
		return operation;
	}
}

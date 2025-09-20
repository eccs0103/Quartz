using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Module : IDatumContainer, IOperationContainer, ITypeContainer
{
	private readonly Dictionary<string, Datum> Database = [];

	public void RegisterDatum(string name, Datum datum, Range<Position> range)
	{
		if (!Database.TryAdd(name, datum)) throw new AlreadyExistsIssue($"Identifier '{name}'", range);
	}

	public Datum ReadDatum(string name, Range<Position> range)
	{
		if (!Database.TryGetValue(name, out Datum? datum)) throw new NotExistIssue($"Identifier '{name}'", range);
		return datum;
	}

	public void WriteDatum(string name, object? value, Range<Position> range)
	{
		Datum datum = ReadDatum(name, range);
		if (!datum.Mutable) throw new NotMutableIssue($"Identifier '{name}'", range);
		datum.Value = value;
		Database[name] = datum;
	}

	public void RegisterOperation(string name, Operation operation, Range<Position> range)
	{
		if (!Database.TryAdd(name, operation)) throw new AlreadyExistsIssue($"Operation '{name}'", range);
	}

	public Operation ReadOperation(string name, Range<Position> range)
	{
		// TODO Change issue type
		if (ReadDatum(name, range) is not Operation operation) throw new NotExistIssue($"Operation '{name}'", range);
		return operation;
	}

	public void RegisterType(string name, Structure type, Range<Position> range)
	{
		if (!Database.TryAdd(name, type)) throw new AlreadyExistsIssue($"Type '{name}'", range);
	}

	public Structure ReadType(string name, Range<Position> range)
	{
		// TODO Change issue type
		if (ReadDatum(name, range) is not Structure type) throw new NotExistIssue($"Type '{name}'", range);
		return type;
	}
}

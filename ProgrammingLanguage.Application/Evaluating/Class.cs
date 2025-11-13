using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Class(string name, Type equivalent, Scope location) : Symbol(name)
{
	public Scope Scope { get; } = location;
	public Type Equivalent { get; } = equivalent;

	public Datum RegisterConstant(string name, string tag, object value, Range<Position> range)
	{
		Datum constant = new(name, tag, value, false);
		location.Register(name, constant, range);
		return constant;
	}

	public Datum RegisterVariable(string name, string tag, object value, Range<Position> range)
	{
		Datum variable = new(name, tag, value, true);
		location.Register(name, variable, range);
		return variable;
	}

	public Datum ReadProperty(string name, Range<Position> range)
	{
		Symbol symbol = location.Read(name, range);
		if (symbol is not Datum datum) throw new NotExistIssue($"Datum '{name}' in {location}", range);
		return datum;
	}

	public void WriteVariable(string name, string tag, object value, Range<Position> range)
	{
		location.Write(name, tag, value, range);
	}

	public Operator RegisterOperator(string name, Range<Position> range)
	{
		Scope scope = new(name, location);
		Operator @operator = new Operator(name, scope);
		location.Register(name, @operator, range);
		return @operator;
	}

	public Operator ReadOperator(string name, Range<Position> range)
	{
		Symbol symbol = location.Read(name, range);
		if (symbol is not Operator @operator) throw new NotExistIssue($"Operator '{name}' in {location}", range);
		return @operator;
	}
}

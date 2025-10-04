using System.Runtime.CompilerServices;
using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Structure(string name, Type equivalent) : Property(name, "Type", equivalent)
{
	public Type Equivalent => Unsafe.As<Type>(Value);
	private readonly Dictionary<string, Property> Properties = [];
	private readonly Dictionary<string, Operation> Operations = [];

	///

	public Property RegisterConstant(string name, string tag, object value, Range<Position> range)
	{
		Property constant = new(name, tag, value);
		if (!Properties.TryAdd(name, constant)) throw new AlreadyExistsIssue($"Identifier '{name}' at {Name}", range);
		return constant;
	}

	public Property RegisterVariable(string name, string tag, object value, Range<Position> range)
	{
		Property variable = new(name, tag, value, MutableOptions);
		if (!Properties.TryAdd(name, variable)) throw new AlreadyExistsIssue($"Identifier '{name}' at {Name}", range);
		return variable;
	}

	public Property ReadProperty(string name, Range<Position> range)
	{
		if (!Properties.TryGetValue(name, out Property? property)) throw new NotExistIssue($"Identifier '{name}' at {Name}", range);
		return property;
	}

	public void WriteProperty(string name, string tag, object value, Range<Position> range)
	{
		Property property = ReadProperty(name, range);
		if (!property.Mutable) throw new NotMutableIssue($"Identifier '{name}' at {Name}", range);
		if (property.Tag != tag) throw new TypeMismatchIssue(tag, property.Tag, range);
		property.Value = value;
		Properties[name] = property;
	}

	///

	private static string MangleName(string name, IEnumerable<string> tags)
	{
		string signature = string.Join(", ", tags);
		return $"{name}({signature})";
	}

	public Operation RegisterOperation(string name, IEnumerable<Parameter> parameters, string returnTag, OperationContent function, Range<Position> range)
	{
		if (!Properties.TryGetValue(name, out Property? property))
		{
			property = new OverloadSet(name);
			Properties.Add(name, property);
		}
		if (property is not OverloadSet set) throw new NotExistIssue($"Overload set '{name}' at {Name}", range);
		string identifier = MangleName(name, parameters.Select(parameter => parameter.Tag));
		Operation operation = new(identifier, parameters, returnTag, function);
		if (!Operations.TryAdd(identifier, operation)) throw new AlreadyExistsIssue($"Operation '{identifier} at {Name}'", range);
		set.Operations.Add(operation);
		return operation;
	}

	public Operation ReadOperation(string name, IEnumerable<string> parameters, Range<Position> range)
	{
		string identifier = MangleName(name, parameters);
		if (!Operations.TryGetValue(identifier, out Operation? operation)) throw new NotExistIssue($"Operation '{identifier}' at {Name}", range);
		return operation;
	}
}
using System.Runtime.CompilerServices;
using ProgrammingLanguage.Application.Exceptions;
using ProgrammingLanguage.Application.Parsing;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Evaluating;

internal delegate ValueNode OperationContent(ValueNode[] arguments, Range<Position> range);

internal class Operation(string name, IEnumerable<Parameter> parameters, string returnTag, OperationContent function) : Property(name, "Operation", function)
{
	public OperationContent Content => Unsafe.As<OperationContent>(Value);
	public readonly IEnumerable<Parameter> Parameters = parameters;
	public readonly string ReturnTag = returnTag;

	public ValueNode Invoke(IEnumerable<ValueNode> arguments, Range<Position> range)
	{
		List<ValueNode> results = [];
		using IEnumerator<ValueNode> iterator = arguments.GetEnumerator();
		foreach (Parameter expected in Parameters)
		{
			if (!iterator.MoveNext()) throw new NoOverloadIssue(Name, Convert.ToByte(results.Count), range);
			ValueNode provided = iterator.Current;
			if (provided.Tag != expected.Tag) throw new TypeMismatchIssue(expected.Tag, provided.Tag, provided.RangePosition);
			results.Add(provided);
		}
		ValueNode result = Content.Invoke([.. results], range);
		if (result.Tag != ReturnTag) throw new TypeMismatchIssue(result.Tag, ReturnTag, range);
		return result;
	}
}

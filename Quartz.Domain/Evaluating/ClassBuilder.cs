using Quartz.Domain.Exceptions;
using Quartz.Domain.Parsing;
using Quartz.Shared.Helpers;

namespace Quartz.Domain.Evaluating;

internal class ClassBuilder(Class type, Scope location)
{
	public ClassBuilder DeclareVariable(string name, string tag, object value)
	{
		type.RegisterVariable(name, tag, value, ~Position.Zero);
		return this;
	}

	public ClassBuilder DeclareConstant(string name, string tag, object value)
	{
		type.RegisterConstant(name, tag, value, ~Position.Zero);
		return this;
	}

	private Operator GetOperator(string name, Range<Position> range)
	{
		try
		{
			return type.RegisterOperator(name, range);
		}
		catch (AlreadyExistsIssue)
		{
			return type.ReadOperator(name, range);
		}
	}

	public ClassBuilder DeclareOperation(string name, IEnumerable<string> parameters, string result, Func<ValueNode[], object> content)
	{
		Scope scope = location.GetSubscope(name);
		Operator @operator = GetOperator(name, ~Position.Zero);
		@operator.RegisterOperation(parameters, result, (args, location, range) => new ValueNode(result, content(args), range), scope, ~Position.Zero);
		return this;
	}
}

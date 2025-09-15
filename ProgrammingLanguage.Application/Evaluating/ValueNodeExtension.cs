using ProgrammingLanguage.Application.Parsing;

namespace ProgrammingLanguage.Application.Evaluating;

public static class ValueNodeExtension
{
	internal static T ValueAs<T>(this ValueNode node)
	{
		if (node.Value is T result) return result;
		string type = node.Value?.GetType().Name ?? "Null";
		throw new InvalidCastException($"Unable to convert '{node.Value}' from {type} to {typeof(T).Name}");
	}
}

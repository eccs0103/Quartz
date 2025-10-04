using System.Runtime.CompilerServices;

namespace ProgrammingLanguage.Application.Evaluating;

internal class OverloadSet(string name) : Property(name, "OverloadSet", new List<Operation>())
{
	public List<Operation> Operations => Unsafe.As<List<Operation>>(Value);

	public override string ToString()
	{
		return $"<Overload set: {Name}>";
	}
}
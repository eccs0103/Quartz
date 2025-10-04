namespace ProgrammingLanguage.Application.Evaluating;

internal class Parameter(string name, string tag)
{
	public readonly string Name = name;
	public readonly string Tag = tag;

	public override string ToString()
	{
		return $"{Name} {Tag}";
	}
}

namespace Quartz.Domain.Evaluating;

public abstract class Symbol(string name)
{
	public string Name { get; } = name;
}

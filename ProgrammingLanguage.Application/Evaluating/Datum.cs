namespace ProgrammingLanguage.Application.Evaluating;

internal class Datum(string type, object? value, bool mutable)
{
	public readonly string Type = type;
	private object? _Value = value;
	public readonly bool Mutable = mutable;

	public object? Value
	{
		get => _Value;
		set
		{
			if (!Mutable) throw new InvalidOperationException("Unable to modify immutable value");
			_Value = value;
		}
	}

	public Datum(string type, object? value) : this(type, value, false)
	{
	}
}

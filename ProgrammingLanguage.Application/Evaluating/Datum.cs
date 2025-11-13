namespace ProgrammingLanguage.Application.Evaluating;

internal class Datum(string name, string tag, object value, bool mutable) : Symbol(name)
{
	public string Tag { get; } = tag;
	public bool Mutable { get; } = mutable;

	private object _Value = value;
	public object Value
	{
		get => _Value;
		set
		{
			if (!Mutable) throw new InvalidOperationException($"Unable to modify immutable datum '{Name}'");
			_Value = value;
		}
	}
}

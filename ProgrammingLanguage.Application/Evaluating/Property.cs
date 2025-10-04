namespace ProgrammingLanguage.Application.Evaluating;

internal class Property(string name, string tag, object value, Property.Options options)
{
	public class Options
	{
		public bool Mutable { get; set; } = false;
	}

	public static readonly Options ImmutableOptions = new() { Mutable = false };
	public static readonly Options MutableOptions = new() { Mutable = true };

	public readonly string Name = name;
	public readonly string Tag = tag;
	private object _Value = value;
	public readonly bool Mutable = options.Mutable;
	public object Value
	{
		get => _Value;
		set
		{
			if (!Mutable) throw new InvalidOperationException("Unable to modify immutable value");
			_Value = value;
		}
	}

	public Property(string name, string tag, object value) : this(name, tag, value, ImmutableOptions)
	{
	}
}

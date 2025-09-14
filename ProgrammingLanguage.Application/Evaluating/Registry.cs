namespace ProgrammingLanguage.Application.Evaluating;

internal class Registry
{
	private readonly Dictionary<string, Datum> Database = [];

	public object? Read(string name)
	{
		if (!Database.TryGetValue(name, out Datum? datum)) throw new NullReferenceException();
		return datum.Value;
	}

	public void Assign(string name, object? value)
	{
		if (!Database.TryGetValue(name, out Datum? datum)) throw new NullReferenceException();
		if (!datum.Mutable) throw new InvalidOperationException();
		// Type conversation
		datum.Value = value;
		Database[name] = datum;
	}

	public void Declare(string type, string name, object? value, bool mutable)
	{
		Datum constant = new(type, value, mutable);
		if (!Database.TryAdd(name, constant)) throw new InvalidOperationException();
	}

	public void DeclareConstant(string type, string name, object? value)
	{
		Declare(type, name, value, false);
	}

	public void DeclareVariable(string type, string name, object? value)
	{
		Declare(type, name, value, true);
	}

	public void DeclareType(string name, Type equivalent)
	{
		Typing constant = new(equivalent);
		if (!Database.TryAdd(name, constant)) throw new InvalidOperationException();
	}
}

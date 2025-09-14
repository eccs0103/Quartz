using ProgrammingLanguage.Application.Parsing;
using static System.Math;

namespace ProgrammingLanguage.Application.Evaluating;

internal class Evaluator
{
	private readonly Registry Memory = new();
	private readonly ValueResolver Valuator;
	private readonly IdentifierResolver Nominator;

	public Evaluator()
	{
		Valuator = new(Memory);
		Nominator = new(Memory);

		Valuator.Nominator = Nominator;
		Nominator.Valuator = Valuator;

		Memory.DeclareType("Type", typeof(Type));
		Memory.DeclareType("Number", typeof(double));
		Memory.DeclareType("Boolean", typeof(bool));
		Memory.DeclareType("String", typeof(string));
		Memory.DeclareConstant("Number", "pi", PI);
		Memory.DeclareConstant("Number", "e", E);
	}

	public void Evaluate(IEnumerable<Node> trees)
	{
		foreach (Node tree in trees) tree.Accept(Valuator);
	}
}

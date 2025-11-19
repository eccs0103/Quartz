using Quartz.Domain.Parsing;
using static System.Math;

namespace Quartz.Domain.Evaluating;

public class Runtime
{
	private RuntimeBuilder Builder { get; } = new();
	private Evaluator Evaluator { get; } = new();

	public Runtime()
	{
		Builder.DeclareModule(static (module) =>
		{
			module.DeclareClass("Type", static (type) =>
			{
			});
			module.DeclareClass("Function", static (type) =>
			{
			});
			module.DeclareClass("Null", static (type) =>
			{
			});
			module.DeclareClass("Boolean", static (type) =>
			{
			});
			module.DeclareClass("String", static (type) =>
			{
				type.DeclareOperation("+", ["String", "String"], "String", static (string left, string right) =>
				{
					return left + right;
				});
				type.DeclareOperation("=", ["String", "String"], "Boolean", static (string left, string right) =>
				{
					return left == right;
				});
				type.DeclareOperation("!=", ["String", "String"], "Boolean", static (string left, string right) =>
				{
					return left != right;
				});
			});
			module.DeclareClass("Number", static (type) =>
			{
				type.DeclareOperation("+", ["Number"], "Number", static (double target) =>
				{
					return +target;
				});
				type.DeclareOperation("+", ["Number", "Number"], "Number", static (double left, double right) =>
				{
					return left + right;
				});
				type.DeclareOperation("-", ["Number"], "Number", static (double target) =>
				{
					return -target;
				});
				type.DeclareOperation("-", ["Number", "Number"], "Number", static (double left, double right) =>
				{
					return left - right;
				});
				type.DeclareOperation("*", ["Number", "Number"], "Number", static (double left, double right) =>
				{
					return left * right;
				});
				type.DeclareOperation("/", ["Number", "Number"], "Number", static (double left, double right) =>
				{
					return left / right;
				});
				type.DeclareOperation("=", ["Number", "Number"], "Boolean", static (double left, double right) =>
				{
					return left == right;
				});
				type.DeclareOperation("!=", ["Number", "Number"], "Boolean", static (double left, double right) =>
				{
					return left != right;
				});
				type.DeclareOperation("<", ["Number", "Number"], "Boolean", static (double left, double right) =>
				{
					return left < right;
				});
				type.DeclareOperation("<=", ["Number", "Number"], "Boolean", static (double left, double right) =>
				{
					return left <= right;
				});
				type.DeclareOperation(">", ["Number", "Number"], "Boolean", static (double left, double right) =>
				{
					return left > right;
				});
				type.DeclareOperation(">=", ["Number", "Number"], "Boolean", static (double left, double right) =>
				{
					return left >= right;
				});
			});
			module.DeclareClass(RuntimeBuilder.NameWorkspace, static (type) =>
			{
				type.DeclareConstant("pi", "Number", PI);
				type.DeclareConstant("e", "Number", E);
				type.DeclareOperation("read", ["String"], "String", static (string args) =>
				{
					Console.Write(args);
					string? input = Console.ReadLine();
					ArgumentNullException.ThrowIfNull(input);
					return input;
				});
				type.DeclareOperation("write", ["Number"], "Null", static (double args) =>
				{
					Console.WriteLine(args);
				});
				type.DeclareOperation("write", ["Boolean"], "Null", static (bool args) =>
				{
					Console.WriteLine(args);
				});
				type.DeclareOperation("write", ["String"], "Null", static (string args) =>
				{
					Console.WriteLine(args);
				});
			});
		});
	}

	public void Evaluate(IEnumerable<Node> nodes)
	{
		foreach (Node node in nodes) node.Accept(Evaluator, RuntimeBuilder.Workspace);
	}
}

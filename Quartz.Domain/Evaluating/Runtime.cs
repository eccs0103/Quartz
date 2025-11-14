using Quartz.Domain.Parsing;
using static System.Math;

namespace Quartz.Domain.Evaluating;

public class Runtime
{
	private readonly RuntimeBuilder Builder = new();
	private readonly Evaluator Evaluator = new();

	public Runtime()
	{
		Builder.DeclareModule(module =>
		{
			module.DeclareClass("Type", type =>
			{
			});
			module.DeclareClass("Function", type =>
			{
			});
			module.DeclareClass("Boolean", type =>
			{
			});
			module.DeclareClass("String", type =>
			{
			});
			module.DeclareClass("Number", type =>
			{
				type.DeclareOperation("+", ["Number"], "Number", args =>
				{
					double target = args[0].ValueAs<double>();
					return +target;
				});
				type.DeclareOperation("+", ["Number", "Number"], "Number", args =>
				{
					double left = args[0].ValueAs<double>();
					double right = args[1].ValueAs<double>();
					return left + right;
				});
				type.DeclareOperation("-", ["Number"], "Number", args =>
				{
					double target = args[0].ValueAs<double>();
					return -target;
				});
				type.DeclareOperation("-", ["Number", "Number"], "Number", args =>
				{
					double left = args[0].ValueAs<double>();
					double right = args[1].ValueAs<double>();
					return left - right;
				});
				type.DeclareOperation("*", ["Number", "Number"], "Number", args =>
				{
					double left = args[0].ValueAs<double>();
					double right = args[1].ValueAs<double>();
					return left * right;
				});
				type.DeclareOperation("/", ["Number", "Number"], "Number", args =>
				{
					double left = args[0].ValueAs<double>();
					double right = args[1].ValueAs<double>();
					return left / right;
				});
				type.DeclareOperation("=", ["Number", "Number"], "Boolean", args =>
				{
					double left = args[0].ValueAs<double>();
					double right = args[1].ValueAs<double>();
					return left == right;
				});
				type.DeclareOperation("<", ["Number", "Number"], "Boolean", args =>
				{
					double left = args[0].ValueAs<double>();
					double right = args[1].ValueAs<double>();
					return left < right;
				});
				type.DeclareOperation("<=", ["Number", "Number"], "Boolean", args =>
				{
					double left = args[0].ValueAs<double>();
					double right = args[1].ValueAs<double>();
					return left <= right;
				});
				type.DeclareOperation(">", ["Number", "Number"], "Boolean", args =>
				{
					double left = args[0].ValueAs<double>();
					double right = args[1].ValueAs<double>();
					return left > right;
				});
				type.DeclareOperation(">=", ["Number", "Number"], "Boolean", args =>
				{
					double left = args[0].ValueAs<double>();
					double right = args[1].ValueAs<double>();
					return left >= right;
				});
			});
			module.DeclareClass(RuntimeBuilder.NameWorkspace, type =>
			{
				type.DeclareConstant("pi", "Number", PI);
				type.DeclareConstant("e", "Number", E);
				type.DeclareOperation("write", ["Number"], "Number", args =>
				{
					Console.WriteLine(args[0].ToString());
					return 0;
				});
				type.DeclareOperation("write", ["Boolean"], "Number", args =>
				{
					Console.WriteLine(args[0].ToString());
					return 0;
				});
				type.DeclareOperation("write", ["String"], "Number", args =>
				{
					Console.WriteLine(args[0].ToString());
					return 0;
				});
			});
		});
	}

	public void Evaluate(IEnumerable<Node> nodes)
	{
		foreach (Node node in nodes) node.Accept(Evaluator, RuntimeBuilder.Workspace);
	}
}

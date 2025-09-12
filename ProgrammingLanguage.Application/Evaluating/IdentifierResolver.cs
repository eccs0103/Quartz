using ProgrammingLanguage.Application.Abstractions;
using ProgrammingLanguage.Application.Parsing;

namespace ProgrammingLanguage.Application.Evaluating;

#pragma warning disable CS9113 // Parameter is unread.
internal class IdentifierResolver(Registry memory, ValueResolver valuator) : IResolverVisitor<IdentifierNode>
#pragma warning restore CS9113 // Parameter is unread.
{
	public IdentifierNode Visit(ValueNode node)
	{
		throw new NotImplementedException();
	}

	public IdentifierNode Visit(IdentifierNode node)
	{
		return node;
	}

	public IdentifierNode Visit(DeclarationNode node)
	{
		throw new NotImplementedException();
	}

	public IdentifierNode Visit(InvokationNode node)
	{
		throw new NotImplementedException();
	}

	public IdentifierNode Visit(UnaryOperatorNode node)
	{
		throw new NotImplementedException();
	}

	public IdentifierNode Visit(BinaryOperatorNode node)
	{
		throw new NotImplementedException();
	}
}

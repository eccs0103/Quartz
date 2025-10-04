using ProgrammingLanguage.Application.Parsing;

namespace ProgrammingLanguage.Application.Evaluating;

internal interface IAstVisitor<out TReturn>
{
	public TReturn Visit(ValueNode node);
	public TReturn Visit(IdentifierNode node);
	public TReturn Visit(DeclarationNode node);
	public TReturn Visit(AssignmentNode node);
	public TReturn Visit(InvokationNode node);
	public TReturn Visit(UnaryOperatorNode node);
	public TReturn Visit(BinaryOperatorNode node);
	public TReturn Visit(BlockNode node);
	public TReturn Visit(IfStatementNode node);
}

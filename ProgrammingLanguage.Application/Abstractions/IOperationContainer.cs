using ProgrammingLanguage.Application.Evaluating;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Abstractions;

internal interface IOperationContainer
{
	public void RegisterOperation(string name, Operation operation, Range<Position> range);
	public Operation ReadOperation(string name, Range<Position> range);
}

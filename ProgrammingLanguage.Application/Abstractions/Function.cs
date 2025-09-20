using ProgrammingLanguage.Application.Parsing;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Abstractions;

internal delegate Node Function(IdentifierNode nodeOperand, IEnumerable<Node> arguments, Range<Position> range);

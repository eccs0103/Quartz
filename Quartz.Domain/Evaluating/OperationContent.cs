using Quartz.Domain.Parsing;
using Quartz.Shared.Helpers;

namespace Quartz.Domain.Evaluating;

internal delegate ValueNode OperationContent(ValueNode[] arguments, Scope location, Range<Position> range);

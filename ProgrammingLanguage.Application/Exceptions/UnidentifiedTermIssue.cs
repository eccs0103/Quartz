using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Exceptions;

public class UnidentifiedTermIssue(Range<Position> range): Issue("Unidentified term", range)
{
}

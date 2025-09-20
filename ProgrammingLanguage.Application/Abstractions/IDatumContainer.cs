using ProgrammingLanguage.Application.Evaluating;
using ProgrammingLanguage.Shared.Helpers;

namespace ProgrammingLanguage.Application.Abstractions;

internal interface IDatumContainer
{
	public void RegisterDatum(string name, Datum datum, Range<Position> range);
	public Datum ReadDatum(string name, Range<Position> range);
	public void WriteDatum(string name, object? value, Range<Position> range);
}

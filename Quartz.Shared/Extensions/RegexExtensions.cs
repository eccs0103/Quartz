using System.Text.RegularExpressions;

namespace Quartz.Shared.Extensions;

public static class RegexExtensions
{
	public static void Deconstruct(this Match match, out string value, out int length)
	{
		value = match.Value;
		length = match.Length;
	}
}

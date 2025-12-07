using CsvHelper;
using Foster.Framework;
using System.Globalization;

namespace Decursed;

internal static class Utility
{
	public static string[,] ParseCsv(string path, Point2 size)
	{
		var grid = new string[size.X, size.Y];

		using var reader = new StreamReader(path);
		using var parser = new CsvParser(reader, CultureInfo.InvariantCulture);

		for (var y = 0; y < size.Y; y++)
		{
			parser.Read();

			for (var x = 0; x < size.X; x++)
			{
				grid[x, y] = parser.Record![x];
			}
		}

		return grid;
	}
}

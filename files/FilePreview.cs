using System;
using System.IO;
using System.Linq;

namespace FilePreviews;

public class FilePreview
{
	public string PreviewFile(string filePath, int maxLines, int maxWidth)
	{
		try
		{
			long fileSize = new FileInfo(filePath).Length;

			if (fileSize > 5 * 1024 * 1024)
			{
				return "File too large.";
			}

			var lines = File.ReadLines(filePath).Take(maxLines).ToList();

			if (lines.Count == 0)
			{
				return "Empty file";
			}

			var cleanLines = lines.Select(line =>
				line.Length > maxWidth ? line.Substring(0, maxWidth - 3) + "..." : line
				);

			return string.Join("\n", cleanLines);
		} catch (Exception e) { return "Error reading file."; }
	}
}
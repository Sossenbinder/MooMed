using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace FrontendEnumGenerator
{
	public class EnumGenerator
	{
		private List<Type> m_enumsToExport;

		private readonly string m_outputFile;

		private readonly string m_wwwRootPath;

		public EnumGenerator()
		{
			m_enumsToExport = EnumsToExport.Enums;

			var outputDirectory = $"{Assembly.GetExecutingAssembly().Location}\\..\\..\\..\\..\\..\\..\\Services\\MooMed\\React\\Enums";

			m_outputFile = Path.Combine(outputDirectory, "moomedEnums.ts");

			m_wwwRootPath = $"{Assembly.GetExecutingAssembly().Location}\\..\\..\\..\\..\\..\\..\\Services\\MooMed\\wwwroot\\dist\\Enums";
		}

		public void GenerateEnums()
		{
			var output = GetOutputFile();

			foreach (var enumToExport in m_enumsToExport)
			{
				output.WriteLine();
				output.WriteLine($"export enum {enumToExport.Name} {{");

				var enumMembers = Enum.GetNames(enumToExport);
				for (var i = 0; i < enumMembers.Length; ++i)
				{
					output.WriteLine($"	{enumMembers[i]} = {i},");
				}
				output.WriteLine($"}}");
			}

			output.Close();

			CopyTranslationsToWWWRoot();
		}

		[NotNull]
		private StreamWriter GetOutputFile()
		{
			var outputDirectory = $"{Assembly.GetExecutingAssembly().Location}\\..\\..\\..\\..\\..\\..\\Services\\MooMed\\React\\Enums";

			var outputPath = Path.Combine(outputDirectory, "moomedEnums.ts");

			if (!File.Exists(outputPath))
			{
				return new StreamWriter(File.Create(outputPath));
			}

			File.WriteAllText(outputPath, string.Empty);

			return new StreamWriter(outputPath, true);
		}

		private void CopyTranslationsToWWWRoot()
		{
			var fileName = m_outputFile.Substring(m_outputFile.LastIndexOf("\\", StringComparison.Ordinal));

			File.Copy(m_outputFile, m_wwwRootPath + fileName, true);
		}
	}
}

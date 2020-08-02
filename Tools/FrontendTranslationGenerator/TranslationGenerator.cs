using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace FrontendTranslationGenerator
{
	internal class TranslationGenerator
	{
		[NotNull]
		private string _translationPaths;

		[NotNull]
		private string _translationOutputPaths;

		[NotNull]
		private string _wwwRootPath;

		public void GenerateTranslations([NotNull] string solutionDir)
		{
			GetTranslationResourcesPath(solutionDir);
			var relevantTranslations = GetRelevantTranslations();

			GenerateTsDefinitionFile(relevantTranslations);
			GenerateJSTranslationFiles(relevantTranslations);
			CopyTranslationsToWWWRoot();
		}

		private void GetTranslationResourcesPath([NotNull] string solutionDir)
		{
			_translationPaths = $"{solutionDir}\\Common\\MooMed.Core\\Translations\\Resources";
			_translationOutputPaths = $"{solutionDir}\\Services\\MooMed.Frontend\\React\\Translations";
			_wwwRootPath = $"{solutionDir}\\Services\\MooMed.Frontend\\wwwroot\\dist\\Translations";
		}

		private IEnumerable<string> GetRelevantTranslations()
		{
			var translationBase = Path.Combine(_translationPaths, "Translation.resx");

			var xDoc = XDocument.Load(translationBase);

			return xDoc
				.Element("root")
				?.Elements("data")
				.Where(node =>
				{
					if (node.Element("comment") != null)
					{
						return node.Element("comment")?.Value.Equals("{Frontend}") ?? false;
					}

					return false;
				})
				.Select(node => node.FirstAttribute.Value)
				.ToList();
		}

		private IEnumerable<string> GetAvailableTranslationLanguages()
		{
			var allFilesInDir = Directory.GetFiles(_translationPaths);

			return allFilesInDir.Where(fileName => Regex.IsMatch(fileName, "(Translation\\..+\\.resx)")).Select(file =>
			{
				var reversedFile = new string(file.Reverse().ToArray());
				var rereversedFile = new string(reversedFile.Substring(0, reversedFile.IndexOf('\\')).Reverse().ToArray());

				return rereversedFile;
			});
		}

		private void GenerateTsDefinitionFile(IEnumerable<string> relevantTranslations)
		{
			var outputFile = GetOutputFile("Translation.d.ts");
			var enTranslationsPath = GetAvailableTranslationLanguages().Single(x => x.Contains("en.resx"));

			outputFile.WriteLine("export type Translation = {");

			var xDoc = XDocument.Load(Path.Combine(_translationPaths, enTranslationsPath));

			var nodesToCopy = xDoc.Element("root")
				?.Elements("data")
				.Where(node => relevantTranslations.Contains(node.FirstAttribute.Value))
				.Select(node => new
				{
					name = node.FirstAttribute.Value,
					value = node.Element("value").Value
				}
				).ToList();

			if (nodesToCopy != null)
			{
				foreach (var node in nodesToCopy)
				{
					if (node.value != null)
					{
						outputFile.WriteLine($"    \"{node.name}\": \"{node.value}\",");
					}
				}
			}

			outputFile.WriteLine("};");
			outputFile.Close();
		}

		private void GenerateJSTranslationFiles(IEnumerable<string> relevantTranslations)
		{
			var translationFiles = GetAvailableTranslationLanguages();

			foreach (var translationFile in translationFiles)
			{
				var firstIndex = translationFile.IndexOf(".", StringComparison.Ordinal);
				var lastIndex = translationFile.LastIndexOf(".", StringComparison.Ordinal);
				var lang = translationFile.Substring(firstIndex + 1, lastIndex - firstIndex - 1);

				var outputFile = GetOutputFile($"translation.{lang}.js");
				outputFile.WriteLine("var Translation = {");

				var xDoc = XDocument.Load(Path.Combine(_translationPaths, translationFile));

				var nodesToCopy = xDoc.Element("root")
					?.Elements("data")
					.Where(node => relevantTranslations.Contains(node.FirstAttribute.Value))
					.Select(node => new
					{
						name = node.FirstAttribute.Value,
						value = node.Element("value").Value
					}
					).ToList();

				if (nodesToCopy != null)
				{
					foreach (var node in nodesToCopy)
					{
						if (node.value != null)
						{
							outputFile.WriteLine($"    \"{node.name}\": \"{node.value}\",");
						}
					}
				}

				outputFile.WriteLine("};");
				outputFile.Close();
			}
		}

		private StreamWriter GetOutputFile(string filename)
		{
			var outputPath = Path.Combine(_translationOutputPaths, filename);

			if (!File.Exists(outputPath))
			{
				return new StreamWriter(File.Create(outputPath));
			}

			File.WriteAllText(outputPath, string.Empty);

			return new StreamWriter(outputPath, true);
		}

		private void CopyTranslationsToWWWRoot()
		{
			var filesInOutputDir = Directory.GetFiles(_translationOutputPaths);

			foreach (var file in filesInOutputDir)
			{
				var fileName = file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal));

				File.Copy(file, _wwwRootPath + fileName, true);
			}
		}
	}
}
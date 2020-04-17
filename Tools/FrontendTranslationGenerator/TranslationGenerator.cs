﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace FrontendTranslationGenerator
{
    internal class TranslationGenerator
    {
        private string _translationPaths;

        private string _translationOutputPaths;

        private string _wwwRootPath;
        
        public void GenerateTranslations()
        {
            GetTranslationResourcesPath();
            var relevantTranslations = GetRelevantTranslations();

            GenerateJSTranslationFiles(relevantTranslations);
            CopyTranslationsToWWWRoot();
        }

        private void GetTranslationResourcesPath()
        {
            _translationPaths = $"{Assembly.GetExecutingAssembly().Location}\\..\\..\\..\\..\\..\\..\\Common\\MooMed.Core\\Translations\\Resources";
            _translationOutputPaths = $"{Assembly.GetExecutingAssembly().Location}\\..\\..\\..\\..\\..\\..\\Services\\MooMed\\React\\Translations";
            _wwwRootPath = $"{Assembly.GetExecutingAssembly().Location}\\..\\..\\..\\..\\..\\..\\Services\\MooMed\\wwwroot\\dist\\Translations";
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

        private void GenerateJSTranslationFiles(IEnumerable<string> relevantTranslations)
        {
            var translationFiles = GetAvailableTranslationLanguages();

            foreach (var translationFile in translationFiles)
            {
                var firstIndex = translationFile.IndexOf(".", StringComparison.Ordinal);
                var lastIndex = translationFile.LastIndexOf(".", StringComparison.Ordinal);
                var lang = translationFile.Substring(firstIndex + 1, lastIndex - firstIndex - 1);

                var outputFile = GetOutputFile(lang);
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

        private StreamWriter GetOutputFile(string lang)
        {
            var outputPath = Path.Combine(_translationOutputPaths, $"translation.{lang}.js");

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

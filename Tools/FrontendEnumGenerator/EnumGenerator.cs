using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GreenPipes.Internals.Extensions;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Attributes;

namespace FrontendEnumGenerator
{
    public class EnumGenerator
    {
        private readonly List<Type> _enumsToExport;

        private readonly string _outputFile;

        private readonly string _wwwRootPath;

        public EnumGenerator([NotNull] string solutionDir)
        {
            Console.WriteLine($"Running EnumGenerator in {solutionDir}");

            _enumsToExport = EnumsToExport.Enums;

            _enumsToExport.AddRange(AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes().Where(y => y.HasAttribute<ExportEnumAttribute>())));

            var outputDirectory = $"{solutionDir}/Services/MooMed.Frontend/React/Enums";

            _outputFile = Path.Combine(outputDirectory, "moomedEnums.ts");

            _wwwRootPath = $"{solutionDir}/Services/MooMed.Frontend/wwwroot/dist/Enums";
        }

        public void GenerateEnums()
        {
            var output = GetOutputFile();

            foreach (var enumToExport in _enumsToExport)
            {
                output.WriteLine();
                output.WriteLine($"export enum {enumToExport.Name} {{");

                var enumMembers = Enum.GetNames(enumToExport);
                for (var i = 0; i < enumMembers.Length; ++i)
                {
                    output.WriteLine($"	{enumMembers[i]} = {i},");
                }
                output.WriteLine("}");
            }

            output.Close();

            CopyTranslationsToWwwRoot();
        }

        [NotNull]
        private StreamWriter GetOutputFile()
        {
            if (!File.Exists(_outputFile))
            {
                return new StreamWriter(File.Create(_outputFile));
            }

            File.WriteAllText(_outputFile, string.Empty);

            return new StreamWriter(_outputFile, true);
        }

        private void CopyTranslationsToWwwRoot()
        {
            File.Copy(_outputFile, Path.Combine(_wwwRootPath, "moomedEnums.ts"), true);
        }
    }
}
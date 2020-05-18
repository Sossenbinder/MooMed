using System;

namespace FrontendTranslationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            new TranslationGenerator().GenerateTranslations(args[0]);
        }
    }
}

namespace FrontendEnumGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new EnumGenerator(args[0]).GenerateEnums();
        }
    }
}
namespace FrontendEnumGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			new EnumGenerator(args[0]).GenerateEnums();
		}
	}
}

using System.IO;
using Microsoft.AspNetCore.Http.Internal;
using MooMed.DotNet.Extensions;
using NUnit.Framework;

namespace MooMed.DotNet.Tests.Extensions
{
	[TestFixture]
	public class HttpPostedFileBaseExtensionsTests
	{
		[Test]
		public void GetFileExtensionShouldWorkForHappyPath()
		{
			const string extension = "txt";
			var formFile = new FormFile(Stream.Null, 0, 0, "FormFile", $"file.{extension}");

			var fileExtension = formFile.GetFileExtension();

			Assert.AreEqual(fileExtension, extension);
		}

		[Test]
		public void GetFileExtensionShouldWorkForHappyPathWithLowerCase()
		{
			const string extension = "txt";
			var formFile = new FormFile(Stream.Null, 0, 0, "FormFile", $"file.{extension.ToUpper()}");

			var fileExtension = formFile.GetFileExtension();

			Assert.AreEqual(fileExtension, extension);
		}

		[Test]
		public void GetFileExtensionShouldWorkForHappyPathWithoutLowerCase()
		{
			const string extension = "TXT";
			var formFile = new FormFile(Stream.Null, 0, 0, "FormFile", $"file.{extension}");

			var fileExtension = formFile.GetFileExtension(false);

			Assert.AreEqual(fileExtension, extension);
		}
	}
}
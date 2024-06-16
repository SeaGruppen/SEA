using AutoFixture;
using Model.DatabaseModule;
using Model.FrontEndAPI;
using Model.Survey;
using Model.UserValidationModule;
using Moq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tests.Backend.FrontEndAPI
{
	internal class FrontEndExperimenterTests
	{
		private Fixture _fixture;

		public FrontEndExperimenterTests()
		{
			_fixture = new Fixture();
		}

		[Test]
		public void TestExportResults()
		{
			var database = new Model.DatabaseModule.Database();

			var sut = new FrontEndExperimenter(database);

			var sutRes = sut.ExportResults(0, "password");

			Assert.False(sutRes);
		}
	}
}


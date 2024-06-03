using Model.Survey;

namespace Tests.Backend.SurveyTests
{
    internal class SurveyWrapperTests
    {
        [Test]
        public void TestAddNewVersion()
        {
            var sut = new SurveyWrapper(1);

            var res1 = sut.AddNewVersion();
            var res2 = sut.AddNewVersion();

            Assert.That(sut.SurveyVersions.Count, Is.EqualTo(2));
            Assert.That(res1.SurveyId, Is.EqualTo("1.0"));
            Assert.That(res2.SurveyId, Is.EqualTo("1.1"));
        }

        [Test]
        public void TestDeleteVersion()
        {
            var sut = new SurveyWrapper(1);

            var res1 = sut.AddNewVersion();
            var res2 = sut.AddNewVersion();
            res2.SurveyName = "survey name";

            sut.DeleteVersion(0);

            Assert.That(sut.SurveyVersions.Count, Is.EqualTo(1));
            Assert.That(sut.SurveyVersions[0], Is.EqualTo(res2));
        }

        [Test]
        public void TestDeleteVersionOutOfRange()
        {
            var sut = new SurveyWrapper(1);

            var res1 = sut.AddNewVersion();
            var res2 = sut.AddNewVersion();

            Assert.DoesNotThrow(() => sut.DeleteVersion(5));

            Assert.That(sut.SurveyVersions.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestTryGetModifySurveyVersion()
        {
            var sut = new SurveyWrapper(1);

            sut.AddNewVersion();
            sut.AddNewVersion();
            var version = sut.AddNewVersion();

            version.SurveyName = "survey name";

            var res = sut.TryGetModifySurveyVersion(2);

            Assert.That(res, Is.EqualTo(version));
        }

        [Test]
        public void TestTryGetModifySurveyVersionIndexOutOfRange()
        {
            var sut = new SurveyWrapper(1);

            sut.AddNewVersion();

            var res = sut.TryGetModifySurveyVersion(2);

            Assert.That(res, Is.EqualTo(null));
        }

        [Test]
        public void TestTryGetReadOnlySurveyVersion()
        {
            var sut = new SurveyWrapper(1);

            sut.AddNewVersion();
            sut.AddNewVersion();
            var version = sut.AddNewVersion();

            version.SurveyName = "survey name";

            var res = sut.TryGetReadOnlySurveyVersion(2);

            Assert.That(res, Is.EqualTo(version));
        }

        [Test]
        public void TestTryGetReadOnlySurveyVersionIndexOutOfRange()
        {
            var sut = new SurveyWrapper(1);

            sut.AddNewVersion();

            var res = sut.TryGetReadOnlySurveyVersion(2);

            Assert.That(res, Is.EqualTo(null));
        }
    }
}

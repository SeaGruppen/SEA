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


        [Test]
        public void TestCopyVersion()
        {
            var qCaption = "What animal is this?";
            var qAnswerOption1 = "Cat";
            var qAnswerOption2 = "Dog";

            var sw = new SurveyWrapper(0);
            var s1 = sw.AddNewVersion();
            var s1mq1 = s1.AddNewMultiQuestion();
            var s1mq1q1 = s1mq1.AddQuestion();
            s1mq1q1.ModifyCaption = qCaption;
            s1mq1q1.ModifyAnswer.AddAnswerOption(qAnswerOption1);
            s1mq1q1.ModifyAnswer.AddAnswerOption(qAnswerOption2);
            
            var s2 = sw.CopyVersion(0);
            var s2mq1 = s2.TryGetNextModifyMultiQuestion();
            var s2mq1E = s2mq1.GetEnumerator();
            s2mq1E.MoveNext();
            var s2mq1q1 = s2mq1E.Current; 

            Assert.Multiple(() =>
            {
                Assert.That(s1.SurveyId, Is.EqualTo("0.0"));
                Assert.That(s2.SurveyId, Is.EqualTo("0.1"));

                Assert.That(s1mq1.MultiQuestionId, Is.EqualTo("0.0.0"));
                Assert.That(s2mq1.MultiQuestionId, Is.EqualTo("0.1.0"));

                Assert.That(s1mq1q1.QuestionId, Is.EqualTo("0.0.0.0"));
                Assert.That(s2mq1q1.QuestionId, Is.EqualTo("0.1.0.0"));


                Assert.That(s2mq1q1.ModifyCaption, Is.EqualTo(qCaption));
                Assert.That(s2mq1q1.ModifyAnswer.ModifyAnswers[0], Is.EqualTo(qAnswerOption1));
                Assert.That(s2mq1q1.ModifyAnswer.ModifyAnswers[1], Is.EqualTo(qAnswerOption2));
            });

            var newCaption = "Which animal do you prefer?";
            s2mq1q1.ModifyCaption = newCaption;

            Assert.Multiple(() =>
            {
                Assert.That(s1mq1q1.ModifyCaption, Is.EqualTo(qCaption));
                Assert.That(s2mq1q1.ModifyCaption, Is.EqualTo(newCaption));
            });

        }
    }
}

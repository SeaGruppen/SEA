using Survey = Model.Survey.Survey;


namespace tests.Model.SurveyTests
{
    internal class SurveyTests
    {
        [Test]
        public void TestAddNewMultiQuestion()
        {
            var sut = new Survey("1");

            var res1 = sut.AddNewMultiQuestion();
            var res2 = sut.AddNewMultiQuestion();

            Assert.That(sut.SurveyQuestions.Count, Is.EqualTo(2));
            Assert.That(res1.MultiQuestionId, Is.EqualTo("1.0"));
            Assert.That(res2.MultiQuestionId, Is.EqualTo("1.1"));
        }

        [Test]
        public void TestAddInsertQuestion()
        {
            var sut = new Survey("1");

            var res1 = sut.InsertNewMultiQuestion(0);
            var res2 = sut.InsertNewMultiQuestion(0);
            var res3 = sut.InsertNewMultiQuestion(1);

            Assert.That(res1.MultiQuestionId, Is.EqualTo("1.0"));
            Assert.That(res2.MultiQuestionId, Is.EqualTo("1.1"));
            Assert.That(res3.MultiQuestionId, Is.EqualTo("1.2"));
            Assert.That(sut.SurveyQuestions[0].MultiQuestionId, Is.EqualTo("1.1"));
            Assert.That(sut.SurveyQuestions[1].MultiQuestionId, Is.EqualTo("1.2"));
            Assert.That(sut.SurveyQuestions[2].MultiQuestionId, Is.EqualTo("1.0"));
        }

        [Test]
        public void TestDeleteQuestion()
        {
            var sut = new Survey("1");

            var res = sut.AddNewMultiQuestion();
            sut.AddNewMultiQuestion();

            sut.DeleteQuestion(0);

            Assert.That(sut.SurveyQuestions[0].MultiQuestionId, Is.EqualTo(res.MultiQuestionId));
        }

        [Test]
        public void TestTryGetModifyMultiQuestion()
        {
            var sut = new Survey("1");

            var question = sut.AddNewMultiQuestion();

            var res = sut.TryGetModifyMultiQuestion(0);

            Assert.That(res, Is.EqualTo(question));
        }

        [Test]
        public void TestTryGetNextModifyMultiQuestion()
        {
            var sut = new Survey("1");

            var question1 = sut.AddNewMultiQuestion();
            var question2 = sut.AddNewMultiQuestion();
            var question3 = sut.AddNewMultiQuestion();

            Assert.That(sut.TryGetNextModifyMultiQuestion(), Is.EqualTo(question1));
            Assert.That(sut.TryGetNextModifyMultiQuestion(), Is.EqualTo(question2));
            Assert.That(sut.TryGetNextModifyMultiQuestion(), Is.EqualTo(question3));
            Assert.That(sut.TryGetNextModifyMultiQuestion(), Is.EqualTo(null));
        }

        [Test]
        public void TestTryGetPreviousModifyMultiQuestion()
        {
            var sut = new Survey("1");

            var question1 = sut.AddNewMultiQuestion();
            var question2 = sut.AddNewMultiQuestion();
            var question3 = sut.AddNewMultiQuestion();
            sut.TryGetNextModifyMultiQuestion();
            sut.TryGetNextModifyMultiQuestion();
            sut.TryGetNextModifyMultiQuestion();
            sut.TryGetNextModifyMultiQuestion();

            Assert.That(sut.TryGetPreviousModifyMultiQuestion(), Is.EqualTo(question2));
            Assert.That(sut.TryGetPreviousModifyMultiQuestion(), Is.EqualTo(question1));
            Assert.That(sut.TryGetPreviousModifyMultiQuestion(), Is.EqualTo(null));
        }

        [Test]
        public void TestTryGetNextReadOnlyQuestion()
        {
            var sut = new Survey("1");

            var question1 = sut.AddNewMultiQuestion();
            var question2 = sut.AddNewMultiQuestion();
            var question3 = sut.AddNewMultiQuestion();

            Assert.That(sut.TryGetNextReadOnlyQuestion(), Is.EqualTo(question1));
            Assert.That(sut.TryGetNextReadOnlyQuestion(), Is.EqualTo(question2));
            Assert.That(sut.TryGetNextReadOnlyQuestion(), Is.EqualTo(question3));
            Assert.That(sut.TryGetNextReadOnlyQuestion(), Is.EqualTo(null));
        }

        [Test]
        public void TestTryGetPreviousReadOnlyQuestion()
        {
            var sut = new Survey("1");

            var question1 = sut.AddNewMultiQuestion();
            var question2 = sut.AddNewMultiQuestion();
            var question3 = sut.AddNewMultiQuestion();
            sut.TryGetNextReadOnlyQuestion();
            sut.TryGetNextReadOnlyQuestion();
            sut.TryGetNextReadOnlyQuestion();
            sut.TryGetNextReadOnlyQuestion();

            Assert.That(sut.TryGetPreviousReadOnlyQuestion(), Is.EqualTo(question2));
            Assert.That(sut.TryGetPreviousReadOnlyQuestion(), Is.EqualTo(question1));
            Assert.That(sut.TryGetPreviousReadOnlyQuestion(), Is.EqualTo(null));
        }
    }
}

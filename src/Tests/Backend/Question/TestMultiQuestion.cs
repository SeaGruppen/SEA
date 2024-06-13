using Model.Question;

namespace Tests.Backend.Question
{
    public class TestMultiQuestion
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestAddQuestion()
        {
            var sut = new MultiQuestion("1");

            var res1 = ((IMultiQuestion<IModifyQuestion>) sut).AddQuestion();

            Assert.That(res1!.QuestionId, Is.EqualTo("1.0"));

            var res2 = ((IMultiQuestion<IModifyQuestion>)sut).AddQuestion();

            Assert.That(res2!.QuestionId, Is.EqualTo("1.1"));
        }

        [Test]
        public void TestAddInsertQuestion()
        {
            var sut = new MultiQuestion("1");

            sut.InsertQuestion(0);
            sut.InsertQuestion(0);
            sut.InsertQuestion(1);

            Assert.That(sut.Questions.Count, Is.EqualTo(3));
            Assert.That(sut.Questions[0].QuestionId, Is.EqualTo("1.1"));
            Assert.That(sut.Questions[1].QuestionId, Is.EqualTo("1.2"));
            Assert.That(sut.Questions[2].QuestionId, Is.EqualTo("1.0"));
        }

        [Test]
        public void TestDeleteQuestion()
        {
            var sut = new MultiQuestion("1");

            sut.InsertQuestion(0);
            sut.InsertQuestion(0);
            sut.DeleteQuestion(1);

            Assert.That(sut.Questions.Count, Is.EqualTo(1));
        }
    }
}

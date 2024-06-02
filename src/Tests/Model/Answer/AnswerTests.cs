using Answer = Model.Answer.Answer;

namespace tests.Backend.AnswerTests
{
    internal class AnswerTests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestAddAnswerOption()
        {
            var sut = new Answer();

            sut.AddAnswerOption("A");
            sut.AddAnswerOption("B");
            sut.AddAnswerOption("C");

            Assert.IsTrue(sut.ModifyAnswers.Count == 3);
            Assert.IsTrue(sut.ModifyAnswers[0] == "A");
            Assert.IsTrue(sut.ModifyAnswers[1] == "B");
            Assert.IsTrue(sut.ModifyAnswers[2] == "C");
        }

        [Test]
        public void TestAddAnswerOptionIndex()
        {
            var sut = new Answer();

            sut.AddAnswerOption("A");
            sut.AddAnswerOption("B");
            sut.AddAnswerOption("C");
            sut.AddAnswerOption("D", 2);
            sut.AddAnswerOption("E", 10);

            Assert.IsTrue(sut.ModifyAnswers.Count == 5);
            Assert.IsTrue(sut.ModifyAnswers[0] == "A");
            Assert.IsTrue(sut.ModifyAnswers[1] == "B");
            Assert.IsTrue(sut.ModifyAnswers[2] == "D");
            Assert.IsTrue(sut.ModifyAnswers[3] == "C");
            Assert.IsTrue(sut.ModifyAnswers[4] == "E");
        }

        [Test]
        public void TestDeleteAnswerOption()
        {
            var sut = new Answer();

            sut.AddAnswerOption("A");
            sut.AddAnswerOption("B");
            sut.AddAnswerOption("C");

            var res = sut.TryDeleteAnswerOption(1);

            Assert.IsTrue(res);
            Assert.IsTrue(sut.ModifyAnswers.Count == 2);
            Assert.IsTrue(sut.ModifyAnswers[0] == "A");
            Assert.IsTrue(sut.ModifyAnswers[1] == "C");

            res = sut.TryDeleteAnswerOption(4);
            Assert.IsFalse(res);

            res = sut.TryDeleteAnswerOption(-1);
            Assert.IsFalse(res);
            Assert.IsTrue(sut.ModifyAnswers.Count == 2);
            Assert.IsTrue(sut.ModifyAnswers[0] == "A");
            Assert.IsTrue(sut.ModifyAnswers[1] == "C");
        }
    }
}

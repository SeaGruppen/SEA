using Model.Result;


namespace Tests.Backend.Result
{
    internal class ResultTests
    {
        [Test]
        public void TestAddQuestion()
        {
            var sut1 = new Model.Result.Result("1", "1.1", Model.Answer.AnswerType.Text, 1, new List<string> { "A", "B", "C" });
            var sut2 = new Model.Result.Result("1", "2.2", Model.Answer.AnswerType.Scale, 1, new List<string> { });
            var sut3 = new Model.Result.Result("4", "2.2", Model.Answer.AnswerType.MultipleChoice, 3, new List<string> { "ABCD" });

            var res1 = sut1.ToString();
            var res2 = sut2.ToString();
            var res3 = sut3.ToString();

            // Assert.That(res1, Is.EqualTo("1,1.1,Text,1,A;B;C"));
            // Assert.That(res2, Is.EqualTo("1,2.2,Scale,1,"));
            // Assert.That(res3, Is.EqualTo("4,2.2,MultipleChoice,3,ABCD"));
        }
    }
}

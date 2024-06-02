using Model.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Model.SurveyTests
{
    internal class SurveyWrapperTests
    {
        [Test]
        public void TestAddNewVersion()
        {
            var sut = new Survey("1");

            var res1 = sut.AddNewMultiQuestion();
            var res2 = sut.AddNewMultiQuestion();

            Assert.That(sut.SurveyQuestions.Count, Is.EqualTo(2));
            Assert.That(res1.MultiQuestionId, Is.EqualTo("1.0"));
            Assert.That(res2.MultiQuestionId, Is.EqualTo("1.1"));
        }
    }
}

namespace Tests.Backend.Question;

using Question = Model.Question.Question;
using Model.Utilities;
using Model.Answer;

[TestFixture]
internal class TestQuestion {

    string projectPath;
    
    [SetUp]
    public void Setup() { 
        projectPath = FileIO.GetProjectPath();
    }

    [Test]
    public void TestQuestionConstructor() {
        string id = "1";
        var sut = new Question(id);
        Assert.That(sut.QuestionId, Is.EqualTo(id));
        Assert.That(sut.ReadOnlyCaption, Is.EqualTo(""));
        Assert.That(sut.ReadOnlyPicture, Is.EqualTo(""));
        Assert.That(sut.ReadOnlyText, Is.EqualTo(""));
    }

    [Test]
    public void TestQuestionModifyCaption() {
        var sut = new Question("1");
        Assert.That(sut.ModifyCaption, Is.EqualTo(""));
        string caption = "testCaption";
        sut.ModifyCaption = caption;
        Assert.That(sut.ModifyCaption, Is.EqualTo(caption));
    }

    [Test]
    public void TestQuestionModifyText() {
        var sut = new Question("1");
        Assert.That(sut.ModifyText, Is.EqualTo(""));
        string text = "text";
        sut.ModifyText = text;
        Assert.That(sut.ModifyText, Is.EqualTo(text));
    }

    [Test]
    public void TestQuestionModifyPictureWhenPicture() {
        var sut = new Question("1");
        string picture = "picture";
        Assert.That(sut.ModifyPicture, Is.EqualTo(""));
        sut.ModifyPicture = picture;
        string expected = Path.Combine(projectPath, picture);
        Assert.That(sut.ReadOnlyPicture, Is.EqualTo(expected));
    }
    
}


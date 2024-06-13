namespace Model.Question;
using System.Text.Json.Serialization;
using Answer;

public class Question : IReadOnlyQuestion, IModifyQuestion {
    
    [JsonInclude]
    private string id;

    [JsonInclude]
    private string caption;

    [JsonInclude]
    private string picture;
    [JsonInclude]
    private string text;
    [JsonInclude]
    private Answer answer;

    public string ReadOnlyCaption => caption;

    public string ReadOnlyPicture => picture;

    public string ReadOnlyText => text;

    public IReadOnlyAnswer ReadOnlyAnswer => answer;

    public string QuestionId => id;

    public string ModifyCaption { get => caption; set => caption = value; }
    public string ModifyPicture { get => picture; set => picture = value; }
    public string ModifyText { get => text; set => text = value; }
    public IModifyAnswer ModifyAnswer { get => answer; }

    public Question(string id) {
        this.id = id;
        caption = string.Empty;
        picture = string.Empty;
        text = string.Empty;
        answer = new Answer();
    }

    internal void UpdateId(string newQuestionId) {
        id = newQuestionId;
    }
}
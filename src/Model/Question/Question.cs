namespace Model.Question;
using System.Text.Json.Serialization;
using Answer;
using Utilities;

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
    private string? localProjectPath;

    public string ReadOnlyCaption => caption;

    public string ReadOnlyPicture => GetLocalPicturePath();

    public string ReadOnlyText => text;

    public IReadOnlyAnswer ReadOnlyAnswer => answer;

    public string QuestionId => id;

    public string ModifyCaption { get => caption; set => caption = value; }
    public string ModifyPicture { 
        get => GetLocalPicturePath();
        set => picture = value; 
    }
    public string ModifyText { get => text; set => text = value; }
    public IModifyAnswer ModifyAnswer { get => answer; }

    public Question(string id) {
        this.id = id;
        caption = string.Empty;
        picture = string.Empty;
        text = string.Empty;
        answer = new Answer();
        localProjectPath = FileIO.GetProjectPath();
    }

    internal void UpdateId(string newQuestionId) {
        id = newQuestionId;
    }

    private string GetLocalPicturePath() {
        if (string.IsNullOrEmpty(picture) || string.IsNullOrEmpty(localProjectPath)) {
            return picture;
        }
        return Path.Combine(localProjectPath, picture);
    }
}
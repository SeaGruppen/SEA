namespace Model.Question;

using Model.Answer;
using Model.Utilities;
using System.Text.Json.Serialization;

public class Question : IReadOnlyQuestion, IModifyQuestion {
    
    [JsonInclude]
    private string id;

    [JsonInclude]
    private string caption;

    [JsonInclude]
    private string questionText;
    [JsonInclude]
    private string picture;
    [JsonInclude]
    private string text;
    [JsonInclude]
    private Answer answer;
    private string databasePath;

    public string ReadOnlyCaption => caption;

    public string ReadOnlyPicture => GetLocalPicturePath(picture);

    public string ReadOnlyText => text;

    public IReadOnlyAnswer ReadOnlyAnswer => answer;

    public string QuestionId => id;

    public string ModifyCaption { get => caption; set => caption = value; }
    public string ModifyPicture 
    { 
        get =>  GetLocalPicturePath(picture);
        set => picture = value; 
    }
    public string ModifyText { get => text; set => text = value; }
    public IModifyAnswer ModifyAnswer { get => answer; }

    public Question(string id) {
        this.id = id;
        caption = string.Empty;
        questionText = string.Empty;
        picture = string.Empty;
        text = string.Empty;
        answer = new Answer();

        string? localProjectPath = FileIO.GetProjectPath();
        if (localProjectPath != null)
        {
            databasePath = Path.Combine(localProjectPath, "surveyDatabase");
        }
        else
        {
            databasePath = "surveyDatabase";
        }
        
    }

    private string GetLocalPicturePath(string picture) {
        return Path.Combine(databasePath, picture);
    }
    internal void UpdateId(string newQuestionId) {
        id = newQuestionId;
    }
}
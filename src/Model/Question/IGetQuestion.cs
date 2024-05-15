namespace Question;

using System.Drawing;

public interface IGetQuestion
{
    int GetId { get; }
    QuestionType GetType { get; }
    string GetCaption { get; }
    string GetPicture { get; }
    string GetText { get; }
}

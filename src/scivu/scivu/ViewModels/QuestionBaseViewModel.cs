using Model.Answer;

namespace scivu.ViewModels;

/// <summary>
/// Used to distinguish question view models from other.
/// </summary>
public abstract class QuestionBaseViewModel : ViewModelBase
{
    public abstract string GetAnswer();
    public abstract AnswerType GetQuestionType();
    public abstract int GetId();
    public abstract void SetResult(string result);
}
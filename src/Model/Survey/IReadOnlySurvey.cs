namespace Model.Survey;
using Question;

public interface IReadOnlySurvey {
    /// <summary>
    /// Gets the id of the survey
    /// </summary>
    string SurveyId {get;}

    /// <summary>
    /// Gets the survey name
    /// </summary>
    string SurveyName {get;}

    /// <summary>
    /// Based on current question pointer
    /// </summary>
    /// <returns>True if a previus question exists otherwise false</returns>
    bool PreviousQuestionExist();

    /// <summary>
    /// Based on current question pointer
    /// </summary>
    /// <returns>True if a next question exists otherwise false</returns>
    bool NextQuestionExist();

    /// <summary>
    /// Based on current question pointer
    /// </summary>
    /// <returns>Returns the next read-only question if a previus question exists otherwise null</returns>
    IEnumerable<IReadOnlyQuestion>? TryGetNextReadOnlyQuestion();

    /// <summary>
    /// Based on current question pointer
    /// </summary>
    /// <returns>Returns the previous read-only question if a previus question exists otherwise null</returns>
    IEnumerable<IReadOnlyQuestion>? TryGetPreviousReadOnlyQuestion();

    /// <summary>
    /// Resets the question pointer to 0
    /// </summary>
    void ResetCounter();
}
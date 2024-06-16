namespace Model.FrontEndAPI;
using Result;

public interface IFrontEndExperimenter {
    
    /// <summary>
    /// Stores a result from a question in the database which is used by the FrontEndExperimenterMenu.
    /// </summary>
    /// <param name="answer">The result to store. which is of type IResult, found in Model/IResult.cs</param>
    /// <returns>void</returns>
    void StoreResultFromQuestion(IResult answer);

    /// <summary>
    /// Request next user id.
    /// </summary>
    /// <returns>uint</returns>
    uint GetNextUserId();

    /// <summary>
    /// Export results related to a SurveyWrapper to the path given as input. It returns true if the export was successful, false otherwise.
    /// The results are exported in a .csv file with the following format:
    /// One result pr line, a line contains the following columns:
    /// SurveyId, QuestionId, AnswerType, UserId, CreationTime, QuestionResult: List<string>
    /// Returns true if the export was successful, false otherwise.
    /// OBS: The file 'SurveyId.csv' is overwritten if it already exists, to include newest data.
    /// </summary>
    /// <param name="surveyWrapperId">The id of the SurveyWrapper to export results from.</param>
    /// <param name="folderPath">The path to export the results to.</param>
    /// <returns>bool</returns>
    bool ExportResults(int surveyWrapperId, string folderPath);
}
namespace Model.Utilities;
using Model.Survey;
// using Model.Multiquestion;
using Model.Question;
public static class ExtractSurveyDetails {

    /// Use to get surveyWrapperId from sub-element's Id
    public static int GetSurveyWrapperId(string subElement) {
        if (string.IsNullOrWhiteSpace(subElement) || !subElement.Contains('.')) {
            throw new ArgumentException("Invalid subElement", nameof(subElement));
        }

        int result;
        if (!Int32.TryParse(subElement.Split('.')[0], out result)){
            throw new ArgumentException("Invalid subElement", nameof(subElement));
        }
        return result;
    }
    public static string GetSurveyId(string subElement) {
        // Check that subElement is not null and that it contains a period
        if (string.IsNullOrWhiteSpace(subElement) || !subElement.Contains('.')) {
            throw new ArgumentException("Invalid subElement", nameof(subElement));
        }
        string[] parts = subElement.Split('.');
        // Validate that the SurveyWrapperId is an integer
        if (!Int32.TryParse(subElement.Split('.')[0], out _)){
            throw new ArgumentException("Invalid SurveyWrapperId, is not int.", nameof(subElement));
        }
        string result = string.Join(".", parts.Take(2));
        return result;
    }    
    public static string GetMultiQuestionId(string subElement) {
        //Check that subElement is not null and that it contains a period
        if (string.IsNullOrWhiteSpace(subElement) || !subElement.Contains('.')) {
            throw new ArgumentException("Invalid subElement", nameof(subElement));
        }
        string[] parts = subElement.Split('.');
        //Check that MultiQuestion contains 3 Id parts
        if (parts.Length < 3) {
            throw new ArgumentException("Invalid subElement", nameof(subElement));
        }
        //Validate that the SurveyWrapper Id is an integer
        if (!Int32.TryParse(subElement.Split('.')[0], out _)){
            throw new ArgumentException("Invalid SurveyWrapperId, is not int.", nameof(subElement));
        }
        string result = string.Join(".", parts.Take(3));
        return result;
    }
}
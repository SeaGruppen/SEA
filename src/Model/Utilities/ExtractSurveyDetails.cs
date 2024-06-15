namespace Model.Utilities;

internal static class ExtractSurveyDetails {

    /// <summary>
    /// Use to get surveyWrapperId from sub-element's Id.
    /// It throws an exception if the subElement is null or does not contain a period.
    /// </summary>
    internal static int TryGetSurveyWrapperId(string subElement) {
        if (string.IsNullOrWhiteSpace(subElement) || !subElement.Contains('.')) {
            throw new ArgumentException("Invalid subElement", nameof(subElement));
        }

        int result;
        if (!Int32.TryParse(subElement.Split('.')[0], out result)){
            throw new ArgumentException("Invalid subElement", nameof(subElement));
        }
        return result;
    }

    /// <summary>
    /// Use to get surveyId from sub-element's Id
    /// It throws an exception if the subElement is null or does not contain a period.
    /// </summary>
    internal static string TryGetSurveyId(string subElement) {
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

    /// <summary>
    /// Use to get multiQuestionId from sub-element's Id
    /// It throws an exception if the subElement is null or does not contain a period.
    /// </summary>
    internal static string TryGetMultiQuestionId(string subElement) {
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
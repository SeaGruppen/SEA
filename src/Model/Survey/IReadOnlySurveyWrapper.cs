namespace Model.Survey;
using System.Collections.Generic;

public interface IReadOnlySurveyWrapper {

    IReadOnlySurveyWrapper TryGetReadOnlySurveyWrapper(int surveyId); // List<SurveyA,SurveyB>

    IReadOnlySurvey TryGetReadOnlySurveyVersion(int index); // Return survey index'

    int GetVersionCount(); // Return number of versions

}

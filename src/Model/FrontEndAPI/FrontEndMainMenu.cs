namespace Model.FrontEndAPI;

using Model.Survey;

    public static class FrontEndMainMenu {
        public static bool ValidateSuperUser(string username, string password) {
            return false;
        }
        public static IReadOnlySurvey? GetSurvey(int surveyId) {
            return null;
        }
    }
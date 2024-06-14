namespace Model.Factory;

using DatabaseModule;
using FrontEndAPI;
using Result;
using Answer;
using StatisticsModule;
using UserValidation;

public static class FrontEndFactory {

    private static Database databaseServices = new Database();
    private static ISuperUserValidator superUserValidator = new SuperUserValidator();
    public static IFrontEndMainMenu CreateMainMenu() {
        return new FrontEndMainMenu(databaseServices, superUserValidator);
    }

    public static IFrontEndExperimenter CreateExperimenterMenu() {
        return new FrontEndExperimenter(databaseServices);
    }

    public static IFrontEndSuperUser CreateSuperUserMenu() {
        return new FrontEndSuperUserMenu(databaseServices, superUserValidator);
    }

    public static IStatistics CreateStatistics() {
        return new Statistics(databaseServices);
    }

    public static IResult CreateResult(
        string surveyId,
        string questionId,
        AnswerType type,
        int userId,
        List<string> questionResult) => new Result(surveyId, questionId, type, userId, questionResult);
}
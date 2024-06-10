namespace Model.Result;

using Model.Answer;
using System.Text;

internal class Result : IResult {
    public AnswerType AnswerType {get; private set;} 
    public List<string> QuestionResult {get; set;}

    public int UserId {get; private set;}

    public string QuestionId {get; private set;}

    public string SurveyId {get; private set;}

    public DateTime CreationTime {get; private set;}

    public Result (string surveyId, string questionId, AnswerType type, int userId, List<string> questionResult) {
        AnswerType = type;
        QuestionResult = questionResult;
        UserId = userId;
        QuestionId = questionId;
        SurveyId = surveyId;
        CreationTime = DateTime.Now;
    }
    public static Result FromString(string resultString) {
        string[] parts = resultString.Split(',');
        Result result =  new Result(parts[0], parts[1], (AnswerType)Enum.Parse(typeof(AnswerType), parts[2]), int.Parse(parts[3]), new List<string>(parts[4].Split(";")));
        return result;
    }

    public override string ToString() {
        return $"{SurveyId},{QuestionId},{AnswerType},{UserId}, {CreationTime}, {Pretty(QuestionResult)}";
    }

    private static string Pretty(List<string> lst)
    {
        var sb = new StringBuilder();
        foreach (var item in lst)
        {
            // We escape all ';' char to as not confuse with our
            // separator, meaning we also escape all escape char ('\')
            // to handle answer ending in such a char i.e. avoiding
            // answer1\;answer2
            // but creating
            // answer1\\;answer2
            var cleanItem = EscapeSpecials(item);
            sb.Append(cleanItem).Append(';');
        }

        // Remove the last ';'
        if (sb.Length > 0) sb.Length--;
        return sb.ToString();
    }

    private static string EscapeSpecials(string str)
    {
        if (string.IsNullOrEmpty(str)) return str;

        // Assuming we only have to escape a small amount of characters
        // so we pre-allocate same length
        var sb = new StringBuilder(str.Length);

        foreach (var c in str)
        {
            switch (c)
            {
                case ';':
                    sb.Append(@"\;");
                    break;
                case '\\':
                    sb.Append(@"\\");
                    break;
                default:
                    sb.Append(c);
                    break;
            }
        }

        return sb.ToString();
    }
}

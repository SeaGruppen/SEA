using System.Text.Json;

public static class Globals {
    public static JsonSerializerOptions OPTIONS = new JsonSerializerOptions { 

        // pretty printing
        WriteIndented = true, 

        // necessary to serialize the fields of MultiQuestion (because of IEnumerable)
        Converters = {
            new Model.Question.MultiQuestionConverter()
        } 
    };
}
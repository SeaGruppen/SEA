namespace Model.Question;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// This class is used to convert a MultiQuestion object to and from JSON.
/// This class can be deleted if the DB changes from storing in JSON format.
/// </summary>

internal class MultiQuestionConverter : JsonConverter<MultiQuestion> {
    public override MultiQuestion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ref reader, options);
        var multiQuestion = new MultiQuestion(dict["MultiQuestionId"].GetString());
        multiQuestion.NextQuestionId = dict["NextQuestionId"].GetInt32();
        multiQuestion.questions = JsonSerializer.Deserialize<List<Question>>(dict["Questions"].GetRawText(), options);
        return multiQuestion;
    }

    public override void Write(Utf8JsonWriter writer, MultiQuestion value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("MultiQuestionId", value.MultiQuestionId);
        writer.WriteNumber("NextQuestionId", value.NextQuestionId);
        writer.WritePropertyName("Questions");
        JsonSerializer.Serialize(writer, value.questions, options);
        writer.WriteEndObject();
    }
}
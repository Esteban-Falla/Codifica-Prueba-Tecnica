namespace SDP_WebAPI.Models;

public class EmployeeModel : IElement
{
    [Required]
    [Range(0, int.MaxValue)]
    [JsonPropertyName("EmployeeId")]
    public int Id { get; init; }

    [Required]
    [JsonPropertyName("FullName")]
    public string Name { get; init; }

    private EmployeeModel()
    {
    }

    public static T FromADOReader<T>(SqlDataReader reader)
        where T : IElement
    {
        var result = new EmployeeModel()
        {
            Id = reader.GetInt32(
                reader.GetOrdinal("Empid")),
            Name = reader.GetString(
                reader.GetOrdinal("FullName"))
        };
        return (T)Convert.ChangeType(result, typeof(T));
    }
}
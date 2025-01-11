using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class BaseModel
{
    [Key]
    public Guid Id { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
    [JsonIgnore]
    public bool IsActive { get; set; } = true;
}
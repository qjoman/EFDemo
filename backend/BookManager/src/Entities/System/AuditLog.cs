using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class AuditLog
{
    [Key]
    public Guid Id { get; set; }
    public string EntityName { get; set; }
    public Guid EntityId { get; set; }
    public string Action { get; set; }
    public Guid? UserId { get; set; }
    [JsonIgnore]
    public virtual User User { get; set; }
    public DateTime Timestamp { get; set; }
}
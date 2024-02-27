using System.Text.Json.Serialization;

namespace Application.Abstractions;

public interface IRequestWithRequestingUserId
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public Guid? RequestingUserId { get; protected set; }

    public void SetRequestingUserId(Guid id)
    {
        RequestingUserId = id;
    }
}
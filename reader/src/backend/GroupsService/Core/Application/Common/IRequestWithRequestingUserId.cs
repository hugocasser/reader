using System.Text.Json.Serialization;

namespace Application.Common;

public interface IRequestWithRequestingUserId
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public Guid? RequestingUserId { get; protected set; }

    void SetRequestingUserId(Guid id)
    {
        RequestingUserId = id;
    }
}
namespace Application.Common;

public static class CachingKeys
{
    public const string Notes = "notes";
    public static string NoteById(Guid id)
    {
        return $"Note-{id}";
    }
}
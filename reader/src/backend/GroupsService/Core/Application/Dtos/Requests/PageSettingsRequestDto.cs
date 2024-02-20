namespace Application.Dtos.Views;

public record PageSettingsRequestDto(int Page, int PageSize)
{
    public int SkipCount()
    {
        return (Page - 1) * PageSize;
    }
};
namespace Application.Dtos.Requests;

public class PageSettingRequestDto(int page, int size)
{
    public int PageNumber { get; set; } = page;
    public int PageSize { get; set; } = size;

    public int Skip()
    {
        return (PageNumber - 1) * PageSize;
    }
}
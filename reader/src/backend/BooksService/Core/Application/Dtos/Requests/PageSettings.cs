using Application.Abstractions;
using Application.Validation;

namespace Application.Dtos.Requests;

public class PageSetting : Request<PageSetting>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool ShowDescription { get; set; }
    public int DescriptionMaxLength { get; set; }
}
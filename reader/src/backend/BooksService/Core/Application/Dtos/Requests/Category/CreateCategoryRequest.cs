using Application.Abstractions;

namespace Application.Dtos.Requests.Category;

public class CreateCategoryRequest : IRequest
{
    public string Name { get; set; } = string.Empty;
}
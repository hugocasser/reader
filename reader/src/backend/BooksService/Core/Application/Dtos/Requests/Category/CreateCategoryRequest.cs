using Application.Abstractions;
using Application.Validation;

namespace Application.Dtos.Requests.Category;

public class CreateCategoryRequest
{
    public string Name { get; set; } = string.Empty;
}
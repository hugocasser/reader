using Application.Abstractions;
using Application.Validation;

namespace Application.Dtos.Requests.Category;

public class CreateCategoryRequest : Request<CreateCategoryRequest>
{
    public string Name { get; set; } = string.Empty;
}
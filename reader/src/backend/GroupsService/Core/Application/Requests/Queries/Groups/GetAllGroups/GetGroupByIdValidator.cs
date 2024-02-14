using Application.Common;
using FluentValidation;

namespace Application.Handlers.Queries.Groups.GetAllGroups;

public class GetGroupByIdValidator : AbstractValidator<GetAllGroupsQuery>
{
    public GetGroupByIdValidator()
    {
        RuleFor(request => request.PageSettingsRequestDto)
            .PageSettings();
    }
}
using Application.Common;
using FluentValidation;

namespace Application.Requests.Queries.Groups.GetAllGroups;

public class GetGroupByIdValidator : AbstractValidator<GetAllGroupsQuery>
{
    public GetGroupByIdValidator()
    {
        RuleFor(request => request.PageSettingsRequestDto)
            .PageSettings();
    }
}
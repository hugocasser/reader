namespace Application.Common;

public record PageSettings(int PageNumber, int PageSize, bool showDescription, int DescriptionMaxLength);
using Application.Abstractions;

namespace Application.Results.Errors;

public class NotFoundError(string entityName) : Error($"{entityName} not found" , 404);
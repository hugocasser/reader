using Application.Abstractions;

namespace Application.Results.Errors;

public class BadRequestError(string message) : Error(message, 400);
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;
public class EmailNotConfirmedException() : ExceptionWithStatusCode(StatusCodes.Status401Unauthorized,
    "Email is not confirmed! Please confirm your Email.");

using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Exceptions;

public class IncorrectEmailOrPasswordException() : ExceptionWithStatusCode(
    StatusCodes.Status401Unauthorized, "Email or password is incorrect!" +
                                       " Please check your credentials and try again.");
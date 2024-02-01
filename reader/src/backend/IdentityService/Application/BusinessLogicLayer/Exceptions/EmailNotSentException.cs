namespace BusinessLogicLayer.Exceptions;

public class EmailNotSentException(string message) : Exception(message);
namespace BusinessLogicLayer.Exceptions;

public class EmailNotSentException(string _message) : Exception(_message);
namespace LgTv.Exceptions;

public class SendMessageException(string message, Exception e) : Exception(message, e);


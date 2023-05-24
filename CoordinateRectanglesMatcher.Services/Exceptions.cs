namespace CoordinateRectanglesMatcher.Services;

[Serializable]
public class MatcherInvalidParameterException: Exception
{
    public MatcherInvalidParameterException()
    {
    }
    
    public MatcherInvalidParameterException(string message) : base(message)
    {
    }

    public MatcherInvalidParameterException(string message, Exception inner)
        : base(message, inner)
    {
    }
}


namespace hayward.Tracing.Error;

public class CliError(string message) : System.Exception(message)
{
}
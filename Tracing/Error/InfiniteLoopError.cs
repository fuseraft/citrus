using hayward.Parsing;

namespace hayward.Tracing.Error;

public class InfiniteLoopError(Token t, string message = "An infinite loop occurred.")
    : KiwiError(t, "InfiniteLoopError", message)
{
}
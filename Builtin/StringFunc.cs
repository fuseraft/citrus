using citrus.Parsing;
using citrus.Typing;

namespace citrus.Builtin;

public struct StringFunc
{
    public static string StringMul(ref Value left, ref Value right)
    {
        var s = left.GetString();
        var multiplier = right.GetInteger();

        if (multiplier <= 0)
        {
            return string.Empty;
        }

        System.Text.StringBuilder builder = new ();
        
        for (int i = 0; i < multiplier; ++i)
        {
            builder.Append(s);
        }

        return builder.ToString();
    }
}
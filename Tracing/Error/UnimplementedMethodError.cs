﻿using citrus.Parsing;
using citrus.Runtime;
using System.Reflection;

namespace citrus.Tracing.Error;

public class UnimplementedMethodError(Token t, string structName, string methodName)
    : KiwiError(t, "UnimplementedMethodError", $"Struct `{structName}` has an unimplemented method `{methodName}`")
{
}
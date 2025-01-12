namespace citrus.Typing;

public partial class Serializer
{
    public static string Serialize(Value v, bool wrapStrings = false)
    {
        if (v.IsInteger())
        {
            return v.GetInteger().ToString();
        }
        else if (v.IsFloat())
        {
            return v.GetFloat().ToString();
        }
        else if (v.IsBoolean())
        {
            return $"{v.GetBoolean()}".ToLower();
        }
        else if (v.IsNull())
        {
            return "null";
        }
        else if (v.IsString())
        {
            if (wrapStrings)
            {
                return $"\"{v.GetString()}\"";
            }
            else
            {
                return v.GetString();
            }
        }
        else if (v.IsList())
        {
            return SerializeList(v.GetList());
        }
        else if (v.IsHashmap())
        {
            return SerializeHashmap(v.GetHashmap());
        }
        else if (v.IsObject())
        {
            return BasicSerializeObject(v.GetObject());
        }
        else if (v.IsLambda())
        {
            return BasicSerializeLambda(v.GetLambda());
        }
        /*else if (v.IsPointer())
        {
            sv.Append(k_pointer::serialize(v.GetPointer()));
        }*/

        return string.Empty;
    }

    public static string SerializeList(List<Value> list)
    {
        System.Text.StringBuilder sv = new();
        bool first = true;

        sv.Append('[');
        foreach (var element in list)
        {
            if (!first)
            {
                sv.Append(", ");
            }
            else
            {
                first = false;
            }

            if (element.IsString())
            {
                sv.Append($"\"{Serialize(element)}\"");
            }
            else
            {
                sv.Append(Serialize(element));
            }
        }

        sv.Append(']');
        return sv.ToString();
    }

    public static string SerializeHashmap(Dictionary<Value, Value> hash)
    {
        System.Text.StringBuilder sv = new();
        sv.Append('{');

        var first = true;

        foreach (var key in hash.Keys)
        {
            if (!first)
            {
                sv.Append(", ");
            }
            else
            {
                first = false;
            }

            sv.Append($"{Serialize(key, true)}: ");
            var v = hash[key];

            if (v.IsHashmap())
            {
                sv.Append(Serialize(v));
            }
            else
            {
                sv.Append(Serialize(v, true));
            }
        }

        sv.Append('}');
        return sv.ToString();
    }

    public static string PrettySerialize(Value v, int indent = 0)
    {
        if (v.IsInteger())
        {
            return $"{v.GetInteger()}";
        }
        else if (v.IsFloat())
        {
            return $"{v.GetFloat()}";
        }
        else if (v.IsBoolean())
        {
            return $"{v.GetBoolean()}";
        }
        else if (v.IsNull())
        {
            return "null";
        }
        else if (v.IsString())
        {
            return $"\"{v.GetString()}\"";
        }
        else if (v.IsList())
        {
            return PrettySerializeList(v.GetList(), indent);
        }
        else if (v.IsHashmap())
        {
            return PrettySerializeHashmap(v.GetHashmap(), indent);
        }
        else if (v.IsObject())
        {
            return BasicSerializeObject(v.GetObject());
        }
        else if (v.IsLambda())
        {
            return BasicSerializeLambda(v.GetLambda());
        }
        // else if (v.IsPointer())
        // {
        //     sv.Append(k_pointer::serialize(v.GetPointer());
        // }

        return string.Empty;
    }

    public static string GetPrettyIndentation(int indent = 0)
    {
        var s = string.Empty;

        for (var i = 0; i < indent; ++i)
        {
            s += ' ';
        }

        return s;
    }

    public static string PrettySerializeList(List<Value> list, int indent = 0)
    {
        System.Text.StringBuilder sv = new();
        sv.AppendLine("[");
        var indentString = GetPrettyIndentation(indent + 2);
        var first = true;

        foreach (var it in list)
        {
            if (!first)
            {
                sv.AppendLine(",");
            }
            else
            {
                first = false;
            }

            sv.Append(indentString);

            if (it.IsList())
            {
                sv.Append(PrettySerializeList(it.GetList(), indent + 2));
            }
            else if (it.IsHashmap())
            {
                sv.Append(PrettySerializeHashmap(it.GetHashmap(), indent + 2));
            }
            else if (it.IsString())
            {
                sv.Append($"\"{it.GetString()}\"");
            }
            else
            {
                sv.Append(PrettySerialize(it, indent + 2));
            }
        }

        sv.AppendLine();
        sv.Append($"{GetPrettyIndentation(indent)}]");
        return sv.ToString();
    }

    public static string PrettySerializeHashmap(Dictionary<Value, Value> hash, int indent = 0)
    {
        System.Text.StringBuilder sv = new();
        sv.AppendLine("{");

        var indentString = GetPrettyIndentation(indent + 2);
        var first = true;

        foreach (var key in hash.Keys)
        {
            if (!first)
            {
                sv.AppendLine(",");
            }
            else
            {
                first = false;
            }

            sv.Append($"{indentString}{Serialize(key, true)}: ");

            var v = hash[key];
            if (v.IsHashmap())
            {
                sv.Append(PrettySerializeHashmap(v.GetHashmap(), indent + 2));
            }
            else if (v.IsList())
            {
                sv.Append(PrettySerializeList(v.GetList(), indent + 2));
            }
            else if (v.IsString())
            {
                sv.Append($"\"{v.GetString()}\"");
            }
            else
            {
                sv.Append(PrettySerialize(v));
            }
        }

        sv.AppendLine();
        sv.Append($"{GetPrettyIndentation(indent)}}}");
        return sv.ToString();
    }

    public static List<Value> GetHashKeysList(Dictionary<Value, Value> hash)
    {
        List<Value> keys = [.. hash.Keys];
        return keys;
    }

    public static List<Value> GetHashValuesList(Dictionary<Value, Value> hash)
    {
        List<Value> values = [.. hash.Values];
        return values;
    }

    public static string BasicSerializeObject(InstanceRef obj)
    {
        if (string.IsNullOrEmpty(obj.Identifier))
        {
            return $"[Struct(Name={obj.StructName})]";
        }

        return $"[Struct(Name={obj.StructName}, Identifier={obj.Identifier}]";
    }

    public static string BasicSerializeLambda(LambdaRef lambda)
    {
        if (string.IsNullOrEmpty(lambda.Identifier))
        {
            return "[Lambda]";
        }

        return $"[Lambda(Identifier={lambda.Identifier})]";
    }
}
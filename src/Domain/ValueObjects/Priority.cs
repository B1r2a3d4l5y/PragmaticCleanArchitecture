﻿using Domain.Primitives;

namespace Domain.ValueObjects;

public class Priority : ValueObject
{
    public static readonly Priority Low = new(1, "Low");
    public static readonly Priority Medium = new(2, "Medium");
    public static readonly Priority High = new(3, "High");

    public int Level { get; }
    public string Name { get; }

    private Priority(int level, string name)
    {
        Level = level;
        Name = name;
    }

    public static Priority FromName(string name)
    {
        return name.ToLowerInvariant() switch
        {
            "low" => Low,
            "medium" => Medium,
            "high" => High,
            _ => throw new ArgumentException($"Unknown priority name: {name}.", nameof(name))
        };
    }

    public static Priority FromLevel(int level)
    {
        return level switch
        {
            1 => Low,
            2 => Medium,
            3 => High,
            _ => throw new ArgumentException($"Invalid priority level: {level}.", nameof(level))
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Level;
    }

    public override string ToString() => Name;
}
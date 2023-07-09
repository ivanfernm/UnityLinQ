using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlyweightPointer
{
    public static readonly Flyweight Enemy = new Flyweight
    {
        _chest = new Vector3(-8.397f, 4.869f, -8.677f),
        destinyRatious = 1.4f
    };

    public static readonly Flyweight Sword = new Flyweight
    {
        damange = 10,
    };
}

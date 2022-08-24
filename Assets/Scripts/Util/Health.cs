using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Health
{
    public int health;
    public int max;
    public int min = 0;

    HealthType type;

    public Health(int health, int max)
    {
        this.max = max;
        min = 0;
        this.health = checkBorders(health);
    }

    private int checkBorders(int health)
    {
        if (health > max)
        {
            return max;
        }
        else if (health < min)
        {
            return min;
        }

        return health;
    }

    public static Health operator +(Health h1, Health h2) => new (h1.health + h2.health, h1.max);
    public static Health operator *(Health h, int x) => new(h.health * x, h.max);
    public static Health operator /(Health h, int x)
    {
        if (x == 0)
        {
            throw new DivideByZeroException();
        }

        return new Health(h.health / 2, h.max);
    }
    public static Health operator -(Health h, Damage d)
    {
        return new Health(h.health - d.damage, h.max);
    }

    public static bool operator ==(Health h, int x) => h.health == x ? true : false;
    public static bool operator !=(Health h, int x) => h.health != x ? true : false;

    public override string ToString()
    {
        return health.ToString();
    }

    public override bool Equals(object obj)
    {
        return obj is Health health &&
               this.health == health.health &&
               max == health.max &&
               min == health.min &&
               type == health.type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(health, max, min, type);
    }
}

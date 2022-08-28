using System;

[System.Serializable]
public class Damage
{
    public int damage;
    int max = 1000;
    int min = 1;

    DamageType type;

    public Damage(int damage)
    {
        this.damage = CheckDamage(damage);
    }

    public int CheckDamage(int damage)
    {
        if (damage > max)
        {
            return max;
        }
        else if (damage < min)
        {
            return min;
        }

        return damage;
    }

    public static Damage operator +(Damage d1, Damage d2) => new Damage(d1.damage + d2.damage);
    public static Damage operator -(Damage d1, Damage d2) => new Damage(d1.damage - d2.damage);
    public static Damage operator *(Damage d, int x) => new Damage(d.damage * x);
    public static Damage operator /(Damage d, int x)
    {
        if (x == 0)
        {
            throw new DivideByZeroException();
        }

        return new Damage(d.damage / x);
    }

    public override string ToString()
    {
        return damage.ToString();
    }
}

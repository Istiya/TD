using System;

public enum EnemyType
{
    ENEMY_TYPE_FIRST,
    ENEMY_TYPE_SECOND,
    ENEMY_TYPE_THIRD,
    ENEMY_TYPE_FOURTH,
    ENEMY_TYPE_FIFTH,
}

public static class EnemyTypeExtension {

    public static PrefabType GetPrefabType(this EnemyType enemyType)
    {
        return Enum.Parse<PrefabType>(enemyType.ToString());
    }
}

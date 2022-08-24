using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveModel : IComparable<WaveModel>
{
    public List<EnemyType> enemyTypes;
    public int enemyCount;
    public int waveIndex;

    public WaveModel() { }

    public WaveModel(List<EnemyType> enemyTypes, int enemyCount)
    {
        this.enemyTypes = enemyTypes;
        this.enemyCount = enemyCount;
    }

    int IComparable<WaveModel>.CompareTo(WaveModel other)
    {
        return this.waveIndex.CompareTo(other.waveIndex);
    }
}



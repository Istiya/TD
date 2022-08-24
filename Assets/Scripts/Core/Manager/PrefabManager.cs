using System;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject Get(PrefabType type)
    {
        return PrefabsContainer.instance.prefabs.Find(pm => pm.type == type).prefab;
    }
}

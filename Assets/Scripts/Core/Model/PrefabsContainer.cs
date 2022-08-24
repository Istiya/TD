using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsContainer : MonoBehaviour
{
    public static PrefabsContainer instance;

    public List<PrefabModel> prefabs = new List<PrefabModel>();

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
}

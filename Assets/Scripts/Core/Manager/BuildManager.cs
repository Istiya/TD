using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance = null;

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

    public void SellTurret(TurretModel model)
    {
        GameManager.instance.ChangeMoney((int)model.cost / 2);
        model.terrainNodeModel.isTowerPlaced = false;
        Destroy(model.gameObject);
    }

    public void BuildTurret(ref TerrainNodeModel model)
    {
        if (ChoosenTurretController.instance == null)
        {
            return;
        }

        if (!model.isTowerPlaced && GameManager.instance.GetMoney() >= ChoosenTurretController.instance.cost)
        {
            TurretModel turret = Instantiate(PrefabManager.instance.Get(ChoosenTurretController.instance.turret), model.transform.position + new Vector3(0, 0.05f, 0), Quaternion.identity, model.transform.root.Find("Towers")).GetComponent<TurretModel>();
            Type type = Type.GetType(turret.controller);
            turret.gameObject.AddComponent(type);
            turret.terrainNodeModel = model;

            GameManager.instance.ChangeMoney(-ChoosenTurretController.instance.cost);
            model.isTowerPlaced = true;

            if (GameManager.instance.GetMoney() < ChoosenTurretController.instance.cost)
            {
                Destroy(ChoosenTurretController.instance.gameObject);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance = null;

    private int cost;

    GameObject turretToBuild;

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

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret, int cost = 0)
    {
        this.turretToBuild = turret;
        this.cost = cost;
    }

    public void SellTurret(TurretModel model)
    {
        GameManager.instance.ChangeMoney((int)model.cost / 2);
        model.terrainNodeModel.isTowerPlaced = false;
        Destroy(model.gameObject);
    }

    public void BuildTurret(ref TerrainNodeModel model)
    {
        if (GetTurretToBuild() == null)
        {
            return;
        }

        if (!model.isTowerPlaced && GameManager.instance.GetMoney() >= cost)
        {
            TurretModel turret = Instantiate(GetTurretToBuild(), model.transform.position + new Vector3(0, 0.05f, 0), Quaternion.identity, model.transform.root.Find("Towers")).GetComponent<TurretModel>();
            Type type = Type.GetType(turret.controller);
            turret.gameObject.AddComponent(type);
            turret.terrainNodeModel = model;

            GameManager.instance.ChangeMoney(-cost);
            model.isTowerPlaced = true;

            if (GameManager.instance.GetMoney() < cost)
            {
                Destroy(ChoosenTurretController.instance.gameObject);
                SetTurretToBuild(null);
            }

        }
    }

}

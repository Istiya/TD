using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class ShopPanelController : MonoBehaviour
{
    public static ShopPanelController instance;

    private void Awake()
    {
        instance = this;
    }

    public void PurchaseMGTurret()
    {
        if (ChoosenTurretController.instance != null)
        {
            if (ChoosenTurretController.instance.gameObject.name.Contains(PrefabManager.instance.Get(PrefabType.MGTURRET).name))
            {
                return;
            }
            else
            {
                Destroy(ChoosenTurretController.instance.gameObject);
            }
        }

        int money = GameManager.instance.GetMoney();
        int cost = (int)TurretCost.MG_TURRET;

        if (money >= cost)
        {
            GameObject NewMGTurret = Instantiate(PrefabManager.instance.Get(PrefabType.MGTURRET), transform.position, Quaternion.identity);
            NewMGTurret.AddComponent<ChoosenTurretController>().Init(PrefabType.MGTURRET, cost);
        }
        else
        {
            StartCoroutine(MoneyController.instance.ChangeMoneyColor());
            Debug.Log("Not enough money!");
        }
    }

    public void PurchaseMGsTurret()
    {
        if (ChoosenTurretController.instance != null)
        {
            if (ChoosenTurretController.instance.gameObject.name.Contains(PrefabManager.instance.Get(PrefabType.MGSTURRET).name))
            {
                return;
            }
            else
            {
                Destroy(ChoosenTurretController.instance.gameObject);
            }
        }

        int money = GameManager.instance.GetMoney();
        int cost = (int)TurretCost.MGs_TURRET;

        if (money >= cost)
        {
            GameObject NewMGsTurret = Instantiate(PrefabManager.instance.Get(PrefabType.MGSTURRET), transform.position, Quaternion.identity);
            NewMGsTurret.AddComponent<ChoosenTurretController>().Init(PrefabType.MGSTURRET, cost);
        }
        else
        {
            StartCoroutine(MoneyController.instance.ChangeMoneyColor());
            Debug.Log("Not enough money!");
        }

    }

    public void PurchaseTrianglesTurret()
    {
        
    }
}

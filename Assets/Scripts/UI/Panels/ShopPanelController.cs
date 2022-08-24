using UnityEngine;

public class ShopPanelController : MonoBehaviour
{
    public void PurchaseMGTurret()
    {
        int money = GameManager.instance.GetMoney();
        int cost = (int)TurretCost.MG_TURRET;

        if (money >= cost)
        {
            if (ChoosenTurretController.instance == null)
            {
                BuildManager.instance.SetTurretToBuild(PrefabManager.instance.Get(PrefabType.MGTURRET), cost);
                GameObject NewMGTurret = Instantiate(PrefabManager.instance.Get(PrefabType.CHOOSEN_MGTURRET), transform.position, Quaternion.identity);
                NewMGTurret.AddComponent<ChoosenTurretController>();
            }
            else
            {
                BuildManager.instance.SetTurretToBuild(PrefabManager.instance.Get(PrefabType.MGTURRET), cost);
                GameObject NewMGTurret = Instantiate(PrefabManager.instance.Get(PrefabType.CHOOSEN_MGTURRET), transform.position, Quaternion.identity);
                NewMGTurret.AddComponent<ChoosenTurretController>();
            }

            //if (ChoosenTurretController.instance == null)
            //{
            //    BuildManager.instance.SetTurretToBuild(PrefabManager.instance.Get(PrefabType.MGTURRET), cost);
            //    GameObject NewMGTurret = Instantiate(PrefabManager.instance.Get(PrefabType.CHOOSEN_MGTURRET), transform.position, Quaternion.identity);
            //    NewMGTurret.AddComponent<ChoosenTurretController>();
            //}
            //else
            //{
            //    Destroy(ChoosenTurretController.instance.gameObject);
            //    BuildManager.instance.SetTurretToBuild(PrefabManager.instance.Get(PrefabType.MGTURRET), cost);
            //    GameObject NewMGTurret = Instantiate(PrefabManager.instance.Get(PrefabType.CHOOSEN_MGTURRET), transform.position, Quaternion.identity);
            //    NewMGTurret.AddComponent<ChoosenTurretController>();
            //}
        }
        else
        {
            Debug.Log("Not enough money!");
        }

    }

    public void PurchaseMGsTurret()
    {
        int money = GameManager.instance.GetMoney();
        int cost = (int)TurretCost.MGs_TURRET;
        if (money >= cost)
        {
            if (ChoosenTurretController.instance == null)
            {
                BuildManager.instance.SetTurretToBuild(PrefabManager.instance.Get(PrefabType.MGSTURRET), cost);
                GameObject NewMGsTurret = Instantiate(PrefabManager.instance.Get(PrefabType.CHOOSEN_MGSTURRET), transform.position, Quaternion.identity);
                NewMGsTurret.AddComponent<ChoosenTurretController>();
            }
            else
            {
                Destroy(ChoosenTurretController.instance.gameObject);
                BuildManager.instance.SetTurretToBuild(PrefabManager.instance.Get(PrefabType.MGSTURRET), cost);
                GameObject NewMGsTurret = Instantiate(PrefabManager.instance.Get(PrefabType.CHOOSEN_MGSTURRET), transform.position, Quaternion.identity);
                NewMGsTurret.AddComponent<ChoosenTurretController>();
            }

        }
        else
        {
            Debug.Log("Not enough money!");
        }

    }

    public void PurchaseTrianglesTurret()
    {
        int money = GameManager.instance.GetMoney();
        int cost = (int)TurretCost.TRIANGLES_TURRET;
        if (money >= cost)
        {
            if (ChoosenTurretController.instance == null)
            {
                BuildManager.instance.SetTurretToBuild(PrefabManager.instance.Get(PrefabType.TRIANGLES_TURRET), cost);
                GameObject NewTrianglesTurret = Instantiate(PrefabManager.instance.Get(PrefabType.TRIANGLES_TURRET), transform.position, Quaternion.identity);
                NewTrianglesTurret.AddComponent<ChoosenTurretController>();
            }
            else
            {
                Destroy(ChoosenTurretController.instance.gameObject);
                BuildManager.instance.SetTurretToBuild(PrefabManager.instance.Get(PrefabType.TRIANGLES_TURRET), cost);
                GameObject NewTrianglesTurret = Instantiate(PrefabManager.instance.Get(PrefabType.CHOOSEN_TRIANGLES), transform.position, Quaternion.identity);
                NewTrianglesTurret.AddComponent<ChoosenTurretController>();
            }
        }
        else
        {
            Debug.Log("Not enough money!");
        }

    }
}

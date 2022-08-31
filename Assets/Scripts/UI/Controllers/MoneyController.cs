using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public static MoneyController instance;

    public TMP_Text money;

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

        GameManager.instance.OnMoneyChange += ChangeMoney;
    }

    public void ChangeMoney(int money)
    {
        this.money.text = money.ToString() + "$";
    }

    public IEnumerator ChangeMoneyColor()
    {
        for (int i = 0; i < 2; i++)
        {
            MoneyController.instance.money.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            MoneyController.instance.money.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }
}

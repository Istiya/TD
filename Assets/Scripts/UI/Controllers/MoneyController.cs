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

        money.text = GameManager.instance.GetMoney().ToString() + "$";
        GameManager.instance.OnMoneyChange += ChangeMoney;
    }

    public void ChangeMoney(int money)
    {
        this.money.text = money.ToString() + "$";
    }
}

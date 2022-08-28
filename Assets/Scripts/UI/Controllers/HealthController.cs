using TMPro;
using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public static HealthController instance;

    public TMP_Text health;
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

        health.text = GameManager.instance.GetHealth().ToString();
        GameManager.instance.OnHealthChange += ChangeHealth;
    }

    public void ChangeHealth(int health)
    {
        this.health.text = health.ToString();
        StartCoroutine(DoChangeColor());
    }

    IEnumerator DoChangeColor()
    {
        health.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        health.color = Color.white;
    }
}

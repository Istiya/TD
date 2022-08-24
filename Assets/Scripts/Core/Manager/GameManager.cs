using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class GameManager : MonoBehaviour
{
    public static GameManager? instance = null;

    private int health;
    private int money;
    private int score;

    public event Action<int>? OnHealthChange;
    public event Action<int>? OnMoneyChange;
    public event Action<int>? OnScoreChange;

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

        InitGameManager();
    }

    public void ChangeHealth(int damage)
    {
        this.health -= damage;
        CheckHealth();
        OnHealthChange?.Invoke(health);
    }

    public void ChangeMoney(int reward)
    {
        this.money += reward;
        OnMoneyChange?.Invoke(money);
    }

    public void ChangeScore(int score)
    {
        this.score += score;
        OnScoreChange?.Invoke(this.score);
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Instantiate(PrefabManager.instance.Get(PrefabType.GAMEOVER));
        Destroy(gameObject);
        Destroy(GameObject.FindGameObjectWithTag("UI"));
    }

    private void InitGameManager()
    {
        health = 100;
        money = 500;
        InitControllers();
        Instantiate(PrefabManager.instance.Get(PrefabType.UI));
        Instantiate(PrefabManager.instance.Get(PrefabType.MAP), transform.position, Quaternion.identity);
    }
    private void InitControllers()
    {
        gameObject.AddComponent<PrefabManager>();
        gameObject.AddComponent<BuildManager>();
    }

    public int getScore()
    {
        return score;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMoney()
    {
        return money;
    }
}

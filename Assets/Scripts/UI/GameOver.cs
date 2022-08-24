using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text score;

    void Start()
    {
        score.text += GameManager.instance.getScore();   
    }

    public void OnRestartButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }

}

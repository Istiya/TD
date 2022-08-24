using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    public TMP_Text score;
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

        score.text = "0";
        GameManager.instance.OnScoreChange += ChangeScore;
    }

    public void ChangeScore(int score)
    {
        this.score.text = score.ToString();
    }
}

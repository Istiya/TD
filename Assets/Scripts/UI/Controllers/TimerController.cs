using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public TMP_Text timer;
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

        FindObjectOfType<WaveManager>().OnTimeChanges += ChangeTimeBetweensWaves;
    }

    public void ChangeTimeBetweensWaves(float time)
    {
        this.timer.text = time.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoutdownTime : MonoBehaviour
{
    public float countdownTime = 1;
    public Text coundownText;
    private float currenTimes;
    public bool isEnd = false;

    public UIMainFishControl control;
    public void Start()
    {
        currenTimes = countdownTime;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (currenTimes > 0)
        {
            if (control.isGameEnd)
            {
                yield break; 
            }
            isEnd = false;
            currenTimes -= Time.deltaTime;
            coundownText.text = Mathf.Ceil(currenTimes).ToString();
            yield return null;
        }

        coundownText.text = "Time's up!";
        OnCountdownEnd();

        void OnCountdownEnd()
        {
            isEnd = true;
            Debug.Log("Countdown finished");
            control.LoadResutl(isEnd);
        }
        Debug.Log($"isEnd: {isEnd}");
    }
   

}

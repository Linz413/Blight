using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class calculateScore : MonoBehaviour
{
    public Text[] scoreBoxes;
    public GameObject bus;
    private float total;
    public int logBaseTime = 3;
    public int scorePercentTime;
    public int scorePercentCondition;
    public int scorePercentLures;
    public int baseScore = 100;
    public int studentHitPenalty = 350;
    public int luresMultiplier = 50;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void calcScore(float time, int damage, int strikes, int studentsNeeded)
    {
//        Debug.LogWarning(strikes);
        StartCoroutine(calc(time, damage, strikes, studentsNeeded));

    }

    IEnumerator calc(float time, int damage, int strikes, int studentsNeeded)
    {
        int i = 0;
        
        while (i < scoreBoxes.Length)
        {
            Text subText = scoreBoxes[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            subText.gameObject.SetActive(true);
            subText.text = "";
            scoreBoxes[i].gameObject.SetActive(true);
            countScore(subText, i, time, damage, strikes, studentsNeeded);
            i++;
            yield return WaitForRealSeconds(2);
        }
    }
    
    public static IEnumerator WaitForRealSeconds( float delay )
    {
        float start = Time.realtimeSinceStartup;
        while( Time.realtimeSinceStartup < start + delay)
        {
            yield return null;
        }
    }
     
    
    private void countScore(Text subText, int index, float time, int damage, int strikes, int studentsNeeded)
    {
//        int scoreCountUp = 0;
//        float timer = 0;
        float amount = 0;
        double duration = 5.0f;
        float currentDisplayScore = 0;

        float baseS = baseScore * studentsNeeded;

        switch (index)
        {
            case 0:
                amount = baseS * Mathf.Pow(logBaseTime, (1 / Mathf.Log(time)));
//                amount = time;
                total += amount;
                break;
            case 1:
                
                amount = (float) bus.GetComponent<BusAbilities>().lures * luresMultiplier;
                total += amount;
                break;
            case 2:
                amount = (100-damage) *-3;
                total += amount;
                break;
            case 3:
/*                if (strikes == 0)
                {
                    amount = -1;
                }
                else
                {*/
                    amount = -strikes * studentHitPenalty;
                    total += amount;
//                /**/}
                break;
            case 4:
                amount = total;
                break;
                  
        }

        

        StartCoroutine(CountUpToTarget(currentDisplayScore, amount, subText, duration));



    }

    IEnumerator CountUpToTarget(float currentDisplayScore, float amount, Text subText, double duration)
    {
        float absAmount = Mathf.Abs(amount);
        if (absAmount== 0)
        {
            subText.text = "0";
        }
        while (currentDisplayScore < absAmount)
        {
            float scoreMultiplier = (float) (Time.unscaledDeltaTime / duration);
            float scoreAdd = absAmount * scoreMultiplier;
            currentDisplayScore += currentDisplayScore + scoreAdd;

            currentDisplayScore = Mathf.Clamp(currentDisplayScore, 0f, absAmount);
            subText.text = "";
            if (amount < 0)
            {
                subText.text = "-";
            }
            subText.text += currentDisplayScore.ToString("0.##");

            yield return null;
        }
    }
    
}

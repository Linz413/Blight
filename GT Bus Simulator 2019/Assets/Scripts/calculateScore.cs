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
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void calcScore(float time, int damage, int strikes)
    {
//        Debug.LogWarning(strikes);
        StartCoroutine(calc(time, damage, strikes));

    }

    IEnumerator calc(float time, int damage, int strikes)
    {
        int i = 0;
        
        while (i < scoreBoxes.Length)
        {
            Text subText = scoreBoxes[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            subText.gameObject.SetActive(true);
            subText.text = "";
            scoreBoxes[i].gameObject.SetActive(true);
            countScore(subText, i, time, damage, strikes);
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
     
    
    private void countScore(Text subText, int index, float time, int damage, int strikes)
    {
        int scoreCountUp = 0;
        float timer = 0;
        float amount = 0;
        double duration = 5.0f;
        float currentDisplayScore = 0;

        switch (index)
        {
            case 0:
                amount = time;
                total += amount;
                break;
            case 1:
                amount = bus.GetComponent<BusAbilities>().lures;
                total += amount;
                break;
            case 2:
                amount = damage;
                total += amount;
                break;
            case 3:
                if (strikes == 0)
                {
                    amount = -1;
                }
                else
                {
                    amount = strikes;
                    total += amount;
                }
                break;
            case 4:
                amount = total;
                break;
                  
        }

        

        StartCoroutine(CountUpToTarget(currentDisplayScore, amount, subText, duration));



    }

    IEnumerator CountUpToTarget(float currentDisplayScore, float amount, Text subText, double duration)
    {
        while (currentDisplayScore < amount)
        {
            float scoreMultiplier = (float) (Time.unscaledDeltaTime / duration);
            float scoreAdd = amount * scoreMultiplier;
            currentDisplayScore += currentDisplayScore + scoreAdd;
            if (amount == -1)
            {
                subText.text = "0";
            }
            else
            {
                currentDisplayScore = Mathf.Clamp(currentDisplayScore, 0f, amount);
                subText.text = currentDisplayScore.ToString();
            }
            yield return null;
        }
    }
    
}

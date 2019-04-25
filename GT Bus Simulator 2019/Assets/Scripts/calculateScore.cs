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
    private string[] names = {"time", "lures", "damage", "strikes", "total"};
    public Text userName;
        
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
//        Debug.LogWarning("here");
        while (i < scoreBoxes.Length)
        {
//            Debug.LogWarning("here" + i);
            Text subText = scoreBoxes[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            subText.gameObject.SetActive(true);
            subText.text = "";
            var frontText = "";
            switch (i)
            {
                case(0):
                    frontText = studentsNeeded + " Students x (100pt) x time mult = ";
                    break;
                case(1):
                    frontText = bus.GetComponent<BusAbilities>().lures + " x ("+ luresMultiplier + "pts) = ";
                    break;
                case(2):
                    frontText = "3 x (-"+ (100-damage) + "pts) = ";
                    break;
                case(3):
                    frontText = strikes + " x (-" + studentHitPenalty + "pts) = ";
                    break;
                case(4):
                    frontText = "";
                    break;
            }
            scoreBoxes[i].gameObject.SetActive(true);
            countScore(subText, i, time, damage, strikes, studentsNeeded, frontText);
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
     
    
    private void countScore(Text subText, int index, float time, int damage, int strikes, int studentsNeeded, 
        string frontText)
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

        

        StartCoroutine(CountUpToTarget(currentDisplayScore, amount, subText, duration, frontText));



    }

    IEnumerator CountUpToTarget(float currentDisplayScore, float amount, Text subText, double duration, 
        string frontText)
    {
        float absAmount = Mathf.Abs(amount);
        if (absAmount== 0)
        {
            subText.text += "0";
        }
        while (currentDisplayScore < absAmount)
        {
            float scoreMultiplier = (float) (Time.unscaledDeltaTime / duration);
            float scoreAdd = absAmount * scoreMultiplier;
            currentDisplayScore += currentDisplayScore + scoreAdd;

            currentDisplayScore = Mathf.Clamp(currentDisplayScore, 0f, absAmount);
            subText.text = frontText;
            if (amount < 0)
            {
                subText.text += "-";
            }
            subText.text += currentDisplayScore.ToString("0.##");

            yield return null;
        }
    }

    public void SubmitScore()
    {
        var newName = this.userName.text;
        var newScore = this.total;
        HighScoreManager._instance.SaveHighScore(newName, newScore);
//        float oldScore;
//        string oldName;
//        for (int i = 0; i < 10; i++)
//        {
//            if(PlayerPrefs.GetInt(i+"HScore")<newScore){ 
//                // new score is higher than the stored score
//                oldScore = PlayerPrefs.GetFloat(i+"HScore");
//                oldName = PlayerPrefs.GetString(i+"HScoreName");
//                PlayerPrefs.SetFloat(i+"HScore", newScore);
//                PlayerPrefs.SetString(i+"HScoreName",newName);
//                newScore = oldScore;
//                newName = oldName;
//            } else{
//                PlayerPrefs.SetFloat(i+"HScore", newScore);
//                PlayerPrefs.SetString(i+"HScoreName",newName);
//                newScore = 0;
//                newName = "";
//            }
//        }
        


    }
    
    
}

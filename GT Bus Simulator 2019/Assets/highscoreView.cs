using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscoreView : MonoBehaviour
{
    List<Scores> highscore;

    public Text highScoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        highScoreTxt.text = "";
        highscore = HighScoreManager._instance.GetHighScore();
        foreach (var score in highscore)
        {
            highScoreTxt.text += score.name + ": " + score.score.ToString("0.##") + Environment.NewLine;
        }
    }
    
  
}

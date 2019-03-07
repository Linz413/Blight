using System.Collections;
using System.Collections.Generic;
// using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class PeopleCollection : MonoBehaviour
{
	public Text scoreText;
	int score = 0;
    int killedStudents = 0;
    private int hitAI = 0;
    public float timeToWin = 30;
    private float currentTime = 0;
    private int busHealth = 100;
    public Slider myHealthSlider;
    public int requiredScore = 100;
    public int strikes = 0;
    public Text strikeText;
    private string message = "";
    public Canvas gameScoreCanvas;
    public Canvas gameEndCanvas;
    private CanvasGroup gameScoreCanvasGroup;
    private CanvasGroup gameEndCanvasGroup;
    public Text gameEndText;
    public bool isRedRoute = false;
	void Start() {
		updateScore();
		updateStrikes();
        gameScoreCanvasGroup = gameScoreCanvas.GetComponent<CanvasGroup>();
        gameEndCanvasGroup = gameEndCanvas.GetComponent<CanvasGroup>();
        gameEndCanvasGroup.alpha = 0f;
        gameEndCanvasGroup.interactable = false;
        gameEndCanvasGroup.blocksRaycasts = false;
        myHealthSlider.value = busHealth;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;   
        if (currentTime > timeToWin)
        {
            // LOSE CONDITION
            message = "You were too slow and the students rioted! You are now unemployed... You Lose!";
            gameEnd(message);
        }
    }

    public void ReceivePickup() {
    	score++;
    	//scoreText.text = "Picked Up: " + score.ToString();
        updateScore();
        if (score >= requiredScore && currentTime <= timeToWin)
        {
            // WIN CONDITION
            message = "You Win! Good job getting the students to class!";
            gameEnd(message);
        }
    }

    public void HitStudent()
    {
        killedStudents++;
        updateStrikes();
        if (killedStudents >= 3)
        {
            // LOSE CONDITION
            message = "You hit too many students! License is revoked! You Lose!";
            gameEnd(message);
        }
    }
    
    public void HitAICar()
    {
        hitAI++;
        busHealth = busHealth - 5;
        myHealthSlider.value = busHealth;
        if (busHealth == 0)
        {
            //LOSE CONDITION
            message = "Your bus is too beat up to continue! You're out of service! You Lose!";
            gameEnd(message);
        }
//        if (hitAI >= 3)
//        {
//            // LOSE CONDITION
//        }
    }
    
    public void HitBusStop()
    {
        hitAI++;
        busHealth = busHealth - 5;
        myHealthSlider.value = busHealth;
        if (busHealth == 0)
        {
            //LOSE CONDITION
            message = "Your bus is too beat up to continue! You're out of service! You Lose!";
            gameEnd(message);
        }
    }

    private void updateScore()
    {
        scoreText.text = score.ToString() + "/" + requiredScore.ToString();
    }

    
    
    private void updateStrikes()
    {
        strikeText.text = "x" + killedStudents.ToString();
    }
    
    private void gameEnd(string message)
    {
        Time.timeScale = 0f;
        gameScoreCanvasGroup.alpha = 0f;
        gameEndCanvasGroup.alpha = 1f;
        gameEndCanvasGroup.interactable = true;
        gameEndCanvasGroup.blocksRaycasts = true;
        gameEndText.text = message;
    }
    
    
}

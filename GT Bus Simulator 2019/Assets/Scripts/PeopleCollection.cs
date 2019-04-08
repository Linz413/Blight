using System.Collections;
using System.Collections.Generic;
// using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class PeopleCollection : MonoBehaviour
{
	public Text scoreText;
    public AudioSource audioSource;
    public AudioClip peopleCollectNoise;
    public AudioClip crashNoise;
    int score = 0;
    int killedStudents = 0;
    private int hitAI = 0;
    public float timeToWin = 30;
    private float currentTime = 0;
    private int busHealth = 100;
    public Slider myHealthSlider;
    public int requiredScore = 10;
    public int strikes = 0;
    public int maxKilledStudents = 3;
    public Text strikeText;
    private string message = "";
    public Canvas gameScoreCanvas;
    public Canvas gameEndLoseCanvas;
    public Canvas gameEndWinCanvas;
    private CanvasGroup gameScoreCanvasGroup;
    private CanvasGroup gameEndLoseCanvasGroup;
    private CanvasGroup gameEndWinCanvasGroup;
    public Text gameEndLoseText;
    public Text gameEndWinText;
    public bool isRedRoute = false;
    public Text timerText;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;
    public GameObject[] busStops;
    public int busCurrentBusStop;
    public GameObject arrow;
    private Vector3 targetPoint;

    //emitter
    public GameObject emitter;
    private Smoke_Emitters smoke;
	void Start() {
		updateScore();
		updateStrikes();
        gameScoreCanvasGroup = gameScoreCanvas.GetComponent<CanvasGroup>();
        
        gameEndLoseCanvasGroup = gameEndLoseCanvas.GetComponent<CanvasGroup>();
        gameEndLoseCanvasGroup.alpha = 0f;
        gameEndLoseCanvasGroup.interactable = false;
        gameEndLoseCanvasGroup.blocksRaycasts = false;
        
        gameEndWinCanvasGroup = gameEndWinCanvas.GetComponent<CanvasGroup>();
        gameEndWinCanvasGroup.alpha = 0f;
        gameEndWinCanvasGroup.interactable = false;
        gameEndWinCanvasGroup.blocksRaycasts = false;
        
        targetPoint = busStops[busCurrentBusStop].transform.position;
        myHealthSlider.value = busHealth;
        smoke = emitter.GetComponent<Smoke_Emitters>();
    }

    private void Update()
    {
        UpdateTimerUI();
        var difference = targetPoint - transform.position;
        if (Vector3.Distance(targetPoint, transform.position) < 15)
        {
            busCurrentBusStop++;
            targetPoint = busStops[busCurrentBusStop].transform.position;
        }

        if (isRedRoute)
        {
            difference.x = -90;
        }
        else
        {
            difference.x = 90;

        }
//        difference.y = 0;     // Flatten the vector, assuming you're not concerned with indicating height difference
 
        // Maybe use some other method to actually apply the rotation  
        Quaternion quat = Quaternion.LookRotation(difference.normalized);
        Vector3 rot = quat.eulerAngles;
        rot.x = -90;
        arrow.transform.rotation = Quaternion.Euler(rot);
        smoke.damageLevel = 100 - busHealth;
//        currentTime += Time.deltaTime;   
//        if (currentTime > timeToWin)
//        {
//            // LOSE CONDITION
//            message = "You were too slow and the students rioted! You are now unemployed... You Lose!";
//            gameEnd(message); 
//        }
    }

    public void ReceivePickup() {
    	score++;
        audioSource.clip = peopleCollectNoise;
        audioSource.Play();
    	//scoreText.text = "Picked Up: " + score.ToString();
        updateScore();
        if (score >= requiredScore && currentTime <= timeToWin)
        {
            // WIN CONDITION
            message = "You Win! Good job getting the students to class!";
            gameEndWin(message);
        }
    }

    public void HitStudent()
    {
        killedStudents++;
        updateStrikes();
        if (killedStudents >= maxKilledStudents)
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
        audioSource.clip = crashNoise;
        audioSource.Play();
        myHealthSlider.value = busHealth;
        if (busHealth <= 0)
        {
            //LOSE CONDITION
            message = "Your bus is too beat up to continue! You're out of service! You Lose!";
            gameEnd(message);
        }
    }
    
    public void HitObject(int damage)
    {
        busHealth = busHealth - damage;
        audioSource.clip = crashNoise;
        audioSource.Play();
        myHealthSlider.value = busHealth;
        if (busHealth <= 0)
        {
            //LOSE CONDITION
            message = "Your bus is too beat up to continue! You're out of service! You Lose!";
            gameEnd(message);
        }
    }
    
    public void HitBusStop()
    {
        hitAI++;
        busHealth = busHealth - 5;
        audioSource.clip = crashNoise;
        audioSource.Play();
        myHealthSlider.value = busHealth;
        if (busHealth <= 0)
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
        gameEndLoseCanvasGroup.alpha = 1f;
        gameEndLoseCanvasGroup.interactable = true;
        gameEndLoseCanvasGroup.blocksRaycasts = true;
        gameEndLoseText.text = message;
    }
    
    private void gameEndWin(string message)
    {
        Time.timeScale = 0f;
        gameScoreCanvasGroup.alpha = 0f;
        gameEndWinCanvasGroup.alpha = 1f;
        gameEndWinCanvasGroup.interactable = true;
        gameEndWinCanvasGroup.blocksRaycasts = true;
        gameEndWinText.text = message;
        var time = hourCount * 60 * 60 + minuteCount * 60 + secondsCount;
        gameEndWinCanvasGroup.GetComponent<calculateScore>().calcScore(time, busHealth, killedStudents, requiredScore);
        
        
    }

    private void UpdateTimerUI()
    {
        secondsCount += Time.deltaTime;
        timerText.text = hourCount + ":" + minuteCount.ToString("00") + ":" +
                         ((int) secondsCount).ToString("00");
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            hourCount++;
            minuteCount = 0;
        }
    }



}

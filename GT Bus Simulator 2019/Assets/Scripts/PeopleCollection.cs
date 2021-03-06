﻿using System.Collections;
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
    public int score = 0;
    int killedStudents = 0;
    private int hitAI = 0;
    public float timeToWin = 30;
    private float currentTime = 0;
    public int busHealth = 100;
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
    public GameObject[] students;
    public int busCurrentBusStop;
    public GameObject arrow;
    private Vector3 targetPoint;
    public int currentStudent;
    private Vector3 difference;

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
        smoke.damageLevel = 100 - busHealth;
        if (students[currentStudent] == null)
        {
            currentStudent++;
            if (currentStudent >= students.Length)
            {
                currentStudent = 0;
            }
            targetPoint = students[currentStudent].transform.position;
        }

        difference = targetPoint - arrow.transform.position;

        


        // Debug.DrawRay (arrow.transform.position, difference, Color.green);
        // Debug.DrawLine(arrow.transform.position, targetPoint, Color.red);

        var rot = difference;
        
        rot = new Vector3(-90, rot.x, rot.z);
        
        Quaternion quat = Quaternion.LookRotation(difference.normalized);
        rot = quat.eulerAngles;
        rot.x = -90;
        
        arrow.transform.rotation = Quaternion.Euler(rot);

    }
    
//     void OnDrawGizmos()
//     {
//         // Draw a yellow sphere at the transform's position
//         Gizmos.color = Color.green;

// //        Gizmos.DrawSphere/**/(transform.position, 1);
//         Gizmos.DrawCube(targetPoint, new Vector3(5, 5,5));

//     }
    

    public void ReceivePickup() {
    	score++;
        audioSource.clip = peopleCollectNoise;
        audioSource.Play();
    	//scoreText.text = "Picked Up: " + score.ToString();
        updateScore();
        Debug.LogWarning(score);
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
    
    public void gameEnd(string message)
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

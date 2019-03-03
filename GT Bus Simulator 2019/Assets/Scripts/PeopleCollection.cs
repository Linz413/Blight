using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleCollection : MonoBehaviour
{
	public Text scoreText;
	int score = 0;
    int killedStudents = 0;
    public float timeToWin = 30;
    private float currentTime = 0;

	void Start() {
		scoreText.text = "Picked Up: " + score.ToString();
	}

    private void Update()
    {
        currentTime += Time.deltaTime;   
        if (currentTime > timeToWin)
        {
            // LOSE CONDITION
        }
    }

    public void ReceivePickup() {
    	score++;
    	//scoreText.text = "Picked Up: " + score.ToString();
        if (score >= 100 && currentTime <= timeToWin)
        {
            // WIN CONDITION
        }
    }

    public void HitStudent()
    {
        killedStudents++;
        if (killedStudents >= 3)
        {
            // LOSE CONDITION
        }
    }
}

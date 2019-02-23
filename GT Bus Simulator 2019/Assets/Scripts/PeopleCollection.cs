using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleCollection : MonoBehaviour
{
	public Text scoreText;
	int score = 0;

	void Start() {
		scoreText.text = "Picked Up: " + score.ToString();
	}
    public void ReceivePickup() {
    	score++;
    	scoreText.text = "Picked Up: " + score.ToString();
    }
}

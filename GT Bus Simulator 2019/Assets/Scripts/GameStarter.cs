using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBlueRouteGame()
    {
        //ADD AUDIO HERE
        SceneManager.LoadScene("BlueRoute");
    }

    public void StartRedRouteGame()
    {
        //ADD AUDIO HERE
        SceneManager.LoadScene("RedRoute");
    }

    public void ChooseRoute()
    {
        //ADD AUDIO HERE
        SceneManager.LoadScene("Instructions");
    }

    public void GoToControls()
    {
        //ADD AUDIO HERE
        SceneManager.LoadScene("Controls");
    }

    public void GoToScores()
    {
        SceneManager.LoadScene("HighScores");
    }

}

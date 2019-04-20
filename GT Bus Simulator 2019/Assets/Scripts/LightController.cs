using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Sub-components of the prefabs which are the lights to control
    public Light[] redLights;
    public Light[] yellowLights;
    public Light[] greenLights;

    public enum LightState
    {
        Red,
        Yellow,
        Green
    }

    public LightState state;
    private float timeLeft;

    // Randomized timers for lights
    private float yellowTimer;
    private float redTimer;
    private float greenTimer;

    private Light[] selectedLights;
    private Light[] allLights;

    // Start is called before the first frame update
    void Start()
    {
        state = LightState.Red;

        yellowTimer = Random.Range(1.0f, 3.0f);
        redTimer = Random.Range(4.0f, 6.0f);
        greenTimer = Random.Range(5.0f, 7.0f);
        timeLeft = redTimer;

        allLights = new Light[] { redLights[0], redLights[1], greenLights[0], greenLights[1], yellowLights[0], yellowLights[1] };
        selectedLights = new Light[] { redLights[0], redLights[1] };

        turnOffAllLights();
        turnOnSelectedLights();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            switch (state)
            {
                case LightState.Red:
                    state = LightState.Green;
                    timeLeft = greenTimer;
                    selectedLights = new Light[] { greenLights[0], greenLights[1] };
                    break;
                case LightState.Yellow:
                    state = LightState.Red;
                    timeLeft = redTimer;
                    selectedLights = new Light[] { redLights[0], redLights[1] };
                    break;
                case LightState.Green:
                    state = LightState.Yellow;
                    timeLeft = yellowTimer;
                    selectedLights = new Light[] { yellowLights[0], yellowLights[1] };
                    break;
            }

            turnOffAllLights();
            turnOnSelectedLights();
        }
    }

    private void turnOffAllLights()
    {
        foreach (Light light in allLights)
        {
            light.intensity = 0;
        }
    }

    private void turnOnSelectedLights()
    {
        foreach (Light light in selectedLights)
        {
            light.intensity = 6;
        }
    }
}

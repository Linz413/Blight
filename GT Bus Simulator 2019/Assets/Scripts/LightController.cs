using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Sub-components of the prefabs which are the lights to control
    public Light[] redLights;
    public Light[] yellowLights;
    public Light[] greenLights;

    private Light[] selectedLights;
    private Light[] allLights;

    // Start is called before the first frame update
    void Start()
    {
        allLights = new Light[] { redLights[0], redLights[1], greenLights[0], greenLights[1], yellowLights[0], yellowLights[1] };
        selectedLights = new Light[] { redLights[0], redLights[1] };

        turnOffAllLights();
        turnOnSelectedLights();
    }

    public void turnOnLight(LightState.State type)
    {
        switch (type)
        {
            case LightState.State.Green:
                selectedLights = new Light[] { greenLights[0], greenLights[1] };
                break;
            case LightState.State.Red:
                selectedLights = new Light[] { redLights[0], redLights[1] };
                break;
            case LightState.State.Yellow:
                selectedLights = new Light[] { yellowLights[0], yellowLights[1] };
                break;
        }

        turnOffAllLights();
        turnOnSelectedLights();
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

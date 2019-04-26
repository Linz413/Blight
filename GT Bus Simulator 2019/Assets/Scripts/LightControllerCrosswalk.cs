using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControllerCrosswalk : MonoBehaviour
{
    // Sub-components of the prefabs which are the lights to control
    public Light redLight;
    public Light yellowLight;
    public Light greenLight;
    public Light crossRed;
    public Light crossGreen;

    private Light[] selectedLights;
    private Light[] allLights;

    // Start is called before the first frame update
    void Start()
    {
        allLights = new Light[] { redLight, yellowLight, greenLight, crossRed, crossGreen };
        selectedLights = new Light[] { redLight, crossGreen };

        turnOffAllLights();
        turnOnSelectedLights();
    }

    public void turnOnLight(LightState.State type)
    {
        switch (type)
        {
            case LightState.State.Green:
                selectedLights = new Light[] { greenLight, crossRed };
                break;
            case LightState.State.Red:
                selectedLights = new Light[] { redLight, crossGreen };
                break;
            case LightState.State.Yellow:
                selectedLights = new Light[] { yellowLight, crossRed };
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

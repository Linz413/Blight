using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntersectionController : MonoBehaviour
{
    public GameObject[] northSouthLights;
    public GameObject[] eastWestLights;

    public enum IntersectionState
    {
        NorthSouthGreen,
        NorthSouthTransition,
        EastWestTransition,
        EastWestGreen
    }

    private IntersectionState state;
    private float timeLeft;

    // Randomized timers for lights
    private float yellowTimer;
    private float ewTimer;
    private float nsTimer;

    // Start is called before the first frame update
    void Start()
    {
        state = IntersectionState.EastWestTransition;

        yellowTimer = Random.Range(1.0f, 3.0f);
        ewTimer = Random.Range(4.0f, 6.0f);
        nsTimer = Random.Range(5.0f, 7.0f);

        timeLeft = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            LightState.State NSState = LightState.State.Red;
            LightState.State EWState = LightState.State.Red;
            switch (state)
            {
                case IntersectionState.NorthSouthGreen:
                    timeLeft = yellowTimer;
                    EWState = LightState.State.Red;
                    NSState = LightState.State.Yellow;
                    state = IntersectionState.NorthSouthTransition;
                    break;
                case IntersectionState.NorthSouthTransition:
                    timeLeft = ewTimer;
                    EWState = LightState.State.Green;
                    NSState = LightState.State.Red;
                    state = IntersectionState.EastWestGreen;
                    break;
                case IntersectionState.EastWestTransition:
                    timeLeft = nsTimer;
                    EWState = LightState.State.Red;
                    NSState = LightState.State.Green;
                    state = IntersectionState.NorthSouthGreen;
                    break;
                case IntersectionState.EastWestGreen:
                    timeLeft = yellowTimer;
                    EWState = LightState.State.Yellow;
                    NSState = LightState.State.Red;
                    state = IntersectionState.EastWestTransition;
                    break;
            }

            turnOnLights(NSState, EWState);
        }
    }

    private void turnOnLights(LightState.State NSState, LightState.State EWState)
    {
        foreach (GameObject obj in northSouthLights)
        {
            LightController controller = obj.GetComponent<LightController>();
            if (controller == null)
            {
                LightControllerCrosswalk crossController = obj.GetComponent<LightControllerCrosswalk>();
                crossController.turnOnLight(NSState);
            } else
            {
                controller.turnOnLight(NSState);
            }
        }

        foreach (GameObject obj in eastWestLights)
        {
            LightController controller = obj.GetComponent<LightController>();
            if (controller == null)
            {
                LightControllerCrosswalk crossController = obj.GetComponent<LightControllerCrosswalk>();
                crossController.turnOnLight(EWState);
            }
            else
            {
                controller.turnOnLight(EWState);
            }
        }
    }
}

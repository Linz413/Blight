using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class boostIndicator : MonoBehaviour
{
    private WheelDrive wheel;
    private float boost;
    private float boostLeft;
    private float maxBoost;
    private Image flame;

    public GameObject bus;
    // Start is called before the first frame update
    void Start()
    {
        flame = GetComponent<Image>();
        wheel = bus.GetComponent<WheelDrive>();
        boostLeft = wheel.boostTime;
        maxBoost = wheel.maxBoostTime;
        boost = boostLeft / maxBoost;
        flame.fillAmount = 0;


    }

    // Update is called once per frame
    void Update()
    {
        boostLeft = wheel.boostTime;
        maxBoost = wheel.maxBoostTime;
        boost = boostLeft / maxBoost;
        flame.fillAmount = boost;  
    }
}

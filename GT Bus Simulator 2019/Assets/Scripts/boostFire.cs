using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostFire : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem flame;
    public GameObject bus;
    private WheelDrive wheeldrive;
    private Smoke_Emitters sm;
    void Start()
    {
        
        flame = GetComponent<ParticleSystem>();
        wheeldrive = bus.GetComponent<WheelDrive>();
        sm = bus.GetComponent<Smoke_Emitters>();
        flame.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && wheeldrive.boostTime > 0)
        {
            flame.Play();
            
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || wheeldrive.boostTime <= 0) {
            flame.Stop();
}
    }
}

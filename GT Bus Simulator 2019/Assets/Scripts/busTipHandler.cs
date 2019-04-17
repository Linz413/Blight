using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class busTipHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public float downTime;
    private float time;
    public GameObject mainBus;

    private void OnTriggerExit(Collider other)
    {
        time = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        time += Time.deltaTime;
        if(time >= downTime)
        {
            Debug.Log("fall");
            //pc.gameEnd("Your Bus Has Fallen and Cannot Get Up");
            Vector3 target = mainBus.transform.rotation.eulerAngles;
            mainBus.transform.rotation = Quaternion.Euler(0, target.y, 0);
            

        }
    }
}

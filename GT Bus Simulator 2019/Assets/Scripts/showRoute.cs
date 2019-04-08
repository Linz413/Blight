using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showRoute : MonoBehaviour
{
    public Camera mainCamera;


    void Start()
    {

    }

     //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            mainCamera.cullingMask ^= (1 << LayerMask.NameToLayer("route"));
        }
        
    }

}

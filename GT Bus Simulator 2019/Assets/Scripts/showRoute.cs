using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showRoute : MonoBehaviour
{
    public Camera mainCamera;
//    private LayerMask showRouteMask = (1 << LayerMask.NameToLayer("route"));
//    private LayerMask hideRouteMask = (0 << LayerMask.NameToLayer("route"));

    void Start()
    {

    }

     //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            mainCamera.cullingMask ^= (1 << LayerMask.NameToLayer("route"));
//            if ((mainCamera.cullingMask &= (1 << LayerMask.NameToLayer("route")) ) != 0 )
//            {
//                mainCamera.cullingMask 
//            }
//            mainCamera.cullingMask = showRouteMask;
        }
        
    }
//    public GameObject bus;
//
//    private PeopleCollection busLogic;
//    public GameObject redRoute;
//    public GameObject blueRoute;
//    
//    // Start is called before the first frame update
//    void Start()
//    {
//        busLogic = bus.GetComponent<PeopleCollection>();
//        toggleRoute(busLogic.isRedRoute);
//        
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            busLogic.isRedRoute = !busLogic.isRedRoute;
//            toggleRoute(busLogic.isRedRoute);
//        }
//        
//    }
//
//    public void toggleRoute(bool isRed)
//    {
//        if (isRed)
//        {
//            redRoute.SetActive(true);
//            blueRoute.SetActive(false);
//        }
//        else
//        {
//            redRoute.SetActive(false);
//            blueRoute.SetActive(true);
//        }
//        
//    }

}

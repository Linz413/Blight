using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showRoute : MonoBehaviour
{
    public GameObject bus;

    private PeopleCollection busLogic;
    public GameObject redRoute;
    public GameObject blueRoute;
    
    // Start is called before the first frame update
    void Start()
    {
        busLogic = bus.GetComponent<PeopleCollection>();
        toggleRoute(busLogic.isRedRoute);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            busLogic.isRedRoute = !busLogic.isRedRoute;
            toggleRoute(busLogic.isRedRoute);
        }
        
    }

    public void toggleRoute(bool isRed)
    {
        if (isRed)
        {
            redRoute.SetActive(true);
            blueRoute.SetActive(false);
        }
        else
        {
            redRoute.SetActive(false);
            blueRoute.SetActive(true);
        }
        
    }
    
}

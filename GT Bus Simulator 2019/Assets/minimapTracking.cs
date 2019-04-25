using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = target.transform.position;
        targetPos.y = this.gameObject.transform.position.y;
        transform.position = targetPos;
        Vector3 targetRot = target.transform.rotation.eulerAngles;
        targetRot[0] = 0;
        targetRot[2] = 0;
        this.transform.rotation = Quaternion.Euler(targetRot);
    }
}

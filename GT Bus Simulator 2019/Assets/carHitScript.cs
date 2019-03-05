using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carHitScript : MonoBehaviour
{
    void  OnTriggerEnter(Collider c) {
        if ( c.attachedRigidbody != null) {
            PeopleCollection pc = c.attachedRigidbody.gameObject.GetComponent<PeopleCollection>();
            if (pc != null) {
                Debug.Log("hitAI");
                pc.HitAICar();
                // EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);
//                Destroy(this.gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamageBus : MonoBehaviour
{
    public int damage;
    public bool destroyObject;

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            PeopleCollection pc = c.attachedRigidbody.gameObject.GetComponent<PeopleCollection>();
            if (pc != null)
            {
                Debug.Log("hitObject");
                pc.HitObject(damage);
                if (destroyObject)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

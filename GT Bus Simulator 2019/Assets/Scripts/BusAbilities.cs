using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusAbilities : MonoBehaviour
{
    public Rigidbody projectile;
    public Rigidbody bus;
    public Text lureText;
    public AudioSource audioSource;
    public AudioClip pizzaThrowSound;
    public AudioClip jumpSound;

    public int lures = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        updateLures();
    }

    // Update is called once per frame
    void Update()
    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            audioSource.clip = jumpSound;
//            audioSource.Play();
//            Vector3 up = new Vector3(0, 1, 0);
//            Vector3 forceToApply = 5 * up;
//            bus.AddForce(forceToApply, ForceMode.VelocityChange);
//        }

        if (Input.GetKeyDown(KeyCode.E) && lures != 0)
        {
            audioSource.clip = pizzaThrowSound;
            audioSource.Play();
            lures--;
            updateLures();

            Vector3 up = new Vector3(0, 5, 0);
            Transform cameraTransform = Camera.main.transform;
            Vector3 moveVector = Quaternion.AngleAxis(10, Vector3.up) * cameraTransform.forward;
            moveVector = new Vector3(moveVector.x, moveVector.y * 0.1f, moveVector.z);
            Rigidbody newPizza = Instantiate(projectile, (bus.position + up), projectile.rotation);
            newPizza.velocity = moveVector * 25;
        }

        if (Input.GetKeyDown(KeyCode.Q) && lures != 0)
        {
            audioSource.clip = pizzaThrowSound;
            audioSource.Play();
            lures--;
            updateLures();

            Vector3 up = new Vector3(0, 5, 0);
            Transform cameraTransform = Camera.main.transform;
            Vector3 moveVector = Quaternion.AngleAxis(-10, Vector3.up) * cameraTransform.forward;
            moveVector = new Vector3(moveVector.x, moveVector.y * 0.1f, moveVector.z);
            Rigidbody newPizza = Instantiate(projectile, (bus.position + up), projectile.rotation);
            newPizza.velocity = moveVector * 25;
        }

    }
    
    private void updateLures()
    {
        lureText.text = "x" + lures.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
	private Rigidbody rb;
	public float speed;
    // Start is called before the first frame update
    void Start()
    {
     rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
    	float moveHorizontal = Input.GetAxis("Horizontal");
    	float moveVertical = Input.GetAxis("Vertical");
    	Vector3 movement = new Vector3(moveVertical,0.0f, -moveHorizontal);
    	rb.AddForce(movement * speed);
    	// if (Input.GetKeyDown("space") & rb.transform.position.y < 2) {
    	// 	//& rb.transform.position.y < 3
    	// 	Vector3 movementJump = new Vector3(0.0f, 250.0f, 0.0f);
    	// 	rb.AddForce(movementJump);
    	// }
    	// if (rb.transform.position.y < 0) {
    	// 	rb.transform.position = new Vector3(0.0f, .5f, 0.0f);
    	// 	lives--;
    	// 	SetCountText();
    	// }
    	// if (Input.GetKeyDown("r")) {
    	// 	SceneManager.LoadScene("MiniGame");
    	// }
    }
}

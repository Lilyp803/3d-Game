using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscript : MonoBehaviour {

	private Rigidbody rb;
    public float movementSpeed = 10;
	public float jumpHeight = 0;
	public CapsuleCollider col;
	public LayerMask GroundLayers;

	protected Joystick Movejoystick;
	protected Joystick ViewJoystick;
	protected JoyButton joybutton;
	protected bool jump;

	private bool ingame = true;
	void Start()
    {
		//Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
		Movejoystick = FindObjectOfType<Joystick>();
		ViewJoystick = FindObjectOfType<Joystick>();
		joybutton = FindObjectOfType<JoyButton>();
    }

	void Update()
	{
		var rb = GetComponent<Rigidbody>();
		transform.position += transform.rotation * (new Vector3(Movejoystick.Horizontal * 0.1f, 0f, Movejoystick.Vertical * 0.1f));
		//transform.rotation = new Quaternion(ViewJoystick.Horizontal, ViewJoystick.Vertical, 110f, 110f);


		if(!jump && joybutton.Pressed && isGrounded())
		{
			jump = true;
			rb.velocity += Vector3.up * 10f;
		}

		if(jump && !joybutton.Pressed)
		{
			jump = false;
		}

		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
		{
			transform.position += transform.forward * Time.deltaTime * movementSpeed * 2.5f;
		}
		else if ((Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift)))
		{
			transform.position += transform.forward * Time.deltaTime * movementSpeed;
		}
		else if (Input.GetKey("s"))
		{
			transform.position -= transform.forward * Time.deltaTime * movementSpeed;
		}

		if (Input.GetKey("a") && !Input.GetKey("d"))
		{
			transform.position -= transform.right * Time.deltaTime * movementSpeed;
		}
		else if (Input.GetKey("d") && !Input.GetKey("a"))
		{
			transform.position += transform.right * Time.deltaTime * movementSpeed;
		}
		if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
		}
		/*if (Input.GetKeyDown("escape"))
			Cursor.lockState = CursorLockMode.None;
		*/
		/*if (Input.GetKeyDown("escape")){
                 if(ingame)
                  {
				//Cursor.lockState = CursorLockMode.Locked;
				ingame = !ingame;
                   }
                else
                 {
				//Cursor.lockState = CursorLockMode.None;
				ingame = !ingame;
                  
                  }
                       
           }*/
	}
	void FixedUpdate()
	{ 
        
    }

	private bool isGrounded()
	{
		return Physics.CheckCapsule(col.bounds.center,
			new Vector3(col.bounds.center.x, col.bounds.min.y,
			col.bounds.center.z), col.radius * .9f, GroundLayers);
	}
}

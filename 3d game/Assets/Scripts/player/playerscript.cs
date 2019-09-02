using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class playerscript : MonoBehaviour {

	private Rigidbody rb;
    public float movementSpeed = 10;
	public float jumpHeight = 0;
	public CapsuleCollider col;
	public LayerMask GroundLayers;

	protected Joystick Movejoystick;
	protected Joystick ViewJoystick;
	protected JoyButton Jumpjoybutton;
	protected JoyButton Destroyjoybutton;
	protected JoyButton Placejoybutton;



	protected bool jump;

	[SerializeField] private string selectableTag = "Selectable";
	[SerializeField] private Material highlightMaterial;
	[SerializeField] private Material defualtMaterial;
	private Transform _selection;

	public float range = 100f;

	public Camera FPSCam;
	public GameObject Prefab;

	private float timerSpeed = 0.5f;
	private float lastTimeStamp;
	private bool canmineorplace = true;
		void Start()
	{
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
		Movejoystick = GameObject.FindGameObjectWithTag("Move Joystick").GetComponent<Joystick>();
		ViewJoystick = GameObject.FindGameObjectWithTag("View Joystick").GetComponent<Joystick>();
		Jumpjoybutton = GameObject.FindGameObjectWithTag("Jump Button").GetComponent<JoyButton>();
		Destroyjoybutton = GameObject.FindGameObjectWithTag("Destroy Button").GetComponent<JoyButton>();
		Placejoybutton = GameObject.FindGameObjectWithTag("Place Button").GetComponent<JoyButton>();
	}

	void Update()
	{
		var rb = GetComponent<Rigidbody>();
		transform.position += transform.rotation * (new Vector3(Movejoystick.Horizontal * 0.1f, 0f, Movejoystick.Vertical * 0.1f));

		if(!jump && Jumpjoybutton.Pressed && isGrounded())
		{
			jump = true;
			rb.velocity += Vector3.up * 10f;
		}

		if(jump && !Jumpjoybutton.Pressed)
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
		interactRaycast();
		canmineorplace = false;
		if (Time.time - lastTimeStamp >= timerSpeed)
		{
			canmineorplace = true;
		}
	}
	void FixedUpdate()
	{ 
        
    }

	void interactRaycast()
	{
		if (_selection != null)
		{
			var selectionRenderer = _selection.GetComponent<MeshRenderer>();
			selectionRenderer.material = defualtMaterial;
			_selection = null;
		}

		if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out var hit, range,9))
			{
				var selection = hit.transform;
				if (selection.CompareTag(selectableTag))
				{
					var selectionRenderer = selection.GetComponent<MeshRenderer>();
					if (selectionRenderer != null)
					{
						if (Movejoystick.Horizontal == 0 && Movejoystick.Vertical == 0)
						{
							if (ViewJoystick.Horizontal == 0 && ViewJoystick.Vertical == 0)
							{
								if (canmineorplace == true)
								{
									if (Destroyjoybutton.Pressed)
									{
										lastTimeStamp = Time.time;
										Destroy(selection.gameObject);
									}
									if (Placejoybutton.Pressed)
									{
										lastTimeStamp = Time.time;
									Instantiate(
										Prefab
										,
										selection.position + new Vector3(0, 1f, 0)
										,
										new Quaternion());
									}
								}

							}
						}
					selection.GetComponent<Renderer>().material = highlightMaterial;
					}
					_selection = selection;
				}
			}
	}

	private bool isGrounded()
	{
		return Physics.CheckCapsule(col.bounds.center,
			new Vector3(col.bounds.center.x, col.bounds.min.y,
			col.bounds.center.z), col.radius * .9f, GroundLayers);
	}
}

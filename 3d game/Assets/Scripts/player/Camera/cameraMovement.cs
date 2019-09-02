using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
	Vector2 MouseLook;
	Vector2 smoothV;
	protected Joystick joystick;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	GameObject character;

	// Start is called before the first frame update
	void Start()
	{
		character = this.transform.parent.gameObject;
		//joystick = GetComponent<>;
		joystick = GameObject.FindGameObjectWithTag("View Joystick").GetComponent<Joystick>();
	}

	// Update is called once per frame
	void Update()
	{
		//transform.localRotation = new Quaternion();

		var md = new Vector2(joystick.Horizontal, joystick.Vertical);
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
		MouseLook += smoothV;
		
		transform.localRotation = Quaternion.AngleAxis(-MouseLook.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis(MouseLook.x, character.transform.up);

	}
}

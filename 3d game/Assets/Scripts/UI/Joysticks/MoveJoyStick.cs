using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoyStick : MonoBehaviour
{
	protected Joystick joystick;
	protected JoyButton joybutton;
	// Use this for initialization
	void Start()
	{
		joystick = FindObjectOfType<Joystick>();
		joybutton = FindObjectOfType<JoyButton>();
	}

	// Update is called once per frame
	void Update()
	{
		var rb = GetComponent<Rigidbody>();

	}

}

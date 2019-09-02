using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ViewJoyStick : MonoBehaviour
{
	protected Joystick joystick;
	// Use this for initialization
	void Start()
	{
		joystick = FindObjectOfType<Joystick>();	}

	// Update is called once per frame
	void Update()
	{
		var rb = GetComponent<Rigidbody>();

	}

}
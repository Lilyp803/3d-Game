using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramouselook : MonoBehaviour
{
	Vector2 MouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	GameObject character;

    // Start is called before the first frame update
    void Start()
    {
		character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
		var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
		MouseLook += smoothV;

		transform.localRotation = Quaternion.AngleAxis(-MouseLook.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis(MouseLook.x, character.transform.up);

	}
}

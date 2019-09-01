using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 offset;
    [Range(0.001f, 1.0f)]
    public float SmoothFactor = 0.5f;

    // Start is called before the first frame update
    void Start() {
        offset = transform.position - PlayerTransform.position;
    }

    // Update is called once per frame
    void Update() {
        Vector3 newPos = PlayerTransform.position + offset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
    
    }
}
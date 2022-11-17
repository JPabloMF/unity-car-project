using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform cameraTarget;
    public float sSpeed = 1.0f;
    public Vector3 dist = new Vector3(0.5f,0.2f,0);
    private float horizontalInput;
    // public Transform lookTarget;

    // void FixedUpdate()
    // {
    //     Vector3 dPos = cameraTarget.position + dist;
    //     Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);
    //     transform.position = sPos;
    //     transform.LookAt(lookTarget.position);
    // }

    private void LateUpdate() {
        // horizontalInput = Input.GetAxis("Horizontal");
        // transform.position = cameraTarget.transform.position + dist;
        // transform.Rotate(Vector3.up, Time.deltaTime * 100.0f * horizontalInput);
        transform.LookAt(cameraTarget.transform.position);
    }

    // private void Update() {
    //     FixedUpdate();
    // }

}
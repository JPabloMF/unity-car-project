using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private float inputX, inputY;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform shipModel;
    [SerializeField] private float maxSpeed = 150.0f;
    [SerializeField] private float turnSpeed = 200.0f;
    [SerializeField] private Vector3 centerOfMass;
    private float rotation = 0f;
    private float shipModelRotation = 0f;
    void Start()
    {

    }

    void Update()
    {
        GetInputs();
    }

    private void LateUpdate()
    {
        Move();
    }

    private void GetInputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }
    
    private void FixedUpdate() {
        if (Input.GetKey("d"))
            rotation += 5 * Time.deltaTime;
        if (Input.GetKey("a"))
            rotation -= 5 * Time.deltaTime;
    }

    private void Move()
    {
        float forceY = inputY * maxSpeed * Time.deltaTime;
        float forceX = inputX * turnSpeed * Time.deltaTime;
        Quaternion deltaRotation;
        if (forceY < 0)
        {
            deltaRotation = Quaternion.Euler(new Vector3(0, 50, 0) * -forceX * Time.fixedDeltaTime);
        }
        else
        {
            deltaRotation = Quaternion.Euler(new Vector3(0, 50, 0) * forceX * Time.fixedDeltaTime);
        }
        rb.AddRelativeForce(new Vector3(-forceY, 0.1f, 0), ForceMode.Impulse);
        rb.MoveRotation(rb.rotation * deltaRotation);
        rb.centerOfMass = centerOfMass;
        // transform.position = new Vector3(0, 0.2f, 0);
        shipModelRotation = Mathf.Clamp(shipModelRotation, -20.0f, 20.0f);
        shipModelRotation += 5;
        shipModel.Rotate(new Vector3(shipModelRotation, 0, 0));
        // rotation = Mathf.Clamp(rotation, -1, 1);
        // transform.RotateAround(Vector3.right, Time.deltaTime * rotation);
    }
}

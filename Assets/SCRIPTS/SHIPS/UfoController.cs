using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoController : MonoBehaviour
{
    private float inputX, inputY;
    [SerializeField] private Rigidbody rb_;
    [SerializeField] private float maxSpeed = 150.0f;
    [SerializeField] private float turnSpeed = 200.0f;
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
        rb_.AddRelativeForce(new Vector3(-forceY, 0, 0), ForceMode.Impulse);
        rb_.MoveRotation(rb_.rotation * deltaRotation);
    }
}

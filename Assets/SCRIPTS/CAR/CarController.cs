using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public enum Axel
{
    Front,
    Rear
}

[Serializable]
public class Wheel
{
    public GameObject model;
    public WheelCollider collider;
    public Axel axel;
}
public class CarController : MonoBehaviour
{
    [SerializeField]
    private float maxAcceleration = 20.0f;
    private float initialAcceleration = 0f;
    [SerializeField]
    private float turnSensitivity = 1.0f;
    [SerializeField]
    private float maxSteerAngle = 45.0f;
    [SerializeField]
    private Vector3 _centerOfMass;
    [SerializeField]
    private List<Wheel> wheels;
    [SerializeField]
    private List<TrailRenderer> tireMarks;
    [SerializeField]
    private List<ParticleSystem> tireSmokes;
    [SerializeField]
    private ParticleSystem TurboParticle;

    private float inputX, inputY;
    private Rigidbody _rb;
    private float currentSpeed = 0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass;
        initialAcceleration = maxAcceleration;
    }

    private void Update()
    {
        AnimateWheels();
        GetInputs();
        Trails();
        Smoke();
    }

    private void LateUpdate()
    {
        Move();
        Turn();
    }

    private void GetInputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.collider.motorTorque = inputY * maxAcceleration * 200 * Time.deltaTime;
        }
        currentSpeed = inputY * maxAcceleration * 200 * Time.deltaTime;
    }

    private void Turn()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = inputX * turnSensitivity * maxSteerAngle;
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, 0.5f);
            }
        }
    }

    private void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion _rot;
            Vector3 _pos;
            wheel.collider.GetWorldPose(out _pos, out _rot);
            wheel.model.transform.position = _pos;
            wheel.model.transform.rotation = _rot;
        }
    }

    private void Trails()
    {
        if ((inputX > 0.7 || inputX < -0.7) || inputY == 0)
        {
            foreach (TrailRenderer trail in tireMarks)
            {
                trail.emitting = true;
            }
        }
        else
        {
            foreach (TrailRenderer trail in tireMarks)
            {
                trail.emitting = false;
            }
        }
    }

    private void Smoke()
    {
        if ((inputX > 0.7 || inputX < -0.7) && currentSpeed > 0)
        {
            foreach (ParticleSystem smoke in tireSmokes)
            {
                smoke.Play();
            }
        }
        else
        {
            foreach (ParticleSystem smoke in tireSmokes)
            {
                smoke.Stop();
            }
        }
    }

    private void Turbo()
    {
        TurboParticle.Play();
        maxAcceleration = 30000f;
        Task.Delay(2000).ContinueWith((task) =>
        {
            maxAcceleration = initialAcceleration;
            TurboParticle.Stop();
            TurboParticle.Clear(true);
        }, TaskScheduler.FromCurrentSynchronizationContext());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Turbo")
        {
            Turbo();
        }
    }
}
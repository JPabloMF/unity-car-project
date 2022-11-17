using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public float speed = 4.0f;
    public float turboSpeed = 5.0f;
    public float baseSpeed = 4.0f;
    public float turnSpeed = 100.0f;
    public float airSpeed = 4.0f;
    private float horizontalInput;
    private float forwardInput;
    private GameObject Main;
    Main MainScript;
    public TextMeshPro lapCounterText;
    public TextMeshPro finalLapText;
    private int lapCounter = 0;
    public AudioSource audioSource;
    public ParticleSystem Fire1, Fire2, Fire3;
    public ParticleSystem SpeedParticle;
    public ParticleSystem PowerUpParticle;
    public Light FireLight1, FireLight2;
    public Canvas FinishedRaceCanvas;
    private bool isRaceFinished = false;
    private bool isInPortal = false;
    public bool gameIsLoading = false;
    public Canvas loader;

    void Start()
    {
        showLoader();
        Main = GameObject.Find("Main");
        MainScript = Main.GetComponent<Main>();
        lapCounterText.enabled = false;
        finalLapText.enabled = false;
        FinishedRaceCanvas.enabled = false;
        // GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.5f, 0);
        // GetComponent<Rigidbody>().AddRelativeTorque(0f, 0f, 1f, ForceMode.Acceleration);
    }

    void handleTurbo()
    {
        PowerUpParticle.Play();
        if (!Fire1.isEmitting && !Fire2.isEmitting && !Fire3.isEmitting)
        {
            speed = turboSpeed;
            Fire1.Play();
            Fire2.Play();
            Fire3.Play();
            SpeedParticle.Play();
            FireLight1.intensity = 1;
            FireLight2.intensity = 1;
            Task.Delay(2000).ContinueWith((task) =>
            {
                speed = baseSpeed;
                Fire1.Stop();
                Fire2.Stop();
                Fire3.Stop();
                SpeedParticle.Stop();
                Fire1.Clear(true);
                Fire2.Clear(true);
                Fire3.Clear(true);
                SpeedParticle.Clear(true);
                FireLight1.intensity = 0;
                FireLight2.intensity = 0;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }

    void handleMovement()
    {
        float currentSpeed = Time.deltaTime * speed * forwardInput;

        // get the user's inputs
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        // move and rorate
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        if ((currentSpeed) != 0)
        {
            // if (!audioSource.isPlaying)
            // {
            //     audioSource.Play();
            // }
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        }
    }

    void handleFinalLap()
    {
        finalLapText.enabled = true;
        Task.Delay(2000).ContinueWith((task) =>
        {
            finalLapText.enabled = false;
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    void handleFinishedRace(){
        isRaceFinished = true;
        FinishedRaceCanvas.enabled = true;
    }

    void showLoader()
    {
        gameIsLoading = true;
        Time.timeScale = 0;
        Task.Delay(2000).ContinueWith((task) =>
        {
            // MainScript.PlayIntroSound();
            gameIsLoading = false;
            loader.enabled = false;
            Time.timeScale = 1;
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    void Update()
    {
        if (!gameIsLoading) {
            if (MainScript.timerFinished == true && !isRaceFinished)
            {
                handleMovement();
                if (Mathf.Abs(transform.localRotation.eulerAngles.z) > 90f)
                {
                    Debug.Log("volcooo");
                    GetComponent<Rigidbody>().AddRelativeTorque(0f, 10f, 1f, ForceMode.Acceleration);
                }
            }
            if (MainScript.timerFinished == true)
            {
                lapCounterText.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            lapCounter += 1;
            lapCounterText.text = "Lap " + lapCounter.ToString() + "/3";
            if (lapCounter == 3) {
                handleFinalLap();
            }
            if (lapCounter == 4)
            {
                handleFinishedRace();
            }
        }
        if (other.tag == "Turbo")
        {
            handleTurbo();
        }
        if (other.tag == "PortalEntry") {
            isInPortal = true;
            speed = 13;
            transform.localScale = new Vector3(0.17f, 0.17f, 0.17f);
        }
        if (other.tag == "PortalExit")
        {
            isInPortal = false;
            speed = baseSpeed;
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Map" && !isInPortal)
        {
            speed = airSpeed;
        }
        if (other.gameObject.tag == "Map" && isInPortal)
        {
            speed = airSpeed - 5;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Map" && !isInPortal)
        {
            speed = baseSpeed;
        }
        if (other.gameObject.tag == "Map" && isInPortal)
        {
            speed = 13;
        }
    }
}

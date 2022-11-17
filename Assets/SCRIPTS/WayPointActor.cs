using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointActor : MonoBehaviour
{
    public float speed = 25f;
    public float baseSpeed = 25f;
    public float airSpeed = 5f;
    public Transform target;
    private GameObject Main;
    Main MainScript;
    private bool isInPortal = false;

    void Start()
    {
        Main = GameObject.Find("Main");
        MainScript = Main.GetComponent<Main>();
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        speed = baseSpeed;
    }

    void Update()
    {
        if (MainScript.timerFinished == true)
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        if (speed < 1)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WayPoint")
        {
            target = other.gameObject.GetComponent<WayPoint>().nextPoint;
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
        if (other.tag == "PortalEntry")
        {
            isInPortal = true;
            speed = 13;
            transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }
        if (other.tag == "PortalExit")
        {
            isInPortal = false;
            speed = baseSpeed;
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }
    private void OnCollisionExit(Collision other)
    {
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

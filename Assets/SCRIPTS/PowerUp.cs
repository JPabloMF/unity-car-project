using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other) {
        // if (other.tag == "Car") {
        //     Destroy(gameObject);
        // }
    }
}

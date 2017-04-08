using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    private Rigidbody rb;
    private float rotationRate;
    private Vector3 rotationAxis;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        Vector3 target = GameObject.Find("World").transform.position;
        rb.AddForce((target - transform.position).normalized * 7f);

        rotationRate = Random.Range(30f, 100f);
        rotationAxis = Random.onUnitSphere;
	}

    private void Update() {
        transform.Rotate(rotationAxis, Time.deltaTime * rotationRate, Space.World);
    }
}

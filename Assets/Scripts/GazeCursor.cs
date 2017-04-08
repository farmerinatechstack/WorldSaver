using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeCursor : MonoBehaviour {
    public static GazeCursor Instance;
    public GameObject FocusedObject;
    public GameObject explosion;
    public AudioSource src;
    

	void Awake () {
		if (Instance == null) {
            Instance = this;
        }
	}
	
	void Update () {
        // Configure a raycast based on the user's gaze direction
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;
        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo)) {
            // If an object is hit, store the access in the FocusedObject variable for other scripts to access and move the cursor on top of the object.
            if (hitInfo.collider.gameObject.GetComponent<Asteroid>() != null) {
                Destroy(hitInfo.collider.gameObject);
                src.Play();
                Instantiate(explosion, hitInfo.collider.gameObject.transform.position, Random.rotation);
            } else {
                FocusedObject = hitInfo.collider.gameObject;
            }
            transform.position = hitInfo.point;
        } else {
            // If no object is hit, set the FocusedObject to null and move the cursor to a set distance in front of the user.
            FocusedObject = null;
            transform.position = headPosition + gazeDirection * 2;
        }
	}
}

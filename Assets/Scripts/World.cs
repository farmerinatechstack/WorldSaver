using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour {
    public static World Instance;
    public GameObject[] asteroidObjects;

    private Vector3 manipulationPrevPosition;

	void Awake () {
		if (Instance == null) {
            Instance = this;
        }
	}

    private void Update() {
        transform.Rotate(Vector3.up, Time.deltaTime * 5f, Space.World);
    }
	
	public void StartManipulation(Vector3 position) {
        manipulationPrevPosition = position;
    }

    public void UpdateManipulation(Vector3 position) {
        Vector3 delta = (position - manipulationPrevPosition) * 2;
        gameObject.transform.position += delta;
        manipulationPrevPosition = position;
    }

    public void StartGame() {
        GestureManager.Instance.StopManipulation();
        StartCoroutine(AsteroidSpawn());
    }

    IEnumerator AsteroidSpawn() {
        while (true) {
            Instantiate(asteroidObjects[Random.Range(0,asteroidObjects.Length-1)], transform.position + Random.onUnitSphere * 2, Random.rotation);
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Reset();
    }

    public void Reset() {
        SceneManager.LoadScene(0);
    }
}

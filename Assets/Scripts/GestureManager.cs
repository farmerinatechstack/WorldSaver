using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureManager : MonoBehaviour {
    public static GestureManager Instance;
    public bool isManipulating;

    // A recognizer to use when manipulation objects.
    private GestureRecognizer ManipulationRecognizer;
	
	void Awake () {
        if (Instance == null) {
            Instance = this;
        }

        isManipulating = false;
        ManipulationRecognizer = new GestureRecognizer();
        ManipulationRecognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);

        // Subscribe to gesture events from the gesture recognizer.
        ManipulationRecognizer.ManipulationStartedEvent += ManipulationStarted;
        ManipulationRecognizer.ManipulationUpdatedEvent += ManipulationUpdated;
        ManipulationRecognizer.ManipulationCanceledEvent += ManipulationEnded;
        ManipulationRecognizer.ManipulationCompletedEvent += ManipulationEnded;

        ManipulationRecognizer.StartCapturingGestures();
	}

    private void OnDestroy() {
        ManipulationRecognizer.ManipulationStartedEvent -= ManipulationStarted;
        ManipulationRecognizer.ManipulationUpdatedEvent -= ManipulationUpdated;
        ManipulationRecognizer.ManipulationCanceledEvent -= ManipulationEnded;
        ManipulationRecognizer.ManipulationCompletedEvent -= ManipulationEnded;
    }

    private void ManipulationStarted(InteractionSourceKind source, Vector3 position, Ray headRay) {
        if (GazeCursor.Instance.FocusedObject != null) {
            isManipulating = true;
            GazeCursor.Instance.FocusedObject.SendMessage("StartManipulation", position);
        }
    }

    private void ManipulationUpdated(InteractionSourceKind source, Vector3 position, Ray headRay) {
        if (GazeCursor.Instance.FocusedObject != null) {
            isManipulating = true;
            GazeCursor.Instance.FocusedObject.SendMessage("UpdateManipulation", position);
        }
    }

    private void ManipulationEnded(InteractionSourceKind source, Vector3 position, Ray headRay) {
        isManipulating = false;
    }

    public void StopManipulation() {
        ManipulationRecognizer.StopCapturingGestures();
    }
}

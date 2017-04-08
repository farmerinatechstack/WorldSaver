using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceManager : MonoBehaviour {
    KeywordRecognizer keywordRecognizer; // listens for keywords
    delegate void KeywordAction();
    Dictionary<string, KeywordAction> keywordCollection; // dictionary of commands to functions

    void Start() {
        // Create a dictionary mapping from verbal commands to functions we will invoke.
        keywordCollection = new Dictionary<string, KeywordAction>();
        keywordCollection.Add("Start", SendStart);
        keywordCollection.Add("Reset", SendReset);

        // Start the keyword recognizer.
        keywordRecognizer = new KeywordRecognizer(keywordCollection.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // When a keyword is recognized, invoke the corresponding function.
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        KeywordAction keywordAction;
        if (keywordCollection.TryGetValue(args.text, out keywordAction)) {
            keywordAction.Invoke();
        }
    }

    private void SendStart() {
        Debug.Log("Calling Start Game");
        World.Instance.StartGame();
    }

    private void SendReset() {
        Debug.Log("Calling Reset");
        World.Instance.Reset();
    }
}


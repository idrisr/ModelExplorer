using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Academy.HoloToolkit.Unity;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class KeywordManager : MonoBehaviour {
    // KeywordRecognizer object.
    KeywordRecognizer keywordRecognizer;

    // Defines which function to call when a keyword is recognized.
    delegate void KeywordAction(PhraseRecognizedEventArgs args);
    Dictionary<string, KeywordAction> keywordCollection;

    void Start()
    {
        keywordCollection = new Dictionary<string, KeywordAction>();

        // Add keyword to start manipulation.
        keywordCollection.Add("Reset Tower", ResetTower);
        keywordCollection.Add("Rotate", RotateBrick);

        // Initialize KeywordRecognizer with the previously added keywords.
        keywordRecognizer = new KeywordRecognizer(keywordCollection.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void OnDestroy()
    {
        keywordRecognizer.Dispose();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction keywordAction;

        if (keywordCollection.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke(args);
        }
    }

    private void RotateBrick(PhraseRecognizedEventArgs args) {
        InteractibleManager.Instance.FocusedGameObject.SendMessage("RotateBrick");
    }

    private void ResetTower(PhraseRecognizedEventArgs args)
    {
        TowerManager.Instance.ResetCommand(args);
    }
}
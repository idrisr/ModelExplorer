using System.Collections;
using Academy.HoloToolkit.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class TowerManager : Singleton<TowerManager>
{
    public void ResetCommand(PhraseRecognizedEventArgs args)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
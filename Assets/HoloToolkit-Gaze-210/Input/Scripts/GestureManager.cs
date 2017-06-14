using UnityEngine;
using UnityEngine.VR.WSA.Input;

namespace Academy.HoloToolkit.Unity
{
    /// <summary>
    /// GestureManager contains event handlers for subscribed gestures.
    /// </summary>
    public class GestureManager : Singleton<GestureManager>
    {
        private GestureRecognizer ManipulationRecognizer;
        public bool isManipulating;

        void Awake()
        {
            ManipulationRecognizer = new GestureRecognizer();
            ManipulationRecognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);
            ManipulationRecognizer.ManipulationStartedEvent += ManipulationRecognizer_ManipulationStartedEvent;
            ManipulationRecognizer.ManipulationUpdatedEvent += ManipulationRecognizer_ManipulationUpdatedEvent;
            ManipulationRecognizer.ManipulationCompletedEvent += ManipulationRecognizer_ManipulationCompletedEvent;
            ManipulationRecognizer.ManipulationCanceledEvent += ManipulationRecognizer_ManipulationCanceledEvent;
        }

        void Start()
        {
            ManipulationRecognizer.StartCapturingGestures();
        }

        void OnDestroy()
        {
            ManipulationRecognizer.StopCapturingGestures();
        }

        private void NavigationRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
        {
            InteractibleManager.Instance.FocusedGameObject.SendMessage("OnTapped");
        }

        private void ManipulationRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            isManipulating = true;
            InteractibleManager.Instance.FocusedGameObject.SendMessage("PerformManipulationStart", position);
        }

        private void ManipulationRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            InteractibleManager.Instance.FocusedGameObject.SendMessage("PerformManipulationUpdate", position);
        }

        private void ManipulationRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            isManipulating = false;
            InteractibleManager.Instance.FocusedGameObject.SendMessage("PerformManipulationEnd");
        }

        private void ManipulationRecognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            isManipulating = false;
            InteractibleManager.Instance.FocusedGameObject.SendMessage("PerformManipulationEnd");
        }
    }
}
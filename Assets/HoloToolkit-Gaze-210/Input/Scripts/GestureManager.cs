using UnityEngine;
using UnityEngine.VR.WSA.Input;

namespace Academy.HoloToolkit.Unity
{
    /// <summary>
    /// GestureManager contains event handlers for subscribed gestures.
    /// </summary>
    public class GestureManager : MonoBehaviour
    {
        private GestureRecognizer NavigationRecognizer;
        private GestureRecognizer ManipulationRecognizer;
        private GestureRecognizer ActiveRecognizer;
        private bool isManipulating;

        void Awake()
        {
            NavigationRecognizer = new GestureRecognizer();
            NavigationRecognizer.SetRecognizableGestures(GestureSettings.Tap);
            NavigationRecognizer.TappedEvent += NavigationRecognizer_TappedEvent;

            ManipulationRecognizer = new GestureRecognizer();
            ManipulationRecognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);
            ManipulationRecognizer.ManipulationStartedEvent += ManipulationRecognizer_ManipulationStartedEvent;
            ManipulationRecognizer.ManipulationUpdatedEvent += ManipulationRecognizer_ManipulationUpdatedEvent;
            ManipulationRecognizer.ManipulationCompletedEvent += ManipulationRecognizer_ManipulationCompletedEvent;
            ManipulationRecognizer.ManipulationCanceledEvent += ManipulationRecognizer_ManipulationCanceledEvent;

            ResetGestureRecognizers();
        }

        public void ResetGestureRecognizers()
        {
            // Default to the navigation gestures.
            Transition(NavigationRecognizer);
        }

        private void Transition(GestureRecognizer newRecognizer)
        {
            if (newRecognizer == null)
            {
                return;
            }

            if (newRecognizer == ManipulationRecognizer)
            {
                isManipulating = true;
            }
            else {
                isManipulating = false;
            }

            if (ActiveRecognizer != null)
            {
                if (ActiveRecognizer == newRecognizer)
                {
                    return;
                }

                ActiveRecognizer.CancelGestures();
                ActiveRecognizer.StopCapturingGestures();
            }

            newRecognizer.StartCapturingGestures();
            ActiveRecognizer = newRecognizer;
        }

        void OnDestroy()
        {
            NavigationRecognizer.StopCapturingGestures();
            ManipulationRecognizer.StopCapturingGestures();
        }

        private void NavigationRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
        {
            if (FocusSelectable() && !isManipulating)
            {
                InteractibleManager.Instance.FocusedGameObject.SendMessage("OnTapped");
                Transition(ManipulationRecognizer);
            }
        }

        private void ManipulationRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            if (FocusSelectable())
            {
                InteractibleManager.Instance.FocusedGameObject.SendMessage("PerformManipulationStart", position);
            }
        }

        private void ManipulationRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            if (FocusSelectable())
            {
                InteractibleManager.Instance.FocusedGameObject.SendMessage("PerformManipulationUpdate", position);
            }
        }

        private void ManipulationRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            if (FocusSelectable())
            {
                InteractibleManager.Instance.FocusedGameObject.SendMessage("PerformManipulationEnd");
                Transition(NavigationRecognizer);
            }
        }

        private void ManipulationRecognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
        {
            if (FocusSelectable())
            {
                InteractibleManager.Instance.FocusedGameObject.SendMessage("PerformManipulationEnd");
                Transition(NavigationRecognizer);
            }
        }

        private bool FocusSelectable() {
            GameObject focusedObject = InteractibleManager.Instance.FocusedGameObject;
            return (focusedObject != null && focusedObject.GetComponent<Interactible>() != null);
        }
    }
}
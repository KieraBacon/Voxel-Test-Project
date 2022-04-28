using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent eventToObserve;
        [SerializeField] private UnityEvent OnEventRaised;

        private void OnEnable()
        {
            eventToObserve.OnEventRaised += InvokeEvent;
        }

        private void OnDisable()
        {
            eventToObserve.OnEventRaised -= InvokeEvent;
        }

        private void InvokeEvent()
        {
            OnEventRaised.Invoke();
        }
    }
}
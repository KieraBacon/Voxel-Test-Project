using UnityEngine;
using UnityEngine.Events;

namespace VoxelProject.Utility
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent eventToObserve;
        [SerializeField] private UnityEvent onEventRaised;

        private void OnEnable()
        {
            eventToObserve.onRaised += OnEventRaised;
        }

        private void OnDisable()
        {
            eventToObserve.onRaised -= OnEventRaised;
        }

        private void OnEventRaised()
        {
            onEventRaised.Invoke();
        }
    }
}
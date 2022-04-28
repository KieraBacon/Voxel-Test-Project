using System;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Event System/Game Event", order = 1)]
    public class GameEvent : ScriptableObject
    {
        public event Action OnEventRaised;

        public void Invoke()
        {
            OnEventRaised?.Invoke();
        }
    }
}
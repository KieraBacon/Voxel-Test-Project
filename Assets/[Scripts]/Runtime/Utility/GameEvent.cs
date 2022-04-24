using System;
using UnityEngine;

namespace VoxelProject.Utility
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Event System/Game Event", order = 1)]
    public class GameEvent : ScriptableObject
    {
        public event Action onRaised;

        public void Invoke()
        {
            onRaised?.Invoke();
        }
    }
}
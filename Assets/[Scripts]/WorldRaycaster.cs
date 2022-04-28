using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.EventSystems;

namespace VoxelProject.Utility
{
    public class WorldRaycaster : PhysicsRaycaster
    {
        public ReadOnlyCollection<RaycastResult> Contacts => _contacts.AsReadOnly();
        private List<RaycastResult> _contacts = new List<RaycastResult>();
        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            _contacts.Clear();
            base.Raycast(eventData, resultAppendList);
            _contacts = resultAppendList;
        }
    }
}

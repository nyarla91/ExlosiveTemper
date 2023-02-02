using System;
using Gameplay.Character.Player;
using UnityEngine;

namespace Gameplay.Rooms
{
    public class RoomExit : MonoBehaviour
    {
        [SerializeField] private Room _originalRoom;
        [SerializeField] private Vector3 _nextRoomOffset;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerComposition _))
            {
                _originalRoom.NextLevel(_nextRoomOffset);
            }
        }
    }
}
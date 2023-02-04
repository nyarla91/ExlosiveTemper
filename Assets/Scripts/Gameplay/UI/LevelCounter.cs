using System;
using Gameplay.Rooms;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class LevelCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmp;
        
        private Room _room;
        
        public void Init(Room room)
        {
            _room = room;
        }

        private void Update()
        {
            _tmp.text = _room.Level.ToString();
        }
    }
}
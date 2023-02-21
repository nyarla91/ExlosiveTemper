using System;
using Extentions;
using UnityEngine;
using UnityEngine.VFX;

namespace Gameplay.Spells.View
{
    public class ContiniousSpellView : LazyGetComponent<ContiniousSpell>
    {
        [SerializeField] private VisualEffect _aura;
        
        private void Awake()
        {
            Lazy.Activated += _aura.Play;
            Lazy.Finished += _aura.Stop;
        }
    }
}
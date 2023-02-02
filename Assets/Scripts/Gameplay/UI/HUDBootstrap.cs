using Extentions;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.UI
{
    public class HUDBootstrap : MonoBehaviour
    {
        [SerializeField] private ResourceBar _healthBar;
        [SerializeField] private ResourceBar _heatBar;
        [SerializeField] private ChargedShotView _chargedShotView;
        [SerializeField] private ConsumableView _healthConsumable;
        [SerializeField] private ConsumableView _heatConsumable;
        [SerializeField] private SpellView[] _spells;
        
        [Inject]
        private PlayerComposition Player { get; set; }
        
        private void Start()
        {
            _healthBar.Init(Player.Vitals.Health);
            _heatBar.Init(Player.Resources.Heat);
            _chargedShotView.Init(Player.Weapons);
            _healthConsumable.Init(Player.Inventory.HealthConsumable);
            _heatConsumable.Init(Player.Inventory.HeatConsumable);
            _spells.Foreach(spell => spell.Init(Player.Resources, Player.Spells));
        }
    }
}
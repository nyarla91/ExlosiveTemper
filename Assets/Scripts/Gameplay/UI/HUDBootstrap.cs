using Extentions;
using Gameplay.Character.Player;
using Gameplay.Rooms;
using UnityEngine;
using Zenject;

namespace Gameplay.UI
{
    public class HUDBootstrap : Transformable
    {
        [SerializeField] private ResourceBar _healthBar;
        [SerializeField] private ResourceBar _heatBar;
        [SerializeField] private ChargedShotView _chargedShotView;
        [SerializeField] private ConsumableView _healthConsumable;
        [SerializeField] private ConsumableView _heatConsumable;
        [SerializeField] private InteractableView _interactable;
        [SerializeField] private SpellView[] _spells;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private LevelCounter _levelCounter;
        [SerializeField] private Room _room;
        
        [Inject]
        private PlayerComposition Player { get; set; }
        
        private void Awake()
        {
            _healthBar.Init(Player.Vitals.Health);
            _heatBar.Init(Player.Resources.Heat);
            _chargedShotView.Init(Player.Weapons);
            _healthConsumable.Init(Player.Inventory.HealthConsumable);
            _heatConsumable.Init(Player.Inventory.HeatConsumable);
            _interactable.Init(Player.Interaction);
            _spells.Foreach(spell => spell.Init(Player.Resources, Player.Spells));
            _enemySpawner.HUD = RectTransform;
            _levelCounter.Init(_room);
        }
    }
}
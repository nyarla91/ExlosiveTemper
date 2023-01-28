using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.UI
{
    public class HUDBootstrap : MonoBehaviour
    {
        [SerializeField] private ResourceBar _healthBar;
        [SerializeField] private ResourceBar _heatBar;
        
        [Inject]
        private void Construct(PlayerComposition player)
        {
            _healthBar.Init(player.Vitals.Health);
            _heatBar.Init(player.Resources.Heat);
        }
    }
}
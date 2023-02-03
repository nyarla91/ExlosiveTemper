using Gameplay.Character.Player;
using UIUtility;
using UnityEngine;
using Zenject;

namespace Gameplay.UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private Menu _menu;
        [Inject] private PlayerComposition PlayerComposition { get; set; }

        private void Start()
        {
            PlayerComposition.Vitals.HealthIsOver += _menu.Open;
        }
    }
}
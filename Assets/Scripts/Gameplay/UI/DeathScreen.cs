using Gameplay.Character.Player;
using UIUtility;
using UnityEngine;
using Zenject;

namespace Gameplay.UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Menu _menu;
        [Inject] private PlayerComposition PlayerComposition { get; set; }

        private void Start()
        {
            PlayerComposition.Vitals.HealthIsOver += Die;
        }

        private void Die()
        {
            _menu.Open();
            _audioSource.Play();
        }
    }
}
using Gameplay.Character.Player;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Gameplay.UI
{
    public class ChargedShotView : MonoBehaviour
    {
        [SerializeField] private GameObject _cooldown;
        [SerializeField] private Image _cooldownRadial;
        [SerializeField] private TMP_Text _cooldownValue;

        private PlayerWeapons _weapons;
        
        public void Init(PlayerWeapons weapons)
        {
            _weapons = weapons;
        }

        private void Update()
        {
            float cooldownLeft = _weapons.ChargedShotCooldown.TimeLeft;
            bool isCooldownOn = _weapons.ChargedShotCooldown.IsOn;
            _cooldown.SetActive(isCooldownOn);
            _cooldownRadial.fillAmount = cooldownLeft / _weapons.ChargedShotCooldown.Length;
            _cooldownValue.gameObject.SetActive(isCooldownOn);
            _cooldownValue.text = Mathf.CeilToInt(cooldownLeft).ToString();
        }
    }
}
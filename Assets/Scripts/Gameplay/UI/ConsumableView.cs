using Gameplay.Consumables;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class ConsumableView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _quantity;

        public void Init(Consumable consumable)
        {
            consumable.OnQuantityChanged += ApplyQuantity;
        }

        private void ApplyQuantity(int quantity)
        {
            _quantity.text = quantity.ToString();
        }
    }
}
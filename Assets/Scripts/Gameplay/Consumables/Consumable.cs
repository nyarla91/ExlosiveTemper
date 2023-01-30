using Gameplay.Character.Player;

namespace Gameplay.Consumables
{
    public abstract class Consumable
    {
        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            private set => _quantity = value;
        }

        public void AddOne() => Quantity++;

        public bool TryConsume(PlayerComposition player)
        {
            if (Quantity < 1)
                return false;
            ConsumeEffect(player);
            Quantity--;
            return true;
        }

        public abstract void ConsumeEffect(PlayerComposition player);
    }
}
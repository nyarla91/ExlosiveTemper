using Extentions.Factory;
using Gameplay.Character.Player;
using UnityEngine;

namespace Gameplay.Collectables
{
    public abstract class Collectable : PooledObject
    {
        public abstract void OnCollect(PlayerComposition player);
    }
}
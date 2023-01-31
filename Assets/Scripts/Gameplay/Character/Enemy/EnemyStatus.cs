using Extentions;

namespace Gameplay.Character.Enemy
{
    public class EnemyStatus : LazyGetComponent<EnemyComposition>
    {
        private void Awake()
        {
            Lazy.VitalsPool.OnHealthOver += Die;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
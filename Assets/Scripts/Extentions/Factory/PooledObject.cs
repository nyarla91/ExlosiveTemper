using System;

namespace Extentions.Factory
{
    public class PooledObject : Transformable
    {
        private PoolFactory _factory;

        public event Action<PooledObject> OnPoolDisable;

        public virtual void PoolInit(PoolFactory factory)
        {
            _factory = factory;
        }
        
        public virtual void Reset() { }

        public virtual void PoolDisable()
        {
            OnPoolDisable?.Invoke(this);
            if (_factory != null)
                _factory.DisableObject(this);
            else
                Destroy(gameObject);
        }
    }
}
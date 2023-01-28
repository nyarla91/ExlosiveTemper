using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Extentions.Factory
{
    public class PoolFactory : Transformable
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private List<TaggedObject> _pool = new List<TaggedObject>();
        
        [Inject] private ContainerFactory ContainerFactory { get; set; }

        public void DisableObject(PooledObject objectToRemove)
        {
            objectToRemove.gameObject.SetActive(false);
            objectToRemove.Transform.SetParent(Transform);
        }

        public T GetNewObject<T>(Vector3 position, Transform parent = null, string tag = "_") where T : PooledObject
            =>  GetNewObject<T>(position, null, parent, tag);
        
        public T GetNewObject<T>(Vector3 position, GameObject overridePrefab, Transform parent = null, string tag = "_") where T : PooledObject
        {
            GameObject prefab = overridePrefab ?? _prefab;
            
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].PooledObject.gameObject.activeSelf || _pool[i].PooledObject is not T || ! _pool[i].Tag.Equals(tag))
                    continue;
                
                T newObject = (T) _pool[i].PooledObject;
                newObject.gameObject.SetActive(true);
                newObject.Transform.position = position;
                newObject.Transform.SetParent(parent);
                newObject.Reset();
                return newObject;
            }
            return InstantiatePrefab<T>(position, prefab, parent, tag);
        }

        private T InstantiatePrefab<T>(Vector3 position, GameObject prefab, Transform parent, string tag) where T : PooledObject
        {
            T newObject = ContainerFactory.Instantiate<T>(prefab, position, parent);
            newObject.Transform.position = position;
            newObject.Transform.parent = parent;
            newObject.PoolInit(this);
            newObject.gameObject.SetActive(true);
            _pool.Add(new TaggedObject(tag, newObject));
            return newObject;
        }

        [Serializable]
        private class TaggedObject
        {
            [SerializeField] private string _tag;
            [SerializeField] private PooledObject _pooledObject;

            public string Tag => _tag;

            public PooledObject PooledObject => _pooledObject;

            public TaggedObject(string tag, PooledObject pooledObject)
            {
                _tag = tag;
                _pooledObject = pooledObject;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extentions
{
    public class OverlapTrigger : Transformable
    {
        [SerializeField] private List<Collider> _colliders = new List<Collider>();

        public Collider[] Content => _colliders.ToArray();
        
        public T[] GetContent<T>() where T : Component
            => GetContent<T>(int.MaxValue);
        
        public T[] GetContent<T>(LayerMask layerMask) where T : Component
        {
            Collider[] colliders =
                _colliders.Where(c => layerMask == (layerMask | (1 << c.gameObject.layer)) && c.gameObject.activeInHierarchy).ToArray();
            return colliders.Select(collider => collider.GetComponent<T>()).ToArray();
        }

        private void OnTriggerEnter(Collider other)
        {
            if ( ! _colliders.Contains(other))
                _colliders.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_colliders.Contains(other))
                _colliders.Remove(other);
        }
    }
}
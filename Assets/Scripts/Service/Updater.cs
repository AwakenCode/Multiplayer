using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Service
{
    public class Updater : MonoBehaviour, IService
    {
        private readonly List<IUpdateable> _updateables = new();

        private void Update()
        {
            foreach (var updateable in _updateables)
                updateable.Update(Time.deltaTime);
        }

        public void AddListeners(params IUpdateable[] updateables)
        {
            foreach (var updateable in updateables)
                _updateables.Add(updateable);
        }

        public void RemoveListener(IUpdateable updateable) => _updateables.Remove(updateable);
    }
}
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Players
{
    public class KaneBehaviour : MonoBehaviour, IHittable
    {
        [SerializeField]
        private PlayerCore core;

        void IHittable.Hit()
        {
            core.Die();
        }

        public class Factory : Factory<KaneBehaviour>
        {
        }
    }
}

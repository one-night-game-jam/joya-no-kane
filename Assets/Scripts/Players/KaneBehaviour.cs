using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Players
{
    public class KaneBehaviour : MonoBehaviour, IHittable
    {
        private ISubject<bool> isDeadSubject = new BehaviorSubject<bool>(false);

        public IObservable<bool> IsDeadAsObservable()
        {
            return isDeadSubject;
        }

        void IHittable.Hit()
        {
            isDeadSubject.OnNext(true);
        }

        public class Factory : Factory<KaneBehaviour>
        {
        }
    }
}

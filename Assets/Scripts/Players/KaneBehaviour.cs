using System;
using UniRx;
using UnityEngine;

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
    }
}

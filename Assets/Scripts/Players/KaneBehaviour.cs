using System;
using UniRx;
using UnityEngine;

namespace Players
{
    public class KaneBehaviour : MonoBehaviour, IHittable
    {
        public IObservable<bool> IsDeadAsObservable()
        {
            return Observable.Return(true);
        }

        void IHittable.Hit()
        {
            Debug.Log("ゴ～ン");
        }
    }
}

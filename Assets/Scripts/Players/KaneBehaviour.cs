using System;
using UniRx;
using UnityEngine;

namespace Players
{
    public class KaneBehaviour : MonoBehaviour
    {
        public IObservable<bool> IsDeadAsObservable()
        {
            return Observable.Return(true);
        }
    }
}

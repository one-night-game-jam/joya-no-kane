using Players.InputImpls;
using System;
using UniRx;
using UnityEngine;

namespace Players
{
    public class PlayerCore : MonoBehaviour
    {
        private ISubject<bool> isDeadSubject = new Subject<bool>();

        public IObservable<bool> IsDeadAsObservable()
        {
            return isDeadSubject;
        }

        public void Die()
        {
            isDeadSubject.OnNext(true);
        }

        IInputEventProvider input;

        void Awake()
        {
            input = new MergedInputEventProvider(GetComponents<IInputEventProvider>());
        }

        public IObservable<Vector3> MoveDirectionAsObservable()
        {
            return input.MoveDirectionAsObservable();
        }
    }
}

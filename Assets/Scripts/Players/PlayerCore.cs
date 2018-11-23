﻿using System;
using UniRx;
using UnityEngine;

namespace Players
{
    public class PlayerCore : MonoBehaviour
    {
        private ISubject<bool> isDeadSubject = new BehaviorSubject<bool>(false);

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
            input = GetComponent<IInputEventProvider>();
        }

        public IObservable<Vector3> MoveDirectionAsObservable()
        {
            return input.MoveDirectionAsObservable()
                .TakeUntil(isDeadSubject.Skip(1))
                .Select(x => x.normalized);
        }
    }
}

using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Players.InputImpls
{
    class MergedInputEventProvider : IInputEventProvider
    {
        IObservable<Vector3> moveDirection;

        public MergedInputEventProvider(params IInputEventProvider[] inputEventProviders)
        {
            moveDirection = inputEventProviders.Select(x => x.MoveDirectionAsObservable()).Merge();
        }

        public IObservable<Vector3> MoveDirectionAsObservable()
        {
            return moveDirection;
        }
    }

}

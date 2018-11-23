using System;
using UniRx;
using UnityEngine;

namespace Players
{
    public class PlayerCore : MonoBehaviour
    {
        IInputEventProvider input;

        void Awake()
        {
            input = GetComponent<IInputEventProvider>();
        }

        public IObservable<Vector3> MoveDirectionAsObservable()
        {
            return input.MoveDirectionAsObservable()
                .Select(x => x.normalized);
        }
    }
}

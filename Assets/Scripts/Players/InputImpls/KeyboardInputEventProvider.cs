using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Players.InputImpls
{

    class KeyboardInputEventProvider : MonoBehaviour, IInputEventProvider
    {
        public IObservable<Vector3> MoveDirectionAsObservable()
        {
            return this.ObserveEveryValueChanged(_ => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        }
    }

}

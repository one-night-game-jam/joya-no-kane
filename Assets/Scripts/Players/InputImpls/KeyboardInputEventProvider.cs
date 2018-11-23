using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Players.InputImpls
{

    class KeyboardInputEventProvider : MonoBehaviour
    {
        public IObservable<Vector2> MoveDirectionAsObservable()
        {
            return this.ObserveEveryValueChanged(_ => new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")));
        }
    }

}

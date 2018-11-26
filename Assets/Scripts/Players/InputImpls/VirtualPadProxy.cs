using System;
using UnityEngine;
using Zenject;

using UI;

namespace Players.InputImpls
{
    class VirtualPadProxy : MonoBehaviour, IInputEventProvider
    {
        [Inject] private VirtualPad _virtualPad;

        IObservable<Vector3> IInputEventProvider.MoveDirectionAsObservable()
        {
            return _virtualPad.MoveDirectionAsObservable();
        }
    }

}

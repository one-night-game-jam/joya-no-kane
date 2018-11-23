using System;
using UnityEngine;

namespace Players
{

    interface IInputEventProvider
    {
        IObservable<Vector3> MoveDirectionAsObservable();
    }

}

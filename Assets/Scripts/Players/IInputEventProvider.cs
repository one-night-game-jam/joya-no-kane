using System;
using UnityEngine;

namespace Players
{

    interface IInputEventProvider
    {
        IObservable<Vector2> MoveDirectionAsObservable();
    }

}

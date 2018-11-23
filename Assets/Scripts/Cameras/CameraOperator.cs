using Players;
using UniRx;
using UnityEngine;
using Zenject;

namespace Cameras
{
    public class CameraOperator : MonoBehaviour
    {
        [Inject]
        ShumokuBehaviour shumoku;

        void Start()
        {
            var y = transform.position.y;
            var diff = transform.position - shumoku.transform.position;
            this.ObserveEveryValueChanged(_ => shumoku.transform.position, FrameCountType.EndOfFrame)
                .TakeUntilDestroy(shumoku)
                .Select(p => p + diff)
                .Select(p =>
                {
                    p.y = y;
                    return p;
                })
                .Subscribe(p => transform.position = p)
                .AddTo(this);
        }
    }
}
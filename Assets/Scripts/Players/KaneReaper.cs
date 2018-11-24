using System;
using UniRx;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(PlayerCore))]
    public class KaneReaper : MonoBehaviour
    {
        [SerializeField] PlayerCore core;
        [SerializeField] float destroyWait;

        private void Start()
        {
            core.IsDeadAsObservable()
                .Where(x => x)
                .Subscribe(_ =>
                {
                    Destroy(gameObject, destroyWait);
                })
                .AddTo(this);
        }
    }
}

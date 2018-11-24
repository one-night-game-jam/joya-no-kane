using System;
using UniRx;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(PlayerCore))]
    public class KaneDeadEffectEmitter : MonoBehaviour
    {
        [SerializeField] PlayerCore core;
        [SerializeField] ParticleSystem deadEffect;

        private void Start()
        {
            deadEffect.Pause();
            core.IsDeadAsObservable()
                .Where(x => x)
                .Subscribe(_ =>
                {
                    deadEffect.Play();
                })
                .AddTo(this);
        }
    }
}

using System;
using UniRx;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(KaneBehaviour))]
    public class KaneDeadEffectEmitter : MonoBehaviour
    {
        [SerializeField] KaneBehaviour kane;
        [SerializeField] ParticleSystem deadEffect;

        private void Start()
        {
            deadEffect.Pause();
            kane.IsDeadAsObservable()
                .Where(x => x)
                .Subscribe(_ =>
                {
                    deadEffect.Play();
                })
                .AddTo(this);
        }
    }
}

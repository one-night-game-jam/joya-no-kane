using System;
using UniRx;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(KaneBehaviour))]
    public class KaneReaper : MonoBehaviour
    {
        [SerializeField] KaneBehaviour kane;
        [SerializeField] float destroyWait;

        private void Start()
        {
            var kaneDead = kane.IsDeadAsObservable()
                .Where(x => x)
                .Subscribe(_ =>
                {
                    Destroy(gameObject, destroyWait);
                })
                .AddTo(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Zenject;

using Players;

namespace GameManagers
{
    public class TempleBellPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [Inject]
        private void Init(List<KaneBehaviour> kanes)
        {
            kanes.Select(k => k.IsDeadAsObservable())
                .Merge()
                .Where(x => x)
                .Subscribe(_ =>
                {
                    audioSource.Play();
                })
                .AddTo(this);
        }
    }

}

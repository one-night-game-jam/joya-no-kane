using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

using Players;
using GameManagers;

namespace UI
{
    class PlayingScreen : MonoBehaviour
    {
        [Inject] KaneSpawner kaneSpawner;
        [Inject] KaneCounter kaneCounter;
        [Inject] ShumokuBehaviour shumoku;

        void Start()
        {
            gameObject.SetActive(false);

            kaneSpawner.Spawned
                .SelectMany(x => x.Select(k => k.IsDeadAsObservable()).Merge())
                .Where(x => x)
                .First()
                .Subscribe(_ => gameObject.SetActive(true))
                .AddTo(this);

            kaneCounter.KaneCountAsObseravble()
                .Where(x => x == 0)
                .First()
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);

            shumoku.life
                .Where(x => x == 0)
                .First()
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);
        }
    }

}

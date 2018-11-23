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

            kaneSpawner.KaneDeadFirst
                .Subscribe(_ => gameObject.SetActive(true))
                .AddTo(this);

            kaneCounter.NoRemainingKane
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);

            shumoku.Dead
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);
        }
    }

}

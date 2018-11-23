using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

using Players;

namespace UI
{
    class StartScreen : MonoBehaviour
    {
        [Inject]
        KaneSpawner kaneSpawner;

        void Start()
        {
            kaneSpawner.KaneDeadFirst
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);
        }
    }

}

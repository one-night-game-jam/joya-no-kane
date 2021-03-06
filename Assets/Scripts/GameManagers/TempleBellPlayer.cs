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
        KaneSpawner kaneSpawner;

        void Start()
        {
            kaneSpawner.KaneDead
                .Subscribe(_ => audioSource.Play())
                .AddTo(this);
        }
    }

}

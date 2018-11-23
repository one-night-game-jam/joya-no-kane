using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

using Players;

namespace GameManagers
{
    public class KaneCounter : MonoBehaviour
    {
        public int initialKaneCount => _initialKaneCount;
        private int _initialKaneCount;

        public IObservable<int> KaneCountAsObseravble() { return kaneCount; }
        readonly ISubject<int> kaneCount = new ReplaySubject<int>();

        public IObservable<Unit> NoRemainingKane => KaneCountAsObseravble()
                .Where(x => x == 0)
                .First()
                .AsUnitObservable();

        [Inject]
        KaneSpawner kaneSpawner;

        private void Start()
        {
            kaneSpawner.Spawned
                .Subscribe(kanes =>
                {
                    _initialKaneCount = kanes.Count();
                })
                .AddTo(this);

            kaneSpawner.Spawned
                .SelectMany(kanes =>
                {
                    return this
                        .UpdateAsObservable()
                        .Select(_ => kanes.Where(x => x != null).Count());
                })
                .Subscribe(kaneCount.OnNext)
                .AddTo(this);
        }
    }
}

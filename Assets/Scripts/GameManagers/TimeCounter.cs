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
    public class TimeCounter : MonoBehaviour
    {
        public IObservable<float> TimeCountAsObseravble() { return timeCount; }
        readonly ISubject<float> timeCount = new ReplaySubject<float>();

        [Inject]
        KaneSpawner kaneSpawner;

        void Start()
        {
            kaneSpawner.Spawned
                .SelectMany(x => x.Select(k => k.IsDeadAsObservable()).Merge())
                .Where(x => x)
                .First()
                .Select(_ => Time.time)
                .SelectMany(beginTime =>
                {
                    return this
                        .UpdateAsObservable()
                        .Select(_ => Time.time - beginTime);
                })
                .Subscribe(timeCount.OnNext)
                .AddTo(this);
        }
    }

}

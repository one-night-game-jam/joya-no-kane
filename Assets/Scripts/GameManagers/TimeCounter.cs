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

        [Inject] KaneSpawner kaneSpawner;
        [Inject] KaneCounter kaneCounter;
        [Inject] ShumokuBehaviour shumoku;

        void Start()
        {
            var clear = kaneCounter
                .KaneCountAsObseravble()
                .Where(x => x == 0)
                .First();

            kaneSpawner.KaneDeadFirst
                .Select(_ => Time.time)
                .SelectMany(beginTime =>
                {
                    return this
                        .UpdateAsObservable()
                        .TakeUntil(clear)
                        .Select(_ => Time.time - beginTime);
                })
                .Subscribe(timeCount.OnNext)
                .AddTo(this);
        }
    }

}

using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Players
{
    public class KaneSpawner : MonoBehaviour
    {
        const int NumberOfBonno = 108;

        [Inject] KaneBehaviour.Factory _factory;

        readonly ISubject<KaneBehaviour[]> _spawned = new AsyncSubject<KaneBehaviour[]>();
        public IObservable<KaneBehaviour[]> Spawned => _spawned;

        void Awake()
        {
            _spawned.OnNext(Enumerable.Range(0, NumberOfBonno).Select(_ => Create()).ToArray());
            _spawned.OnCompleted();
        }

        KaneBehaviour Create()
        {
            var kane = _factory.Create();

            return kane;
        }
    }
}

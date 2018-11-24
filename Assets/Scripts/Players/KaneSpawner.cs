using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Players
{
    public class KaneSpawner : MonoBehaviour
    {
        const int NumberOfBonno = 108;

        [SerializeField] float _spawnAreaRadius;

        [Inject] KaneBehaviour.Factory _factory;

        readonly ISubject<KaneBehaviour[]> _spawned = new AsyncSubject<KaneBehaviour[]>();
        public IObservable<KaneBehaviour[]> Spawned => _spawned;

        public IObservable<bool> KaneDead => Spawned
                .SelectMany(x => x.Select(k => k.GetComponent<PlayerCore>().IsDeadAsObservable()).Merge())
                .Where(x => x);

        public IObservable<Unit> KaneDeadFirst => KaneDead
                .First()
                .AsUnitObservable();

        void Awake()
        {
            _spawned.OnNext(Enumerable.Range(0, NumberOfBonno).Select(_ => Create()).ToArray());
            _spawned.OnCompleted();
        }

        KaneBehaviour Create()
        {
            var kane = _factory.Create();
            var randomPos = Random.insideUnitCircle * _spawnAreaRadius;
            var pos = new Vector3(randomPos.x, 0, randomPos.y);

            // 中央を避ける
            if (pos.sqrMagnitude < 100)
            {
                pos *= 10;
            }
            var randomRotation = Random.rotation;
            kane.transform.SetPositionAndRotation(pos, Quaternion.Euler(0, randomRotation.y, 0));
            return kane;
        }
    }
}

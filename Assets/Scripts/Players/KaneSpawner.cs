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

        public IObservable<Unit> KaneDeadFirst => Spawned
                .SelectMany(x => x.Select(k => k.IsDeadAsObservable()).Merge())
                .Where(x => x)
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
            var randomRotation = Random.rotation;
            kane.transform.SetPositionAndRotation(new Vector3(randomPos.x, 0, randomPos.y), Quaternion.Euler(0, randomRotation.y, 0));
            return kane;
        }
    }
}

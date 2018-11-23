using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Players
{
    public class ShumokuBehaviour : MonoBehaviour
    {
        [SerializeField]
        private PlayerCore core;

        [SerializeField] float lifeReduction;
        private FloatReactiveProperty _life = new FloatReactiveProperty(1.0F);
        public IObservable<float> life => _life;

        public IObservable<Unit> Dead => core
            .IsDeadAsObservable()
            .Where(x => x)
            .First()
            .AsUnitObservable();

        [Inject]
        KaneSpawner kaneSpawner;

        void Start()
        {
            kaneSpawner.KaneDeadFirst
                .SelectMany(_ => this.UpdateAsObservable())
                .Subscribe(_ => { UpdateLife(); })
                .AddTo(this);

            life.Where(x => x == 0)
                .Subscribe(_ => core.Die());
        }

        void OnTriggerEnter(Collider other)
        {
            var hittable = other.GetComponent<IHittable>();

            if (hittable == null)
            {
                return;
            }
            hittable.Hit();

            MaximizeLife();
        }

        void MaximizeLife()
        {
            _life.Value = 1.0F;
        }

        void UpdateLife()
        {
            _life.Value -= lifeReduction * Time.deltaTime;
            if (_life.Value < 0)
            {
                _life.Value = 0;
            }
        }
    }
}

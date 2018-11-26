using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI
{

    class VirtualPad : UIBehaviour
    {
        [SerializeField] private Image _knob;
        [SerializeField] private float _knobClearance;

        private Subject<Vector3> _moveDirection = new Subject<Vector3>();
        public IObservable<Vector3> MoveDirectionAsObservable() => _moveDirection;

        protected override void Start()
        {
            _knob.gameObject.SetActive(false);

            this.OnBeginDragAsObservable()
                .Subscribe(_ => _knob.gameObject.SetActive(true))
                .AddTo(this);

            this.OnEndDragAsObservable()
                .Subscribe(_ => _knob.gameObject.SetActive(false))
                .AddTo(this);

            var resetMovement = this
                .OnEndDragAsObservable()
                .Select(e => Vector3.zero);

            this.OnDragAsObservable()
                .Select(e =>
                {
                    var delta = e.position - e.pressPosition;
                    delta /= _knobClearance;
                    return new Vector3(delta.x, 0, delta.y);
                })
                .Merge(resetMovement)
                .Subscribe(v => _moveDirection.OnNext(v))
                .AddTo(this);

            var knobOrigin = this
                .OnBeginDragAsObservable()
                .Select(e =>
                {
                    var p = e.position;
                    return new Vector3(p.x, p.y, 0);
                });

            _moveDirection
                .Select(v =>
                {
                    v.Set(v.x, v.z, 0);
                    if (v.sqrMagnitude > 1.0F)
                    {
                        v.Normalize();
                    }
                    v *= _knobClearance;
                    return v;
                })
                .WithLatestFrom(knobOrigin, (position, downPosition) => downPosition + position)
                .Subscribe(v => _knob.rectTransform.position = v)
                .AddTo(this);
        }
    }

}

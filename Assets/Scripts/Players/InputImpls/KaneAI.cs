using System;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Players.InputImpls
{
    public class KaneAI : MonoBehaviour, IInputEventProvider
    {
        [Inject]
        ShumokuBehaviour _shumoku;

        readonly ISubject<Vector3> _moveDirection = new Subject<Vector3>();

        float _switchDirectionTime;
        Vector3 _lastPosition;

        void Update()
        {
            _switchDirectionTime -= Time.deltaTime;

            var lastPosition = _lastPosition;
            _lastPosition = transform.position;

            var diff = _shumoku.transform.position - transform.position;
            if (diff.sqrMagnitude < 60f && Vector3.Dot(diff, transform.forward) > 0.5f)
            {
                OnNext(-transform.forward);
                _switchDirectionTime = Random.Range(2f, 4f);
                return;
            }

            if (_switchDirectionTime < 0)
            {
                OnNext(Random.insideUnitSphere);
                _switchDirectionTime = Random.Range(0f, 3f);
            }

            if ((transform.position - lastPosition).sqrMagnitude / Time.deltaTime < 0.2f)
            {
                OnNext(-transform.forward);
                _switchDirectionTime = Random.Range(2f, 3f);
            }

        }

        IObservable<Vector3> IInputEventProvider.MoveDirectionAsObservable()
        {
            return _moveDirection;
        }

        void OnNext(float x, float y)
        {
            OnNext(new Vector3(x, 0, y));
        }

        void OnNext(Vector3 v)
        {
            _moveDirection.OnNext(v);
        }
    }
}

using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace Players
{
    [RequireComponent(typeof(PlayerCore))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField]
        PlayerCore _core;
        [SerializeField]
        NavMeshAgent _agent;

        [SerializeField]
        float _speed;

        void Start()
        {
            _agent.updateRotation = false;

            this.UpdateAsObservable()
                .WithLatestFrom(_core.MoveDirectionAsObservable(), (_, v) => v)
                .TakeUntil(_core.IsDeadAsObservable().Where(x => x))
                .Subscribe(Move)
                .AddTo(this);
        }

        void Move(Vector3 v)
        {
            v.y = 0;
            v = v.normalized;
            _agent.Move(v * _speed * Time.deltaTime);

            if (v.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(v);
            }
        }
    }
}

using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

using GameManagers;
using Players;

namespace UI
{
    class GameOverScreen : MonoBehaviour
    {
        [Inject] ShumokuBehaviour shumoku;
        [Inject] KaneCounter kaneCounter;


        void Start()
        {
            gameObject.SetActive(false);

            shumoku.Dead
                .TakeUntil(kaneCounter.NoRemainingKane)
                .Subscribe(_ => gameObject.SetActive(true))
                .AddTo(this);
        }
    }

}

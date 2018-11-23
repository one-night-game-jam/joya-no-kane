using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

using Players;

namespace UI
{
    class GameOverScreen : MonoBehaviour
    {
        [Inject] ShumokuBehaviour shumoku;

        void Start()
        {
            gameObject.SetActive(false);

            shumoku.life
                .Where(x => x == 0)
                .First()
                .Subscribe(_ => gameObject.SetActive(true))
                .AddTo(this);
        }
    }

}

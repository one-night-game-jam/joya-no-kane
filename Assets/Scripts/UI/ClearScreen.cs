using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

using GameManagers;

namespace UI
{
    class ClearScreen : MonoBehaviour
    {
        [Inject] KaneCounter kaneCounter;

        void Start()
        {
            gameObject.SetActive(false);

            kaneCounter.KaneCountAsObseravble()
                .Where(x => x == 0)
                .First()
                .Subscribe(_ => gameObject.SetActive(true))
                .AddTo(this);
        }
    }

}

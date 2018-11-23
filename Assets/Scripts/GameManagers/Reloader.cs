using System;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

using Players;

namespace GameManagers
{
    public class Reloader : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        [SerializeField] private float waitToGetInput;

        [Inject] private ShumokuBehaviour shumoku;
        [Inject] private KaneCounter kaneCounter;

        private async UniTask Start()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(waitToGetInput));

            var finished = Observable.Merge(shumoku.Dead, kaneCounter.NoRemainingKane);

            GetComponent<IInputEventProvider>()
                .MoveDirectionAsObservable()
                .SkipUntil(finished)
                .Subscribe(_ => SceneManager.LoadScene(sceneName))
                .AddTo(this);
        }
    }
}

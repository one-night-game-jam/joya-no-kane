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

         void Start()
        {
            Observable.Merge(shumoku.Dead, kaneCounter.NoRemainingKane)
                .SelectMany(Observable.Timer(TimeSpan.FromSeconds(waitToGetInput)))
                .Select(_ => GetComponent<IInputEventProvider>().MoveDirectionAsObservable())
                .Switch()
                .Skip(1)
                .Subscribe(_ => SceneManager.LoadScene(sceneName))
                .AddTo(this);
        }
    }
}

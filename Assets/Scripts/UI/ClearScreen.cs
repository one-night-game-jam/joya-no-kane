using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

using GameManagers;

namespace UI
{
    class ClearScreen : MonoBehaviour
    {
        [Inject] private TimeCounter timeCounter;

        [SerializeField] private TMP_Text timeCountText;
        [SerializeField] private string timeCountTextFormat;

        private void Start()
        {
            timeCounter
                .TimeCountAsObseravble()
                .Subscribe(ShowTimeCount)
                .AddTo(this);
        }

        private void ShowTimeCount(float timeCount)
        {
            timeCountText.text = timeCount.ToString(timeCountTextFormat);
        }
    }

}

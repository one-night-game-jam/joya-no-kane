using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

using GameManagers;

namespace UI
{
    class KaneCount : MonoBehaviour
    {
        [Inject] private KaneCounter kaneCounter;

        [SerializeField] private TMP_Text kaneCountText;

        private void Start()
        {
            kaneCounter
                .KaneCountAsObseravble()
                .Subscribe(ShowKaneCount)
                .AddTo(this);
        }

        private void ShowKaneCount(int kaneCount)
        {
            var remaining = kaneCounter.initialKaneCount - kaneCount;
            var all = kaneCounter.initialKaneCount;
            kaneCountText.text = $"{remaining}/{all}";
        }
    }

}

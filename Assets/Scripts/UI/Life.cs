using UniRx;
using UnityEngine;
using Zenject;

using Players;

namespace UI
{
    class Life : MonoBehaviour
    {
        [Inject] private ShumokuBehaviour shumoku;

        [SerializeField] private GameObject lifeBar;

        private void Start()
        {
            shumoku.life
                .Subscribe(UpdateLifeBarLength)
                .AddTo(this);
        }

        private void UpdateLifeBarLength(float life)
        {
            var scale = lifeBar.transform.localScale;
            scale.x = life;
            lifeBar.transform.localScale = scale;
        }
    }

}

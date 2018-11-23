using UnityEngine;

namespace Players
{
    public class KaneBehaviour : MonoBehaviour, IHittable
    {
        void IHittable.Hit()
        {
            Debug.Log("ゴ～ン");
        }
    }
}

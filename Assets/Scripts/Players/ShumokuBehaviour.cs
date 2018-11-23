using UnityEngine;

namespace Players
{
    public class ShumokuBehaviour : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            var hittable = other.GetComponent<IHittable>();
            hittable?.Hit();
        }
    }
}

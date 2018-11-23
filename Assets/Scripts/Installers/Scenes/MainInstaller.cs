using Players;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller<MainInstaller>
{
    [SerializeField] KaneBehaviour _kanePrefab;

    public override void InstallBindings()
    {
        Container.BindFactory<KaneBehaviour, KaneBehaviour.Factory>()
            .FromComponentInNewPrefab(_kanePrefab)
            .WithGameObjectName("Kane")
            .UnderTransformGroup("Kanes");
    }
}
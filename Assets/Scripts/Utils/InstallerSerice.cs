using UnityEngine;

namespace TowerDefender.Assets.Scripts.Utils
{
    public class InstallerSerice : MonoBehaviour{
        [SerializeField] private Game game;
        private void Awake() {
            ServiceLocator.Instance.RegisterService<IGame>(game);
        }

        private void OnDestroy() {
            ServiceLocator.Instance.RemoveService(typeof(IGame));
        }
    }
}
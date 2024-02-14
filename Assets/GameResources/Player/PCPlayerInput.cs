namespace TestTask
{
    using UnityEngine;

    /// <summary>
    /// Инпут с ПК
    /// </summary>
    public class PCPlayerInput : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController = default;

        private Vector2 _direction = default;

        private void Update()
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.y = Input.GetAxis("Vertical");
        }

        private void FixedUpdate() => _playerController.MovePlayer(_direction);
    }
}

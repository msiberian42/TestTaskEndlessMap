namespace TestTask
{
    using UnityEngine;

    /// <summary>
    /// Контроллер игрока
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Min(1f)]
        private float speed = 1f;

        private Rigidbody2D rb = default;

        private void Awake() => rb = GetComponent<Rigidbody2D>();
        
        /// <summary>
        /// Передвигает игрока
        /// </summary>
        /// <param name="direction"></param>
        public void MovePlayer(Vector2 direction) => 
            rb.velocity = direction * speed * Time.fixedDeltaTime;
    }
}

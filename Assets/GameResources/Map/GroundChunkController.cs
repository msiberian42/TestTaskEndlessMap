namespace TestTask
{
    using UnityEngine;

    /// <summary>
    /// Контроллер одного участка карты
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class GroundChunkController : MonoBehaviour
    {
        /// <summary>
        /// Количество игроков на данном участке
        /// </summary>
        public int PlayersCount => _playersCount;

        private Collider2D _coll = default;
        private GroundSpawner _spawner = default;

        private PlayerController _playerController = default;
        private int _playersCount = 0;

        private void Awake()
        {
            _coll = GetComponent<Collider2D>();
            _coll.isTrigger = true;

            _spawner = FindAnyObjectByType<GroundSpawner>();
        }

        private void OnEnable() => _spawner.AddChunk(this);

        private void OnDisable() => _spawner.RemoveChunk(this);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _playerController = collision.GetComponent<PlayerController>();

            if (_playerController)
            {
                _playersCount++;
                _spawner.AddMissingChuncks(this);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _playerController = collision.GetComponent<PlayerController>();

            if (_playerController)
            {
                _playersCount--;
                _spawner.DeleteExcessChuncks(this);
            }
        }
    }
}

namespace TestTask
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class GroundSpawner : MonoBehaviour
    {
        [SerializeField]
        private GroundChunksPool _chunksPool = default;

        private List<GroundChunkController> _allChunks = new List<GroundChunkController>();

        private float _scaleX = 0f;
        private float _scaleY = 0f;
        private float _posX = 0f;
        private float _posY = 0f;
        private Vector2 _currentChunkCoordinates = default;
        private GroundChunkController _chunk = default;

        private List<GroundChunkController> _neighboringChunks = new List<GroundChunkController>();     
        private List<GroundChunkController> _chunksToDelete = new List<GroundChunkController>();

        /// <summary>
        /// Добавляет чанк в общий список чанков
        /// </summary>
        /// <param name="ground"></param>
        public virtual void AddChunk(GroundChunkController ground) => _allChunks.Add(ground);

        /// <summary>
        /// Удаляет чанк из общего списка чанков
        /// </summary>
        /// <param name="ground"></param>
        public virtual void RemoveChunk(GroundChunkController ground) => _allChunks.Remove(ground);

        /// <summary>
        /// Добавляет недостающие чанки на пути игрока
        /// </summary>
        /// <param name="ground"></param>
        public void AddMissingChuncks(GroundChunkController ground)
        {
            _scaleX = ground.transform.lossyScale.x;
            _scaleY = ground.transform.lossyScale.y;

            _posX = ground.transform.position.x;
            _posY = ground.transform.position.y;

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0) continue;

                    _currentChunkCoordinates.x = _posX + x * _scaleX;
                    _currentChunkCoordinates.y = _posY + y * _scaleY;

                    if (!_allChunks.Any(c => (Vector2)c.transform.position == _currentChunkCoordinates))
                    {
                        _chunk = _chunksPool.GetObject();
                        _chunk.transform.position = _currentChunkCoordinates;
                    }
                }
            }
        }

        /// <summary>
        /// Убирает лишние чанки
        /// </summary>
        /// <param name="ground"></param>
        public void DeleteExcessChuncks(GroundChunkController ground)
        {
            _chunksToDelete.Clear();

            foreach (var chunk in _allChunks)
            {
                FindNeighboringChunks(chunk);

                if (!_neighboringChunks.Any(c => c.PlayersCount > 0))
                {
                    _chunksToDelete.Add(chunk);
                }
            }

            foreach (var chunk in _chunksToDelete)
            {
                if (chunk.PlayersCount <= 0)
                    _chunksPool.ReleaseObject(chunk);
            }
        }

        private void FindNeighboringChunks(GroundChunkController ground)
        {
            _scaleX = ground.transform.lossyScale.x;
            _scaleY = ground.transform.lossyScale.y;

            _posX = ground.transform.position.x;
            _posY = ground.transform.position.y;

            _neighboringChunks.Clear();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0) continue;

                    _currentChunkCoordinates.x = _posX + x * _scaleX;
                    _currentChunkCoordinates.y = _posY + y * _scaleY;

                    if (_allChunks.Any(c => (Vector2)c.transform.position == _currentChunkCoordinates))
                    {
                        _neighboringChunks.Add(_allChunks.FirstOrDefault(
                            c => (Vector2)c.transform.position == _currentChunkCoordinates));
                    }
                }
            }
        }
    }
}
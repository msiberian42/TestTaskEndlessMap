namespace TestTask
{
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// Пул участков земли
    /// </summary>
    public class GroundChunksPool : MonoBehaviour
    {
        [SerializeField]
        private Transform _parent = default;

        [SerializeField]
        private GroundChunkController _prefab = default;

        private ObjectPool<GroundChunkController> _pool = default;

        private void Awake()
        {
            _pool = new ObjectPool<GroundChunkController>(
                createFunc: () => Instantiate(_prefab, _parent),
                actionOnGet: (obj) => obj.gameObject.SetActive(true),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 100);
        }

        /// <summary>
        /// Возвращает чанк из пула
        /// </summary>
        public GroundChunkController GetObject() => _pool.Get();

        /// <summary>
        /// Возвращает чанк в пул
        /// </summary>
        public void ReleaseObject(GroundChunkController proj) => _pool.Release(proj);
    }
}

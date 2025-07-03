using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private Collectable _collectable;
    [SerializeField] private List<Transform> _places;
    [SerializeField] private int _count;
    private CollectableSound _sound;

    private List<Collectable> _spawned;

    public int Count => _count;
    public CollectableSound Sounds => _sound;

    private ObjectPool<Collectable> _pool;
    private int _poolCapacity = 4;
    private int _poolMaxSize = 4;

    private void Awake()
    {
        _pool = new ObjectPool<Collectable>(
            createFunc: () => Instantiate(_collectable, transform),
            actionOnGet: (collectable) => collectable.gameObject.SetActive(true),
            actionOnRelease: (collectable) => collectable.gameObject.SetActive(false),
            actionOnDestroy: (collectable) => Destroy(collectable.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );

        _spawned = new List<Collectable>();

        _sound = GetComponent<CollectableSound>();
    }

    public void Spawn()
    {
        if (_count > _places.Count)
        {
            Debug.LogError("You need more places or less collectables.");
        }
        else if (_count == _places.Count)
        {
            foreach (Transform place in _places)
            {
                Collectable collectable = _pool.Get();
                collectable.transform.position = place.position;
                _spawned.Add(collectable);
            }
        }
        else
        {
            for (int i = 0; i < _count; i++)
            {
                int randomPlace = Random.Range(0, _places.Count);

                Collectable collectable = _pool.Get();
                collectable.transform.position = _places[randomPlace].position;
                _spawned.Add(collectable);

                _places.RemoveAt(randomPlace);
            }
        }
    }

    public void Despawn()
    {
        if (_spawned.Count > 0)
        {
            foreach (Collectable collectable in _spawned)
                _pool.Release(collectable);
        }

        _spawned.Clear();
    }

    public void SpawnReward(Vector2 position)
    {
        Collectable collectable = _pool.Get();
        collectable.transform.position = position;
        _spawned.Add(collectable);
    }
}

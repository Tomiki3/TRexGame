using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefabs;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;
    public float coeff = 0.01f;

    public static float minSpawnRate = 1f;
    public static float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefabs);
                obstacle.transform.position += transform.position;
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        float newMinSpawnRate = 1f - coeff * GameManager.instance.gameSpeed;
        float newMaxSpawnRate = 2f - coeff * GameManager.instance.gameSpeed;

        minSpawnRate = newMinSpawnRate < 0.3f ? 0.3f : newMinSpawnRate;
        maxSpawnRate = newMaxSpawnRate < 0.6f ? 0.6f : newMaxSpawnRate;

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}

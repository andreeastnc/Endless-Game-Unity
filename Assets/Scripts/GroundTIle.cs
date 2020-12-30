using UnityEngine;

public class GroundTIle : MonoBehaviour
{

    GroundSpawner groundSpawner;
    // Initializat cu null ca sa nu mai primim warning in unity
    [SerializeField] GameObject coinPrefab = null;
    [SerializeField] GameObject obstaclePrefab = null;
    [SerializeField] GameObject powerUpPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }


    public void SpawnObstacle()
    {
        // Choose a random point to spawn the obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // y reprezinta intaltimea obstacolului
        float y;
        if (obstacleSpawnIndex % 2 == 0)
        {
            y = 3;
        }
        else
        {
            y = 1;
        }

        // Vector care contine x, z ale obstacolului si y pe care l am ales mai sus
        Vector3 obstacleScale = new Vector3(obstaclePrefab.transform.localScale.x, y, obstaclePrefab.transform.localScale.z);
        // Ar fi o idee sa nu modificam prefab ul, ci sa memoram game objectul dupa instantiere si sa modificam atunci
        obstaclePrefab.transform.localScale = obstacleScale;

        // Spawn the obstacle at the position
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);

        // N am stiut exact unde sa pun asta dar aici se spawneaza cu o frecventa decenta :))
        int score = GameManager.inst.GetScore();
        if (score == 0) { return; }
        if (score % 5 == 0)
        {
            SpawnPowerUp();
        }
    }


    public void SpawnCoins()
    {
        int coinsToSpawn = 10;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    public void SpawnPowerUp()
    {
        // Deoarece script ul ruleaza de mai multe ori la momentul in care scorul ajunge la 
        // valoarea pentru a spawna un powerups, este nevoie sa verificam ca nu exista altul
        var numberOfPowerUpsActive = GameObject.FindGameObjectsWithTag("PowerUp").Length;
        if (numberOfPowerUpsActive != 0)
        {
            return;
        }

        GameObject temp = Instantiate(powerUpPrefab, transform);
        // Mutam power up ul mai in fata cu 50 de unitati ca sa fie in fata player ului
        var transformPosition = GetRandomPointInCollider(GetComponent<Collider>());
        temp.transform.position = transformPosition;
    }

    Vector3 GetRandomPointInCollider (Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}

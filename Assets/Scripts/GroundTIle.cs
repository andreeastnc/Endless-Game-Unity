using UnityEngine;

public class GroundTIle : MonoBehaviour
{

    GroundSpawner groundSpawner;
    // Initializat cu null ca sa nu mai primim warning in unity
    [SerializeField] GameObject coinPrefab = null;
    [SerializeField] GameObject obstaclePrefab = null;

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
        obstaclePrefab.transform.localScale = obstacleScale;

        // Spawn the obstacle at the position
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
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
        GameObject temp = Instantiate(powerUpPrefab, transform);
        temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());

        int typeOfPowerUp = Random.Range(0, 2);
        switch (typeOfPowerUp)
        {
            case 0:
                temp.renderer.material.color = Color.blue;

                break;
            case 1:
                temp.renderer.material.color = Color.red;

                break;
        }

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

    void Update()
    {
        if (GameManager.inst.GetScore() % 25 == 0)
        {
            Debug.Log("12300");
            SpawnPowerUp();
        }
    }
}

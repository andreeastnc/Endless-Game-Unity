using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    [SerializeField] GameObject groundTile;
    Vector3 nextSpawnPoint;

    public void SpawnTile(bool spawnItems)
    {
        // This quaternion corresponds to "no rotation" - the object is perfectly aligned with the world or parent axes.
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        if (spawnItems)
        {
            temp.GetComponent<GroundTIle>().SpawnObstacle();
            temp.GetComponent<GroundTIle>().SpawnCoins();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            if (i < 2)
            {
                SpawnTile(false);
            } else
            {
                SpawnTile(true);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    // todo later whenever user place a tile spawn one on the plane

    [Tooltip("This number will be multiplied to num of players")]
    [SerializeField] int numOfItemsToSpawn;
    private int numOfPlayers = 4;

    [SerializeField] GameObject[] brickPrefabs;
    [SerializeField] GameObject brickSpawnPoint;

    public static List<GameObject> spawnedBrickList = new List<GameObject>();

    private GameObject brickSpawnArea;

    [SerializeField] bool isStartArea;
    [SerializeField] LayerMask layerMask;

    private void Start()
    {
         brickSpawnArea = gameObject.transform.GetChild(0).gameObject;

        if (isStartArea)
        {
            StartCoroutine(SpawnItemsAtStart(numOfItemsToSpawn, numOfPlayers));
        }
    }

    public IEnumerator SpawnItemsAtStart(int numItemsToSpawn, int numOfPlayers)
    {
        for (int j = 0; j < numOfPlayers; j++)
        {
            for (int i = 0; i < numItemsToSpawn; i++)
            {
                Vector3 targetPos = randomizeSpawnPoint();

                Collider[] colliders = Physics.OverlapSphere(targetPos, 1f, layerMask);

                while (colliders.Length != 0)
                {
                    targetPos = randomizeSpawnPoint();
                    colliders = Physics.OverlapSphere(targetPos, 1f, layerMask);
                }

                var brick = Instantiate(brickPrefabs[j], targetPos, Quaternion.Euler(0, 0, 0));
                brick.transform.parent = brickSpawnPoint.transform;
            }
        }

        yield return null;
    }

    public IEnumerator SpawnItemsAtWill(int numItemsToSpawn, int playerColorIndex)
    {
        for (int i = 0; i < numItemsToSpawn; i++)
        {
            Vector3 targetPos = randomizeSpawnPoint();

            Collider[] colliders = Physics.OverlapSphere(targetPos, 1f, layerMask);

            while (colliders.Length != 0)
            {
                targetPos = randomizeSpawnPoint();
                colliders = Physics.OverlapSphere(targetPos, 1f, layerMask);
            }

            var brick = Instantiate(brickPrefabs[playerColorIndex], targetPos, Quaternion.Euler(0, 0, 0));
            brick.transform.parent = brickSpawnPoint.transform;
        }

        yield break;
    }



    Vector3 randomizeSpawnPoint()
    {
        // Calculate Bounds
        var meshRenderer = brickSpawnArea.GetComponent<MeshRenderer>();
        Bounds meshBounds = meshRenderer.bounds;

        //Debug.Log(meshBounds.min.x + " " + meshBounds.max.x + " " + meshBounds.min.z + " " + meshBounds.max.z);

        return new Vector3(Random.Range(meshBounds.min.x, meshBounds.max.x), meshBounds.max.y + 0.164f, Random.Range(meshBounds.min.z, meshBounds.max.z));
    }
}

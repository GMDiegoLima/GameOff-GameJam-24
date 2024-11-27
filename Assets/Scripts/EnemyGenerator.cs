using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform generatePoint;
    [SerializeField] private float generateCD;
    private float generateCDTimer;

    private void Update()
    {
        generateCDTimer += Time.deltaTime;
        if (generateCDTimer >= generateCD)
        {
            GenerateRandomEnemy();
            generateCDTimer = 0;
		}
    }

    public void GenerateRandomEnemy()
    {
        int idx = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[idx], generatePoint.position, Quaternion.identity);
	}

}

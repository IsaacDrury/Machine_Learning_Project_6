using System.Data;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    private GameObject poisonPrefab;
    [SerializeField]
    private GameObject[] foods;
    [SerializeField]
    private int foodAmount;

    private void Awake()
    {
        foods = new GameObject[foodAmount];
    }

    public void SpawnFood()
    {
        foreach (GameObject food in foods) 
        { 
            Destroy(food);
        }
        for (int i = 0; i < foodAmount; i++)
        {
            float x = Random.Range(-9.0f, 9.0f);
            float z = Random.Range(-9.0f, 9.0f);
            GameObject food = Instantiate(foodPrefab);
            food.transform.localPosition = new Vector3(x, 0.5f, z);
            foods[i] = food;
        }
    }
}

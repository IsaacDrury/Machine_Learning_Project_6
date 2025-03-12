using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    private GameObject[] agents;
    [SerializeField]
    private GameObject[] foods;
    [SerializeField] 
    private GameObject[] walls;
    [SerializeField]
    private int foodAmount;

    public void SpawnFood()
    {
        foreach (GameObject food in foods) 
        { 
            Destroy(food);
        }
        for (int i = 0; i < foodAmount; i++)
        {
            bool goodPos = false;
            int x = Random.Range(-9, 10);
            int z = Random.Range(-9, 10);
            while (!goodPos)
            {
                goodPos = true;
                foreach (GameObject agent in agents)
                {
                    Vector3 pos = agent.transform.position;
                    if (x > pos.x - 1.0f && x < pos.x + 1.0f && z > pos.z - 1.0f && z < pos.z + 1.0f)
                    {
                        goodPos = false;
                        x = Random.Range(-9, 10);
                        z = Random.Range(-9, 10);
                        break;
                    }
                }
                if (goodPos)
                {
                    foreach (GameObject wall in walls)
                    {
                        Vector3 pos = wall.transform.position;
                        if (x > pos.x - 0.5f && x < pos.x + 0.5f && z > pos.z - 0.5f && z < pos.z + 0.5f)
                        {
                            goodPos = false;
                            x = Random.Range(-9, 10);
                            z = Random.Range(-9, 10);
                            break;
                        }
                    }
                }
            }
            GameObject food = Instantiate(foodPrefab);
            food.transform.localPosition = new Vector3(x, 0.5f, z);
            foods.Append(food);
        }
    }

    public GameObject[] GetFoods()
    {
        return foods;
    }
}

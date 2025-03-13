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
    private int maxFood;

    public void SpawnFood()
    {
        foreach (GameObject food in foods) 
        { 
            Destroy(food);
        }
        foods = new GameObject[maxFood];
        for (int i = 0; i < maxFood; i++)
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
            GameObject food = Instantiate(foodPrefab, this.gameObject.transform, false);
            food.transform.localPosition = new Vector3(x, 0.5f, z);
            foods[i] = food;
        }
    }

    public void SetFoodAmount(GameObject eatenFood)
    {
        foreach (GameObject food in foods) 
        {
            if (food == eatenFood)
            {
                GameObject[] temp = new GameObject[foods.Length - 1];
                int iter = 0;
                for (int i = 0; i < foods.Length; i ++)
                {
                    if (foods[i] != eatenFood)
                    {
                        temp[iter] = foods[i];
                        iter++;
                    }
                }
                foods = temp;
                Destroy(eatenFood);
            }
        }
    }

    public bool allFoodEaten()
    {
        if (foods.Length == 0) 
        {
            return true;
        }
        return false;
    }
}

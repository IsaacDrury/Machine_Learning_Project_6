using Grpc.Core;
using UnityEngine;

public class MoveAgents : MonoBehaviour
{
    [SerializeField]
    private GameObject[] agents;
    [SerializeField]
    private GameObject[] walls;

    public void Reposition()
    {
        foreach (GameObject agent in agents) 
        {
            bool goodPos = false;
            int x = Random.Range(-9, 10);
            int z = Random.Range(-9, 10);
            while (!goodPos)
            {
                goodPos = true;
                foreach (GameObject wall in walls)
                {
                    Vector3 pos = wall.transform.position;
                    if (x > pos.x - 1.0f && x < pos.x + 1.0f && z > pos.z - 1.0f && z < pos.z + 1.0f)
                    {
                        goodPos = false;
                        x = Random.Range(-9, 10);
                        z = Random.Range(-9, 10);
                        break;
                    }
                }
            }
            agent.transform.localPosition = new Vector3(x, 0.5f, z);
        }
        this.gameObject.GetComponent<SpawnItems>().SpawnFood();
    }
}

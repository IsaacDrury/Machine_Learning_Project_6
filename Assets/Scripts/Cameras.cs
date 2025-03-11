using UnityEngine;

public class Cameras : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    private GameObject[] agentCams;
    [SerializeField]
    private GameObject currentCam;
    [SerializeField]
    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        agentCams = GameObject.FindGameObjectsWithTag("AgentCamera");
        foreach (GameObject cam in agentCams)
        {
            cam.SetActive(false);
        }
    }

    public void UseMainCam()
    {
        currentCam.SetActive(false);
        currentCam = mainCam;
        currentCam.SetActive(true);
        index = 0;
    }

    public void UseAgentCams()
    {
        if (index == agentCams.Length) 
        {
            index = 0;
        }
        if (agentCams[index] != null)
        {
            currentCam.SetActive(false);
            currentCam = agentCams[index];
            currentCam.SetActive(true);
            index += 1;
        }
    }
}

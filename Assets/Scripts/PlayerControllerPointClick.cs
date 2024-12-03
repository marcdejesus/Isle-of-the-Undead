using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerPointClick : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Shoot out a ray and if hit something, store the hit into hit
            if (Physics.Raycast(ray, out hit)) {
                //Move our agent
                agent.destination = hit.point;
            }
        }
    }
}

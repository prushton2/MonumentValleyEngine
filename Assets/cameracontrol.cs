using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Vector3 point;

    void Start() {
        // agent = GetComponent<NavMeshAgent>();
    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000)) {
                point = hit.point;
                agent.destination = hit.point;
            }
        }
    }
}

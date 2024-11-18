using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Edge {
    public bool solved;
    public bool undefinedSlope;
    public float m;
    public float b;

    public override bool Equals(object obj) 
    {
        if (!(obj is Edge))
            return false;

        Edge mys = (Edge) obj;
        
        return this.solved == mys.solved &&
               this.undefinedSlope == mys.undefinedSlope &&
               this.m == mys.m &&
               this.b == mys.b;

    }
}

public class ConnectionChecker : MonoBehaviour
{

    public GameObject dot;
    public GameObject dot2;

    public GameObject[] walkables;
    public Dictionary<GameObject, Edge[]> edgeMap = new Dictionary<GameObject, Edge[]>();
    public bool reInitialize = false;
    public bool printEdges = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initializing walkability");
        walkables = GameObject.FindGameObjectsWithTag("Walkable");

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(reInitialize) {
            reInitialize = false;
            Start();
        }

        for(int i = 0; i<walkables.Length; i++) {
            edgeMap[walkables[i]] = createEdges(walkables[i]);
        }
        // createEdges(walkables[0]);
        // dot.transform.position = Camera.main.WorldToScreenPoint(walkables[0].transform.position);
    }

    Edge[] createEdges(GameObject go) {

        Edge[] edges = new Edge[12];

        Vector3[,] cornerMap = new Vector3[12,2];

        float x = go.transform.position.x;
        float y = go.transform.position.y;
        float z = go.transform.position.z;

        float hx = go.transform.localScale.x/2;
        float hy = go.transform.localScale.y/2;
        float hz = go.transform.localScale.z/2;

        //ok so i really dont like this and i will redo it, but considering my track record it will remain because "it just works"
        cornerMap[0,0] = new Vector3(x + hx, y + hy, z + hz);
        cornerMap[0,1] = new Vector3(x + hx, y + hy, z - hz);

        cornerMap[1,0] = new Vector3(x + hx, y + hy, z - hz);
        cornerMap[1,1] = new Vector3(x - hx, y + hy, z - hz);

        cornerMap[2,0] = new Vector3(x - hx, y + hy, z - hz);
        cornerMap[2,1] = new Vector3(x - hx, y + hy, z + hz);

        cornerMap[3,0] = new Vector3(x - hx, y + hy, z + hz);
        cornerMap[3,1] = new Vector3(x + hx, y + hy, z + hz);

        Debug.Log(go.name);

        for(int i = 0; i<4; i++) {
            Vector3 posA = Camera.main.WorldToScreenPoint(cornerMap[i,0]);
            Vector3 posB = Camera.main.WorldToScreenPoint(cornerMap[i,1]);

            if((posA.x - posB.x) == 0) {
                edges[i].m = 0;
                edges[i].b = posB.x;
            } else {
                edges[i].m = (posA.y-posB.y) / (posA.x-posB.x);
                edges[i].b = edges[i].m * -posA.x + posA.y;
            }

            edges[i].undefinedSlope = (posA.x - posB.x) == 0;
            edges[i].solved = true;

            // if(edges[i].undefinedSlope) {
            //     Debug.Log("x = "+edges[i].b.ToString("F2"));
            // } else {
            //     Debug.Log("y = "+edges[i].m.ToString("F2")+"x + "+edges[i].b.ToString("F2"));
            // }
        }
        return edges;
    }
}
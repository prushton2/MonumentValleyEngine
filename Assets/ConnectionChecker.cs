using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConnectionChecker : MonoBehaviour
{
    public struct Corners {
        public Vector2[] corners;
    }

    public GameObject[] walkables;
    public Corners[] corners;
    public Dictionary<GameObject, Corners> cornerMap = new Dictionary<GameObject, Corners>();
    public bool reInitialize = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initializing walkability");
        walkables = GameObject.FindGameObjectsWithTag("Walkable");
        corners = new Corners[walkables.Length];

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(reInitialize) {
            reInitialize = false;
            Start();
        }

        for(int i = 0; i<walkables.Length; i++) {
            corners[i] = getCorners(walkables[i]);
        }
        // dot.transform.position = Camera.main.WorldToScreenPoint(walkables[0].transform.position);
    }

    Corners getCorners(GameObject go) {
        Corners corners;
        corners.corners = new Vector2[8];

        for(int i = 0; i < 8; i++) {
            int x = -1 + 2*Convert.ToInt32(i%2 == 0);
            int y = -1 + 2*Convert.ToInt32(i%4 <  2);
            int z = -1 + 2*Convert.ToInt32(i%8 <  4);
            // Debug.Log(i.ToString()+": "+(-1 + 2*Convert.ToInt32(i%2 == 0)).ToString() + ", "+(-1 + 2*Convert.ToInt32(i%4 < 2)).ToString()+ ", "+(-1 + 2*Convert.ToInt32(i%8 < 4)).ToString());

            Vector3 vec = new Vector3(
                go.transform.position.x + x * go.transform.lossyScale.x/2,
                go.transform.position.y + y * go.transform.lossyScale.y/2,
                go.transform.position.z + z * go.transform.lossyScale.z/2
            );
            Vector3 point = Camera.main.WorldToScreenPoint(vec);

            Vector2 vec2 = new Vector2(point.x, point.y);
            corners.corners[i] = vec2;            
            // dot.transform.position = corners.corners[i];
            // yield return new WaitForSeconds(1);
        }
        return corners;
    }
}

/*
-1 -1 -1
-1 -1  1
-1  1 -1
-1  1  1
 1 -1 -1
 1 -1  1
 1  1 -1
 1  1  1*/
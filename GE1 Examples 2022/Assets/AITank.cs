using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AITank : MonoBehaviour {

    public float radius = 10;
    public int numWaypoints = 6;
    public int current = 0;
    List<Vector3> waypoints = new List<Vector3>();
    public float speed = 10;
    public Transform player; 
    public float explosionRadius = 1.0f;
    public int rings = 1;


    public void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            float theta = Mathf.PI * 2.0f / (float) numWaypoints;
            for(int ring = 1; ring < rings ; ring ++)
            {
                int count = Mathf.RoundToInt(Mathf.PI * 2.0f * ring);
                
                for(int i = 0 ; i < count; i ++)
                {
                    Vector3 pos = new Vector3(
                        Mathf.Cos(theta * i) * radius,
                        0,
                        Mathf.Sin(theta * i) * radius
                    );
                    pos = transform.TransformPoint(pos);
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireSphere(pos, explosionRadius);
                }
            }
        }
    }

    // Use this for initialization
    void Awake () 
    {
        float theta = Mathf.PI * 2.0f / (float) numWaypoints;   
        for(int i = 0 ; i < numWaypoints ; i ++)
        {
            Vector3 pos = new Vector3(
            Mathf.Cos(theta * i) * radius,
            0,
            Mathf.Sin(theta * i) * radius
            );
            pos = transform.TransformPoint(pos);
            waypoints.Add(pos); 
        }
    }

    // Update is called once per frame
    void Update () {

        Vector3 tankpos = transform.position;
        Vector3 nextpos = waypoints[current] - tankpos;

        float dis = nextpos.magnitude;
        if(dis < 1)
        {
          current = (current + 1) % waypoints.Count;
        }
        Vector3 direc = nextpos / dis;

        transform.position = Vector3.Lerp(transform.position, waypoints[current], Time.deltaTime);
    

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(nextpos, Vector3.up), 180 * Time.deltaTime);

        //dod.transform.parent = this.transform;

        // Task 4
        // Put code here to check if the player is in front of or behine the tank
        
        // Task 5
        // Put code here to calculate if the player is inside the field of view and in range
        // You can print stuff to the screen using:
        GameManager.Log("Hello from th AI tank");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float r;
    public float g;
    public float m;
    public Vector3 f;
    public Vector3 a;
    public Vector3 prevPos;
    public Vector3 currPos;
    public Vector3 airForce;
    public float restitution;
    public Vector3 color;
    public float wind;

    // Start is called before the first frame update
    void Start(){
    }

    /* When particles collide against a wall of the imaginarycube, they will
    bounce in thedirection of the reflected ray around the cube’s wall internal normal vector 
    (the normalpointing inside the cube).

    */
    // void CheckWalls(){ //Check in x
    //     if(currPos.x > 10-r){ //Right Wall
    //         prevPos.x = currPos.x;
    //         currPos.x = 10f-r;
    //         f.x = -f.x * restitution;
    //         a = f / m;
    //     }

    //     if(currPos.x < -10+r){ //Left Wall
    //         prevPos.x = currPos.x;
    //         currPos.x = -10+r;
    //         f.x = -f.x * restitution;
    //         a = f / m;
    //     }
    // }

    // void CheckFloorCeil(){ //Check in y
    //     if(currPos.y > 10-r){ //Ceil
    //         prevPos.y = currPos.y;
    //         currPos.y = 10-r;
    //         f.y = -f.y * restitution;
    //         a = f / m;
    //     }

    //     if (currPos.y < -10+r){ //Floor
    //         prevPos.y = currPos.y;
    //         currPos.y = -10+r;
    //         f.y = -f.y * restitution;
    //         a = f / m;
    //     }    

    // }

    void CheckFloor()
    {
        if (currPos.y < 0.000001f)
        {
            currPos.y = 0;
            currPos.x = prevPos.x;
            currPos.z = prevPos.z;
            f = -f * restitution;
        }
    }

    // void CheckBackFront(){ //Check in z
    //     if(currPos.z > 10-r){ //Front
    //         prevPos.z = currPos.z;
    //         currPos.z = 10-r;
    //         f.z = -f.z * restitution;
    //         a = f / m;
    //         //Debug.Log("Ceil?");
    //     }

    //     if (currPos.z < -10+r){ //Back
    //         prevPos.z = currPos.z;
    //         currPos.z = -10+r;
    //         f.z = -f.z * restitution;
    //         a = f / m;
    //     }

    // }
    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(currPos.y - prevPos.y) < 0.00001f && Mathf.Abs(currPos.y - r) < 0.00001f){
            currPos.y = r;
            prevPos.y = r;
            f.y = 0;
        }else{
            f.y = -m * g;
            f.x = -m * wind;
            if (currPos.y != prevPos.y){
                Vector3 vel = (currPos - prevPos) / Time.deltaTime;
                if(currPos.y > prevPos.y){
                    f.y = f.y - r * 0.001f * vel.magnitude;
                }else if (currPos.y < prevPos.y){
                    f.y = f.y + r * 0.001f * vel.magnitude;
                }
            }
        }
        a = f/m;
        Vector3 temp = currPos;
        float dt = Time.deltaTime;
        if(Time.frameCount > 100){
            // +Particles will be simulated using Verlet's integration technique
            currPos = 2 * currPos - prevPos + a * dt * dt; //Verlets
            prevPos = temp;
            // CheckWalls();
            // CheckFloorCeil();
            // CheckBackFront();
            CheckFloor();

        }
        transform.localPosition = currPos;
    }

    public bool InCollision(Particle antoher){
        float sumR2 = r + antoher.r;
        sumR2 *= sumR2;
        Vector3 c1 = transform.localPosition; //C1
        Vector3 c2 = antoher.transform.localPosition; //C2
        float dx = c2.x - c1.x;
        float dy = c2.y - c1.y;
        float dz = c2.z - c1.z;
        float d2 = dx * dx + dy * dy + dz * dz;
        return d2 < sumR2;
    }


}

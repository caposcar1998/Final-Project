﻿using System.Collections;
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

    public float bladesWind;

    public bool hasStoped;

    private bool hasBounce = false;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start(){
    }

    void CheckFloor()
    {
        if (currPos.y < r)
        {
            prevPos.y = currPos.y;
            currPos.y = r ;
            f.y = -f.y * restitution;
            a = f / m;
            hasBounce = true;
            //Esto quiere decir que toca el suelo, por lo tanto hay rebote ajua    
        }
        
    }

    void CheckOneBounce(){
        /*Si se registra el mini bounce, pues está con madre esto huerco*/
        if (currPos.y >= r+1.0f){
            hasStoped = true;
            hasBounce = false;
            
        }
        /*Si no registra el primer bounce, eliminar pq se fue a la verga*/
        if(currPos.x < -40.0f){
            hasStoped = true;
            hasBounce = false;
        }
    }


    void CheckOnWindMill(){
        //Esas medidas raras son las medidas del molino, pero solo del molino aprox
        //Mi idea era que cuando estuviera menor a sus medidas, se sabía
        //que en ese momento se encontraba con el molino, pero valió pura barriga señor verga.
        if(currPos.x < 3.199815f+r && currPos.z < 3.501239f+r && currPos.y < 6.929605f+r){
            prevPos.y = currPos.y;
            currPos.y = 6.929605f+r;
            f.y = -f.y * restitution;
            
            prevPos.x = currPos.x;
            currPos.x = 3.199815f+r;
            f.x = -f.x * restitution;
            
            prevPos.z = currPos.z;
            currPos.z = 3.501239f+r;
            f.z = -f.z * restitution;
            a = f / m;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!isPaused){
            if(Mathf.Abs(currPos.y - prevPos.y) < 0.00001f && Mathf.Abs(currPos.y - r) < 0.00001f){
                currPos.y = r;
                prevPos.y = r;
                f.y = 0;
            }else{
                f.y = -m * g;
                f.x = -m * 2.5f; //Wind from the ambiente
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
                currPos = 2 * currPos - prevPos + a * dt * dt; //Verlets
                prevPos = temp;
                CheckFloor();
                //CheckOnWindMill();
                if(hasBounce){
                    //Solo si se registra que la partícula haya tocado el suelo y rebotado
                    CheckOneBounce();
                }
            }

            transform.localPosition = currPos;
        }

        
        if(Input.GetKeyDown("r")){
            if(isPaused){
                isPaused = false;
            }else{
                isPaused = true;
            }
        }
        
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

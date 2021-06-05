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
    public float ambientWind;

    

    public bool hasStoped;

    private bool hasBounce = false;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start(){
        ambientWind = 2.5f;
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
        //Después de un "pequeño" salto, queremos que se recicle
        if (currPos.y >= r+1.0f){
            hasStoped = true;
            hasBounce = false;
            
        }
        //Esto hay que tratar de evitarlo
        //Si no se registra el salto, lo validamos conforme a la distancia que tenga en x
        if(currPos.x < -40.0f){
            hasStoped = true;
            hasBounce = false;
        }
    }

    void GoneByTheWind(float windForce){
        //La windForce es 0.0f cuando se pare la animación de la rotación del molino
        if(windForce > 0.0f){
            //Si la windForce es > 0.0f, quiere decir que tiene un valor porque el molino se mueve
            if((currPos.x >= -4.0f && currPos.x <= 4.0f) && 
                (currPos.z > 1.8f && currPos.z <= 8.0f) &&
                (currPos.y < 10f && currPos.y > 1.4f)){ 
                //Estos son meramente rangos en los que tenemos establecidos las aspas
                // en el eje x: está seteado en el origen +- las medidas de las aspas = 4.0f aprox
                // en el eje y: está seteado en 5.5 aprox +- las medidas de las aspas = 4.0f aprox
                // en el eje z: está seteado a partir de 2.8 aprox, el rango solo es un aproximado de hasta donde atacaría el viento del molino
                f.z = -m * -windForce;  //La fuerza en z+, es decir, hacia enfrente del molino
                f.y = -m * -windForce+15.0f; //La fuerza en y-, es decir, va bajando con más fuerza

            }
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
                f.y = -m * g; //
                f.x = -m * ambientWind; //Wind from the ambient
                GoneByTheWind(Blade.windForce);
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

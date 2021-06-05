﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*EQUIPO
Gerardo Arturo Miranda Godoy
Mónica Lara Pineda
Manuel Ortiz Hernández
Óscar Contreras Palacios
*/

public class Blade : MonoBehaviour
{
    float angleZ = 0;
    float dz = 10.0f;

    public static float windForce;

    Vector3 bladesRectangles = new Vector3(3.805398f/2f, 1.011605f/2f, 0.4801749f/2f);
    Vector3[] originalPoints;
    Vector3[] originalPoints2;
    Vector3[] originalPoints3;
    Vector3[] originalPoints4;
    Vector3[] originalPoints5;

    
    Vector3[] boxVertices;

    Vector3[] outputBox;

    public GameObject g1;
    public GameObject g2;
    public GameObject g3;
    public GameObject g4;
    public GameObject g5;
    public GameObject g6;
    public GameObject g7;
    public GameObject g8;

    bool isPaused;

    Vector3[] TransformBlade(Vector3[] input, Matrix4x4 r)
    {
        Vector3[] output = new Vector3[input.Length];
        outputBox = new Vector3[boxVertices.Length];
        Matrix4x4 t = Transformations.TranslateM(0, 5.45f, 1.8f);
        Matrix4x4 rotateX = Transformations.RotateM(180, Transformations.AXIS.AX_X);
        Matrix4x4 tLeft = Transformations.TranslateM(2.5f, 0, 0);

        
        for (int i = 0; i < input.Length; i++)
        {
            Vector4 temp = input[i];
            temp.w = 1;
            output[i] = t * r * tLeft * rotateX * temp;
            //output[i] = Transformations.Translate(temp, new Vector4(0,10,0,1));
        }

        for (int i = 0; i < boxVertices.Length; i++)
        {
            Vector4 temp = boxVertices[i];
            temp.w = 1;
            outputBox[i] = t * r * tLeft * rotateX  * temp;
            //output[i] = Transformations.Translate(temp, new Vector4(0,10,0,1));
        }

        return output;
    }

    

    Vector3[] TransformWindmill(Vector3[] input)
    {
        Vector3[] output = new Vector3[input.Length];
        Matrix4x4 t = Transformations.TranslateM(0, 0.8f, 0);

        for (int i = 0; i < input.Length; i++)
        {
            Vector4 temp = input[i];
            temp.w = 1;
            output[i] = t * temp;
            //output[i] = Transformations.Translate(temp, new Vector4(0,10,0,1));
        }
        return output;

    }

    void PrintBladesRectangles(Color color){
        Debug.DrawLine(boxVertices[0], boxVertices[1], color);
        Debug.DrawLine(boxVertices[1], boxVertices[2], color);
        Debug.DrawLine(boxVertices[2], boxVertices[3], color);
        Debug.DrawLine(boxVertices[3], boxVertices[4], color);
        Debug.DrawLine(boxVertices[4], boxVertices[5], color);
        Debug.DrawLine(boxVertices[5], boxVertices[6], color);
        Debug.DrawLine(boxVertices[6], boxVertices[7], color);
    }


    // Start is called before the first frame update
    void Start()
    {

        isPaused = false;
        GameObject bladeOne = GameObject.Find("default1");
        MeshFilter mf = bladeOne.GetComponent<MeshFilter>();
        originalPoints = mf.mesh.vertices;
        

        GameObject bladeTwo = GameObject.Find("default2");
        MeshFilter mf2 = bladeTwo.GetComponent<MeshFilter>();
        originalPoints2 = mf2.mesh.vertices;

        GameObject bladeThree = GameObject.Find("default3");
        MeshFilter mf3 = bladeThree.GetComponent<MeshFilter>();
        originalPoints3 = mf3.mesh.vertices;

        GameObject bladeFour = GameObject.Find("default4");
        MeshFilter mf4 = bladeFour.GetComponent<MeshFilter>();
        originalPoints4 = mf4.mesh.vertices;

        GameObject WindMill = GameObject.Find("defaultW");
        MeshFilter mf5 = WindMill.GetComponent<MeshFilter>();
        originalPoints5 = mf5.mesh.vertices;
        mf5.mesh.vertices = TransformWindmill(originalPoints5);


        boxVertices = new Vector3[8]
        {
            new Vector3(-bladesRectangles.x, -bladesRectangles.y, bladesRectangles.z),
            new Vector3(bladesRectangles.x, -bladesRectangles.y, bladesRectangles.z),
            new Vector3(bladesRectangles.x, bladesRectangles.y, bladesRectangles.z),
            new Vector3(-bladesRectangles.x, bladesRectangles.y, bladesRectangles.z),

            new Vector3(bladesRectangles.x, -bladesRectangles.y, -bladesRectangles.z),
            new Vector3(bladesRectangles.x, bladesRectangles.y, -bladesRectangles.z),
            new Vector3(-bladesRectangles.x, -bladesRectangles.y, -bladesRectangles.z),
            new Vector3(-bladesRectangles.x, bladesRectangles.y, -bladesRectangles.z)
        };

    }
    // Update is called once per frame
    void Update()
    {
        
        if(!isPaused){
            windForce = dz*4; 
            angleZ += dz;
            GameObject bladeOne = GameObject.Find("default1");
            MeshFilter mf = bladeOne.GetComponent<MeshFilter>();
            Matrix4x4 r = Transformations.RotateM(angleZ, Transformations.AXIS.AX_Z);
            mf.mesh.vertices = TransformBlade(originalPoints, r);
            
            // for(int i = 0; i < 8; i++){
            //     boxVertices[i] = bladeOne.transform.position * 3.81f;
            // }

            // g1.transform.localPosition = boxVertices[0];
            // g2.transform.localPosition = boxVertices[1];
            // g3.transform.localPosition = boxVertices[2];
            // g4.transform.localPosition = boxVertices[3];
            // g5.transform.localPosition = boxVertices[4];
            // g6.transform.localPosition = boxVertices[5];
            // g7.transform.localPosition = boxVertices[6];
            // g8.transform.localPosition = boxVertices[7];

            boxVertices = outputBox;
            PrintBladesRectangles(Color.red);
            GameObject bladeTwo = GameObject.Find("default2");
            MeshFilter mf2 = bladeTwo.GetComponent<MeshFilter>();
            Matrix4x4 r2 = Transformations.RotateM(angleZ - 90.0f, Transformations.AXIS.AX_Z);
            mf2.mesh.vertices = TransformBlade(originalPoints2, r2);
            boxVertices = outputBox; 
            PrintBladesRectangles(Color.white);
            GameObject bladeThree = GameObject.Find("default3");
            MeshFilter mf3 = bladeThree.GetComponent<MeshFilter>();
            Matrix4x4 r3 = Transformations.RotateM(angleZ - 180.0f, Transformations.AXIS.AX_Z);
            mf3.mesh.vertices = TransformBlade(originalPoints3, r3);
            boxVertices = outputBox;
            PrintBladesRectangles(Color.blue);
            GameObject bladeFour = GameObject.Find("default4");
            MeshFilter mf4 = bladeFour.GetComponent<MeshFilter>();
            Matrix4x4 r4 = Transformations.RotateM(angleZ - 270.0f, Transformations.AXIS.AX_Z);
            mf4.mesh.vertices = TransformBlade(originalPoints4, r4);
            boxVertices = outputBox;
            PrintBladesRectangles(Color.black);
        }

        if(Input.GetKeyDown("space")){
            if(isPaused){
                isPaused = false;
            }else{
                windForce = 0.0f;
                isPaused = true;
            }
        }


        //}
    }
}
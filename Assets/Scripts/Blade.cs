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
    float dz = 0.3f;
    Vector3[] originalPoints;
    Vector3[] originalPoints2;
    Vector3[] originalPoints3;
    Vector3[] originalPoints4;
    
    Vector3[] TransformBlade(Vector3[] input, Matrix4x4 t, Matrix4x4 r)
    {
        Vector3[] output = new Vector3[input.Length];
        Matrix4x4 rotateX = Transformations.RotateM(180, Transformations.AXIS.AX_X);
        Matrix4x4 tLeft = Transformations.TranslateM(2.5f, 0, 0); 

        for(int i = 0; i < input.Length; i++)
        {
            Vector4 temp = input[i];
            temp.w = 1;
            output[i] =  t * r * tLeft * rotateX * temp;
            //output[i] = Transformations.Translate(temp, new Vector4(0,10,0,1));
        }
        return output;
    }

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        angleZ += dz;

        //if(Input.GetKeyDown("space"))
        //{
            GameObject bladeOne = GameObject.Find("default1");
            MeshFilter mf = bladeOne.GetComponent<MeshFilter>();
            Matrix4x4 t = Transformations.TranslateM(0, 3.8f, 1.8f);
            Matrix4x4 r = Transformations.RotateM(angleZ, Transformations.AXIS.AX_Z);
            mf.mesh.vertices = TransformBlade(originalPoints, t, r);

            GameObject bladeTwo = GameObject.Find("default2");
            MeshFilter mf2 = bladeTwo.GetComponent<MeshFilter>();
            Matrix4x4 t2 = Transformations.TranslateM(0, 3.8f, 1.8f);
            Matrix4x4 r2 = Transformations.RotateM(angleZ, Transformations.AXIS.AX_Z);
            mf2.mesh.vertices = TransformBlade(originalPoints2, t2, r2);

            GameObject bladeThree = GameObject.Find("default3");
            MeshFilter mf3 = bladeThree.GetComponent<MeshFilter>();
            Matrix4x4 t3 = Transformations.TranslateM(0, 3.8f, 1.8f);
            Matrix4x4 r3 = Transformations.RotateM(angleZ, Transformations.AXIS.AX_Z);
            mf3.mesh.vertices = TransformBlade(originalPoints3, t3, r3);

            GameObject bladeFour = GameObject.Find("default4");
            MeshFilter mf4 = bladeFour.GetComponent<MeshFilter>();
            Matrix4x4 t4 = Transformations.TranslateM(0, 3.8f, 1.8f);
            Matrix4x4 r4 = Transformations.RotateM(angleZ, Transformations.AXIS.AX_Z);
            mf4.mesh.vertices = TransformBlade(originalPoints4, t4, r4);

        //}
    }
}

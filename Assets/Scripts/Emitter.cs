using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public GameObject ParticlePrefab; 
    private int numParticles = 500;
    private GameObject[] particles;
    private Vector3 initialPos;

    
 
    // Start is called before the first frame update
    
    void Start()
    {
        
        particles = new GameObject[numParticles];

        for(int p = 0; p < numParticles; p++){
            GameObject part = Instantiate(ParticlePrefab);
            Particle pScript = part.GetComponent<Particle>();
            pScript.hasStoped = false;
            CreateParticle(p, part, pScript);
        }

    }

    void CreateParticle(int p, GameObject part, Particle pScript){
        float diam = 0.2f;
        pScript.r = diam/2.0f;
        part.transform.localScale = new Vector3(diam, diam, diam);
        Vector3 materialColors = new Vector3(0,0, 0.4f);
        Color c = new Color(materialColors.x,materialColors.y, materialColors.z);
        Renderer rend = part.GetComponent<Renderer>();
        rend.material.SetColor("_Color", c);
        pScript.num = p;
        particles[p] = part;
        pScript.color = materialColors;
        pScript.g = 9.81f;
        pScript.m = pScript.r * 2.0f; //0.4
        pScript.currPos = new Vector3(Random.Range(5.0f, 20.0f), Random.Range(15.0f, 20.0f), Random.Range(-5.0f, 8.0f));
        pScript.restitution = 0.9f; 
        pScript.f = Vector3.zero;
        pScript.f.y = -pScript.m * pScript.g;
        pScript.f.x += Random.Range(-1.0f, 1.0f);
        pScript.f.y += 5f;
        pScript.f.z += Random.Range(-1.0f, 1.0f);
        pScript.prevPos = pScript.currPos;
    }
    // Update is called once per frame
    void Update()
    {
        
        foreach(GameObject p in particles){
            Particle pScript = p.GetComponent<Particle>();
            if(pScript.hasStoped){
                Recycle(pScript);
            }
        }


    }
    void Recycle(Particle pScript){
        pScript.hasStoped = false;
        pScript.currPos = new Vector3(Random.Range(5.0f, 20.0f), Random.Range(15.0f, 20.0f), Random.Range(-5.0f, 8.0f));
        pScript.f = Vector3.zero;
        pScript.prevPos = pScript.currPos;
        
    }
}

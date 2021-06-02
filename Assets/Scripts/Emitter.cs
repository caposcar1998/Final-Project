using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public GameObject ParticlePrefab; 
    public int numParticles;
    private GameObject[] particles;
    // Start is called before the first frame update
    
    void Start()
    {
        particles = new GameObject[numParticles];

        for(int p = 0; p < numParticles; p++){
            GameObject part = Instantiate(ParticlePrefab);
            Particle pScript = part.GetComponent<Particle>(); 
            // + Each particle will have a random radius in the range [0.3, 0.9]
            float diam = Random.Range(0.3f, 0.9f)*2; 
            pScript.r = diam/2.0f;
            part.transform.localScale = new Vector3(diam, diam, diam);

            // + Each particle will have a random material color.
            Vector3 materialColors = new Vector3(Random.Range(0.2f, 0.7f), /* - The red diffuse channel in the range [0.2, 0.7] */
                                                Random.Range(0.2f, 1.0f), /* - The green diffuse channel in the range [0.2, 1.0] */
                                                Random.Range(0.2f, 1.0f)); /* - The blue diffuse channel in the range [0.2, 1.0] */
            Color c = new Color(materialColors.x,materialColors.y, materialColors.z);
            Renderer rend = part.GetComponent<Renderer>();
            rend.material.SetColor("_Color", c);
            particles[p] = part;
            pScript.color = materialColors;
            pScript.g = 9.81f;
        
            // + Each particle's mass will equal the radius times 2
            pScript.m = pScript.r * 2.0f;

            pScript.restitution = 0.9f; 
            pScript.f = Vector3.zero;
            pScript.f.y = -pScript.m * pScript.g;
            // + The particles will explode from the emitter at(0,0, 0) with random forces in ±X ±Y and±Z.
            pScript.currPos = new Vector3(0,15,0); //
            pScript.f.x += Random.Range(-1.5f, 1.5f);
            pScript.f.y += Random.Range(-1.5f, 1.5f);
            pScript.f.z += Random.Range(-1.5f, 1.5f);

            pScript.prevPos = pScript.currPos;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup > 3.0f){      
            for(int i = 0; i < numParticles; i++){
                bool crashed = false;
                Particle p1 = particles[i].GetComponent<Particle>();
                for(int j = i+1; j < numParticles; j++){       
                    Particle p2 = particles[j].GetComponent<Particle>();
                    if(p1.InCollision(p2)){
                        // +When two particles collide, they will change theirdiffuse color tored.
                        //Change p1 to RED
                        crashed = true;
                        Renderer rend = p1.GetComponent<Renderer>();
                        rend.material.SetColor("_Color", Color.red);
                        //Change p2 to RED
                        rend = p2.GetComponent<Renderer>();
                        rend.material.SetColor("_Color", Color.red);
                        //Debug.Log("Collision PUM: " + i.ToString() + " vs " + j.ToString());
                    }
                }
                if(!crashed){
                    // +When any particle is not in collision, it will returnto itsoriginal color.
                    Color c = new Color(p1.color.x, p1.color.y, p1.color.z);
                    Renderer rend = p1.GetComponent<Renderer>();
                    rend.material.SetColor("_Color", c);
                }
            }
        }
    }
}

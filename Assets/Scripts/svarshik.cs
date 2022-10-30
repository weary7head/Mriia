using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class svarshik : MonoBehaviour
{
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("sMetal").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ps.Play();   
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    public class touchtest : MonoBehaviour
    {
        // Start is called before the first frame update
        public ParticleSystem[] pss;
        public GameObject cube;
        public AudioSource audioPlayer;
        public float explosionForce = 10f;
        public float multiplier = 3f, disappeartime = 0, restime = 1f;
        private Collider[] cols;
        private float r;
        private bool disappear = false;



    void Start()
        {
            pss = GetComponentsInChildren<ParticleSystem>();
            
        }

        // Update is called once per frame  
        void Update()
        {
         r = 10 * multiplier;
        cols = Physics.OverlapSphere(transform.position, r);
        if (disappear == true)
        {
            disappeartime += Time.deltaTime;
            //Debug.Log(disappeartime);
            if (disappeartime > restime)
            {
              cube.SetActive(true);
                disappear = false;
            }

        }
        else { disappeartime = 0; }
    }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player") 
                { 
                foreach (Collider hit in cols)
                    {
            
                    var rigidbodies= hit.GetComponent<Rigidbody>();
                        playParticle();
                        audioPlayer.Play();
                        if (rigidbodies != null)
                        {
                            rigidbodies.AddExplosionForce(explosionForce * multiplier, transform.position, r, 1 * explosionForce, ForceMode.Impulse);
                        }
                        cube.SetActive(false);
                        disappear = true;
                }
            }

        }
        void playParticle()
        {
            foreach (var p in pss)
            {
                p.Play();
               
            
            }
        }
        
    
}

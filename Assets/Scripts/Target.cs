using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRigid;
    private GameManager gameManager;
    private float minSpeed = 11f;
    private float maxSpeed = 15f;
    private float maxTorque = 10f;
    private float xRange = 4f;
    private float ySpawnPos = -2f;
    public int pointValue;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        targetRigid = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
       
        targetRigid.AddForce(RandomForce(),ForceMode.Impulse);
        targetRigid.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(),ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

     private void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {
            Destroy(this.gameObject);
            Instantiate(explosionParticle , transform.position,explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        Destroy(this.gameObject);
        if(this.pointValue > 0)
        {
            gameManager.GameOver();
        }
    }

    private Vector3 RandomForce()
    {
       return Vector3.up * Random.Range(minSpeed,maxSpeed);
    }

    private float RandomTorque()
    {
       return Random.Range(-maxTorque,maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange,xRange), ySpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{

    public float speed = 3;
    private int atkValue = 30;
    public GameObject peaBulletHitPrefab;
    public void SetAtkValue(int atkValue)
    {
        this.atkValue = atkValue;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void Start() {
        Destroy(gameObject, 10);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Zombie")   
        {
            Destroy(gameObject);
            other.GetComponent<Zombie>().TaskDamage(atkValue);
            GameObject go = GameObject.Instantiate(peaBulletHitPrefab, transform.position, Quaternion.identity);
            Destroy(go, 1);

        }

    }
}

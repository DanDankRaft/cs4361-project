using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float velocity = 2;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
       transform.GetComponent<Rigidbody>().velocity = direction;
       //auto-destory rocket after 10 seconds
       Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col) {
        if(col.GetComponent<Rocket>())
            return;

        if(col.GetComponent<Target>()) {
            col.GetComponent<Target>().RegisterHit(50, Shooter.Weapon.rocket);
        }
        GameObject.Destroy(gameObject);
    }
}

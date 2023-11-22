using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform gunOrigin;
    public LineRenderer laser;
    public float laserDamage = 1;
    public GameObject rocket;
    public float rocketCooldown = 0.7f;
    [SerializeField]
    float cooldown = 0;

    public enum Weapon {
        laser,
        rocket
    }


    // Start is called before the first frame update
    void Start()
    {
        laser.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        laser.enabled = false;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        Debug.DrawLine(gunOrigin.position, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 2)));
        if(Input.GetButton("Fire1"))
        {
            Vector3 laserPosition1 = ray.origin + ray.direction * 4;
            if(Physics.Raycast(ray, out hit)) {

                //hit target                
                if(hit.transform.GetComponent<Target>())
                    hit.transform.GetComponent<Target>().RegisterHit(laserDamage * Time.deltaTime, Weapon.laser);


                laserPosition1 = hit.point;
            }
            laser.SetPosition(0, gunOrigin.transform.position);
            laser.SetPosition(1, laserPosition1);
            laser.enabled = true;
        }
        else if(Input.GetButtonDown("Fire2") && cooldown <= 0)
        {
            Vector3 direction;
            if(Physics.Raycast(ray, out hit))
            {
                direction = Vector3.Normalize(hit.point - gunOrigin.position);
            } else {
                direction = Vector3.Normalize(Camera.main.transform.TransformPoint(Vector3.forward * 100) - gunOrigin.position);
            }
                GameObject newRocket = Instantiate(rocket, gunOrigin.position, gunOrigin.rotation);
                newRocket.name = gameObject.name + "'s Rocket";
                newRocket.GetComponent<Rocket>().direction = direction;
                cooldown = rocketCooldown;
        }
    }
}

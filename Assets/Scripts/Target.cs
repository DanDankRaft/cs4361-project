using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100;

    Color originalColor;
    public Material hitMaterial;
    // Renderer hitOverlay;
    HitTargetAnim hitOverlay;
    // Start is called before the first frame update
    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;  
        GameObject overlayPreface = new GameObject("Overlay");
        MeshFilter f = overlayPreface.AddComponent<MeshFilter>();
        f.mesh = GetComponent<MeshFilter>().mesh;
        MeshRenderer r = overlayPreface.AddComponent<MeshRenderer>();
        r.material = hitMaterial;
        r.enabled = false;
        overlayPreface.AddComponent<HitTargetAnim>();
        hitOverlay = Instantiate(overlayPreface, transform).GetComponent<HitTargetAnim>();  
    }

    // Update is called once per frame
    float overlayTimer = 0;
    float lastHit = 0;
    void Update()
    {
        //show being hit
        // if(Time.time > lastHit)
        overlayTimer = Mathf.Max(overlayTimer - Time.deltaTime, 0);
        hitOverlay.setOverlayTime(overlayTimer);
    }


    public void RegisterHit(float damage, Shooter.Weapon weaponType) {
        switch(weaponType) {
            case Shooter.Weapon.laser:
                overlayTimer += Time.deltaTime;
                hitOverlay.setOverlayTime(overlayTimer);
                break;
            case Shooter.Weapon.rocket:
                overlayTimer += 1;
                hitOverlay.setOverlayTime(overlayTimer);
                break;
        }
        lastHit = Time.time;
        health -= damage;
        if(health <= 0)
            Die();
    }

    public void Die() {
        Debug.Log(gameObject.name + " has died");
        GameObject.Destroy(gameObject);
    }
}

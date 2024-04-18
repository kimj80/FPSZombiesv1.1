using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    //assignables
    public Rigidbody rb;

    // damage
    public int gunDamage;
    public int critDamage = 100;
    public int minDamageBody = 20;
    public int maxDamageBody = 30;
    public int minDamageArm = 10;
    public int maxDamageArm = 20;

    public int maxCollision;
    int collision;
    PhysicMaterial physics_mat;

    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    //lifetime
    public float maxLifetime;

    private AudioSource bulletSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        Setup();
        bulletSoundEffect = GetComponent<AudioSource>();
        gunDamage = Random.Range(minDamageArm, maxDamageArm);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        //count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // body shot
        if (collision.collider.CompareTag("Zombie"))
        {
            gunDamage = Random.Range(minDamageBody, maxDamageBody);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(gunDamage);
        }
        // head shot
        else if (collision.collider.CompareTag("CritHit"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(critDamage);
        }
        // head shot
        else if (collision.collider.CompareTag("WeakHit"))
        {
            gunDamage = Random.Range(minDamageArm, maxDamageArm);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(gunDamage);
        }
        if (bounciness == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            bulletSoundEffect.PlayOneShot(bulletSoundEffect.clip);
        }

        return;
    }
    private void Setup()
    {
        // create new physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //assign material to collider
        GetComponent<SphereCollider>().material = physics_mat;

        // set gravity
        rb.useGravity = useGravity;
    }
}

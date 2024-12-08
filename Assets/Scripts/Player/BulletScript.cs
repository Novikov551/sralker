using HelloWorld;
using System;
using Unity.Netcode;
using UnityEngine;

public class BulletScript : NetworkBehaviour
{
    public float Speed;
    public float LifeTime;
    public float Distance;
    public int Damage;
    public LayerMask WhatIsSolid;

    public GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBulletRpc", LifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,
            transform.up, Distance, WhatIsSolid);

        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<HelloWorldPlayer>().TakeDamage(Damage);
            }

            DestroyBullet();
        }

        SubmitPositionRequestRpc(Time.fixedDeltaTime);
    }

    void DestroyBullet()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    [Rpc(SendTo.Server)]
    void SubmitPositionRequestRpc(float fixedTime)
    {
        transform.Translate(Vector3.up * Speed * fixedTime);
    }
}

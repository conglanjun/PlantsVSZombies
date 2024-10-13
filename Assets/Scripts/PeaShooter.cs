using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    public float shootDuration = 2;
    public float shootTimer = 0;

    public Transform shootPointTransform;
    public PeaBullet peaBulletPrefab;
    public float bulletSpeed = 5;


    override protected void EnableUpdate()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootDuration)
        {
            shootTimer = 0;
            Shoot();
        }

    }

    private void Shoot()
    {
        print(peaBulletPrefab);
        PeaBullet pb = GameObject.Instantiate(peaBulletPrefab, shootPointTransform.position, Quaternion.identity);
        pb.SetSpeed(bulletSpeed);
    }
}

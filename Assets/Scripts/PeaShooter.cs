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
    public int atkValue = 20;


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
        AudioManager.Instance.PlayClip(Config.shoot);
        PeaBullet pb = GameObject.Instantiate(peaBulletPrefab, shootPointTransform.position, Quaternion.identity);
        pb.SetSpeed(bulletSpeed);
        pb.SetAtkValue(atkValue);
    }
}

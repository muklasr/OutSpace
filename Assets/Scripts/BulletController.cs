﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : DamageEffector
{

    public float speed = 10f;
    public float destroyTime = 1.5f;

    // Use this for initialization
    void Start()
    {
        damage = 1;
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Trigger detected");
        Destroy(gameObject);
    }
}

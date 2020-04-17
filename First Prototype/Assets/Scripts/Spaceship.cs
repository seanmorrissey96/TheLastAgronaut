using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private DayNightCycle dnCycle;
    public GameObject spaceship;

    public float moveSpeed;
    public Vector3 speed = Vector3.right;

    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
        dnCycle.SpaceshipMove += DNCycle_SpaceshipMove;
    }

    private void DNCycle_SpaceshipMove(object sender, EventArgs e)
    {
        transform.Translate(speed * moveSpeed * Time.deltaTime);
    }
}
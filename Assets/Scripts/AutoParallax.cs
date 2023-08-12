using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoParallax : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float speed, diff;

    private void Start()
    {
        if (cam == null) cam = Camera.main;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(-1 * speed * Time.deltaTime, 0f, 0f);

        if (cam.transform.position.x >= transform.position.x + diff)
        {
            transform.position = new Vector2(cam.transform.position.x + diff, transform.position.y);
        }
    }
}
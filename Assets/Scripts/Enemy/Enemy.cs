using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Vector3 target;
    [SerializeField] private Vector3[] path;
    [SerializeField] private int pathIndex = 0;


    private void Start()
    {
        target = path[pathIndex];
    }

    private void Update()
    {
        Vector3 dir = target - transform.position;
        transform.Translate(dir * speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target) < .5f)
        {
            if (pathIndex >= path.Length-1) return;
            pathIndex++;
            target = path[pathIndex];

        }
    }
}

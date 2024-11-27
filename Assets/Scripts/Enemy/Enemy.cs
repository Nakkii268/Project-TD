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
       
        if(Vector3.Distance(new Vector3( transform.position.x, transform.position.y,0), new Vector3(target.x,target.y,0)) < .1f)
        {
            if (pathIndex >= path.Length-1) return;
            pathIndex++;
            target = path[pathIndex];

        }
        Vector2 dir = new Vector2(target.x, target.y) - new Vector2(transform.position.x, transform.position.y);
        transform.Translate(new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime);
    }
}

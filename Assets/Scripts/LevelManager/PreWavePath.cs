using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PreWavePath : MonoBehaviour
{
    [SerializeField] private Transform preWavePrefab;
    [SerializeField] private int pathIndex;
    [SerializeField] private float speed;

    private void Start()
    {
        DeActivePrefab();
    }

    public void VisualizePath(Vector2[] path)
    {
        Vector3 target;
        if (path.Length == 0) return;
        target = new Vector3(path[pathIndex].x, path[pathIndex].y, preWavePrefab.position.z);

        preWavePrefab.position = Vector3.MoveTowards(preWavePrefab.position,target ,speed * Time.deltaTime);
        
        //move
        if (Vector3.Distance(new Vector3(preWavePrefab.position.x, preWavePrefab.position.y, 0), new Vector3(target.x, target.y, 0)) < .3f)
        {
            if(pathIndex == 1) ActivePrefab();
            if (pathIndex >= path.Length - 1)
            {
                DeActivePrefab();
                return;
            }
            pathIndex++;
            target = path[pathIndex];
            Debug.Log(target);

        }
       
    }
    private void ActivePrefab()
    {
       
        preWavePrefab.gameObject.SetActive(true);
       
    }
    private void DeActivePrefab()
    {
        preWavePrefab.gameObject.SetActive(false);

    }
}

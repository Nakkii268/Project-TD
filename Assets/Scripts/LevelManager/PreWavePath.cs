using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PreWavePath : MonoBehaviour
{
    [SerializeField] private Transform preWavePrefab;
    [SerializeField] private int pathIndex;
    [SerializeField] private float speed;
    [SerializeField] public bool isPathVisualize;

    private void Start()
    {
        DeActivePrefab();
        
    }

    public IEnumerator VisualizePath(Vector2[] path)
    {
        if (path.Length == 0) yield break; 

       

        while (pathIndex < path.Length) 
        {
            if(pathIndex>= 1) ActivePrefab();
           
            if (path[pathIndex].x == -99)
            {
                isPathVisualize = false;
                DeActivePrefab();
                yield break;
            }

            Vector3 target = new Vector3(path[pathIndex].x, path[pathIndex].y, preWavePrefab.position.z);

            
            while (Vector3.Distance(preWavePrefab.position, target) > 0.2f)
            {
                preWavePrefab.position = Vector3.MoveTowards(preWavePrefab.position, target, speed * Time.deltaTime);
                yield return null; 
            }

            pathIndex++; 
        }

        isPathVisualize = false;
        DeActivePrefab(); 
    }

    private void ActivePrefab()
    {
       
        preWavePrefab.gameObject.SetActive(true);
       
    }
    private void DeActivePrefab()
    {
        

        preWavePrefab.gameObject.SetActive(false);
        pathIndex = 0;

    }
}

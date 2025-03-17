using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    
    public void SpawnParticle( GameObject target,float duration)
    {
        ParticleSystem particle = Instantiate(_particleSystem,target.transform.position,Quaternion.identity,target.transform);
        particle.Play();
        StartCoroutine(StopParticle(particle,duration));    
        
    }
    public IEnumerator StopParticle(ParticleSystem p,float duration)
    {
        if(duration == 99)
        {
            yield return null;
        }
        yield return new WaitForSeconds(duration);
        p.Stop();
        Destroy(p.gameObject);
        
        
    }

}

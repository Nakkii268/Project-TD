using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private ParticleSystem slashParticle;
    
    public void SpawnParticle( GameObject target,float duration)
    {
        ParticleSystem particle = Instantiate(_particleSystem,target.transform.position,Quaternion.identity,target.transform);
        particle.Play();
        StartCoroutine(StopParticle(particle,duration));    
        
    }
    public void HitParticle(GameObject target,ParticleSystem p)
    {
        
        ParticleSystem particle = Instantiate(p, target.transform.position, Quaternion.identity, target.transform);
        particle.Play();
    }
    public void AttackParticle(GameObject target, ParticleSystem p,Transform pos)
    {
        
        ParticleSystem particle = Instantiate(p, pos.position, Quaternion.identity, target.transform);
        particle.Play();
    }
    public void SkillParticle(GameObject target, VFXData p,Transform pos,Quaternion rotate)
    {

        ParticleSystem particle = Instantiate(p.Particle, pos.position, Quaternion.identity, target.transform);
        
        if (p.Rotatable) {
            Debug.LogWarning(rotate);
            particle.transform.rotation =rotate;  
        }
        particle.Play();
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

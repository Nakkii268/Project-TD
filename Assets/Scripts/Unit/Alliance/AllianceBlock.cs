using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceBlock : MonoBehaviour
{
    [SerializeField] private Alliance Alliance;
    [SerializeField] private Collider2D BlockCollider;
    [SerializeField] private LayerMask enemyLayer;
    public int BlockCount;
    public int MaxBlockCount;
    [SerializeField] private List<GameObject> blockedEnemies = new List<GameObject>();

    private void Start()
    {
        MaxBlockCount = Alliance.GetAllianceUnit().Block;
        BlockCount = MaxBlockCount;
        Alliance.OnUnitRetreat += Alliance_OnUnitRetreat;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (BlockCount == 0) return;
        if ((enemyLayer.value & (1<<collision.gameObject.layer))==0)  return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<Enemy>().Blocked();
            blockedEnemies.Add(collision.gameObject);
            
            BlockOccupancy();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) == 0) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<Enemy>().UnBlock();
            blockedEnemies.Remove(collision.gameObject);
            BlockFree();
        }
    }
    private void Alliance_OnUnitRetreat(object sender, System.EventArgs e)
    {
        foreach (GameObject obj in blockedEnemies)
        {
            obj.GetComponent<Enemy>().UnBlock();
        }
    }

    public List<GameObject> GetBlockedEnemy()
    {
        return blockedEnemies;
    }

    private void BlockOccupancy()
    {
        if (BlockCount <=0) return;
        BlockCount--;
    }
    private void BlockFree()
    {
        if (BlockCount >= MaxBlockCount) return;
        BlockCount++;
    }
}

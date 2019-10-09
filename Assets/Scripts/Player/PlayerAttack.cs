using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float attackRadius;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Draw(Color.red);
    }

    public void Draw( Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere  (transform.position, attackRadius);

    }
#endif

    private void CkeckColliders()
    {
        var targets = GetHittables();
        if(targets == null || targets.Count == 0)
        {
            return;
        }

        for(int i=0; i<targets.Count; i++)
        {
            targets[i].TakeDamage();
        }
    }

    public List<IHittable> GetHittables()
    {
        var colliders = Physics.OverlapSphere(transform.position, attackRadius, layerMask);
        var result = new List<IHittable>();

        for (int i = 0; i < colliders.Length; i++)
        {
            var comp = colliders[i].GetComponent<IHittable>();
            if (comp == null)
                continue;
            result.Add(comp);
        }

        return result;
    }

    public void OnAttackAnimation()
    {
        CkeckColliders();
    }
}

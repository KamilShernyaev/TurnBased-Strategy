using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    public static event EventHandler OnAnyGrenadeExplosion;

    private Vector3 targetPosition;
    private Action OnGrenadeBehaviourComplete;

    private float moveSpeed = 15f;
    private float damageRadius = 4f;

    private void Update() 
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        transform.position += moveDir * Time.deltaTime * moveSpeed;

        float reachedTargetDistance = 0.2f;
        if(Vector3.Distance(transform.position, targetPosition) < reachedTargetDistance)
        {
            Collider[] colliderArray = Physics.OverlapSphere(targetPosition, damageRadius);

            foreach (Collider collider in colliderArray)
            {
                if(collider.TryGetComponent<Unit>(out Unit targetUnit))
                {
                    targetUnit.Damage(30);
                }
            }

            OnAnyGrenadeExplosion?.Invoke(this,EventArgs.Empty);
            
            Destroy(gameObject);

            OnGrenadeBehaviourComplete();
        }
    }
    public void Setup(GridPosition targetgridPosition, Action OnGrenadeBehaviourComplete)
    {
        this.OnGrenadeBehaviourComplete = OnGrenadeBehaviourComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetgridPosition);
    }
}

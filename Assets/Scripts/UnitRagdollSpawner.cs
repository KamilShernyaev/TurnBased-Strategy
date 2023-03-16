using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] private Transform unitRagdollPrefab;
    [SerializeField] private Transform orinalRootBone;
    [SerializeField] private Unit unit;

    private HealthSystem healthSystem;

    private void Start() 
    {
        healthSystem = unit.GetHealthSystem();
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Transform ragdollTransform = Instantiate(unitRagdollPrefab, transform.position, transform.rotation);
        UnitRagdoll unitRagdoll = ragdollTransform.GetComponent<UnitRagdoll>();
        unitRagdoll.Setup(orinalRootBone);
    }
}

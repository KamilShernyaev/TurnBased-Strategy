using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnHealthMaxChanged;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;

    private float healthMax;
    private float health = 100;

    public HealthSystem(float healthMax) 
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public float GetHealth() 
    {
        return health;
    }

    public float GetHealthMax() 
    {
        return healthMax;
    }

    public float GetHealthNormalized() 
    {
        return health / healthMax;
    }

    public void Damage(float amount) 
    {
        health -= amount;
        if (health < 0) 
        {
            health = 0;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (health <= 0) 
        {
            Die();
        }
    }

    public void Die() 
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead() 
    {
        return health <= 0;
    }

    public void Heal(float amount) 
    {
        health += amount;
        if (health > healthMax) 
        {
            health = healthMax;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealComplete() 
    {
        health = healthMax;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void SetHealthMax(float healthMax, bool fullHealth) 
    {
        this.healthMax = healthMax;
        if (fullHealth) health = healthMax;
        OnHealthMaxChanged?.Invoke(this, EventArgs.Empty);
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetHealth(float health) 
    {
        if (health > healthMax) 
        {
            health = healthMax;
        }
        if (health < 0) 
        {
            health = 0;
        }
        this.health = health;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);

        if (health <= 0) 
        {
            Die();
        }
    }

     public static bool TryGetHealthSystem(GameObject getHealthSystemGameObject, out HealthSystem healthSystem, bool logErrors = false) {
        healthSystem = null;

        if (getHealthSystemGameObject != null) {
            if (getHealthSystemGameObject.TryGetComponent(out IGetHealthSystem getHealthSystem)) {
                healthSystem = getHealthSystem.GetHealthSystem();
                if (healthSystem != null) {
                    return true;
                } else {
                    if (logErrors) {
                        Debug.LogError($"Got HealthSystem from object but healthSystem is null! Should it have been created? Maybe you have an issue with the order of operations.");
                    }
                    return false;
                }
            } else {
                if (logErrors) {
                    Debug.LogError($"Referenced Game Object '{getHealthSystemGameObject}' does not have a script that implements IGetHealthSystem!");
                }
                return false;
            }
        } else {
            // No reference assigned
            if (logErrors) {
                Debug.LogError($"You need to assign the field 'getHealthSystemGameObject'!");
            }
            return false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointText;
    [SerializeField] private Unit unit;
    [SerializeField] private Image HealthBarImage;
    private HealthSystem healthSystem;
  

    private void Start() 
    {
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        UpdateActionPointText();   

        if (HealthSystem.TryGetHealthSystem(unit.gameObject, out HealthSystem healthSystem)) 
        {
            SetHealthSystem(healthSystem);
        }
    }

    private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPointText();
    }

    private void UpdateActionPointText()
    {
        actionPointText.text = unit.GetActionPoints().ToString();
    }

    public void SetHealthSystem(HealthSystem healthSystem) 
    {
        if (this.healthSystem != null) {
            this.healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
        }
        this.healthSystem = healthSystem;

        UpdateHealthBar();

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) 
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar() {
        HealthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }

    private void OnDestroy() 
    {
        healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            unit.GetMoveAction().GetValidActionGridPositionList();
        }
    }
}

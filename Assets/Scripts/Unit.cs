using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{   
   [SerializeField] private Animator unitAnimator;

    private Vector3 targetPosition;

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotateSpeed = 10f;

    private GridPosition gridPosition;

    private void Awake() 
    {
        targetPosition = transform.position;
    }

    private void Start() 
    {
        LevelGrid.instance.AddUnitGridPosition(gridPosition,this);
    }

    private void Update()
    {
        Vector3 moveDirection = targetPosition - transform.position;
        float dist = moveDirection.magnitude;
        
        if (dist > 0)
        {
            Vector3 move = moveDirection.normalized * moveSpeed * Time.deltaTime;
            unitAnimator.SetBool("isWalking", true);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            if (move.magnitude > dist)
            {
                move = moveDirection;
                unitAnimator.SetBool("isWalking", false);
            }
            transform.position += move;
        }

        GridPosition newGridPosition = LevelGrid.instance.GetGridPosition(transform.position);

        if(newGridPosition != gridPosition)
        {
            LevelGrid.instance.UnitMoveGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
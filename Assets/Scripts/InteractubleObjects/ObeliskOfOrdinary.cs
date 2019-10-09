using System;
using UnityEngine;

public class ObeliskOfOrdinary : BaseObject
{
    public override ObjectType Type => ObjectType.Obelisk;

    [SerializeField] private float radius = 5f;
    [SerializeField] private float minRadiusToActivate = 3f;
    [SerializeField] private float angleNeedPath = 360f;

    private bool playerEntered;
    private Transform cachedPlayerTransform;
    private Vector3 startPoint;
    private float currentAngle;

    private Vector3 Center => transform.position;

    protected override void Activate()
    {
        base.Activate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cachedPlayerTransform = other.transform;
            startPoint = cachedPlayerTransform.position;
            currentAngle = 0f;
            playerEntered = true;
        }
    }

    private void Update()
    {
        if (IsActivated) { return; }
        if (playerEntered)
        {
            CheckIfPlayerInCircle();
        }
    }

    protected override void Failed()
    {
        base.Failed();
        currentAngle = 0f;
    }

    private void CheckIfPlayerInCircle()
    {
        var angle = Vector3.SignedAngle(startPoint - Center, cachedPlayerTransform.position - Center, Vector3.up);
        currentAngle += angle;
        startPoint = cachedPlayerTransform.position;

        if(Mathf.Abs(currentAngle) >= angleNeedPath)
        {
            ActionCompleted();
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerEntered = false;
            currentAngle = 0f;
        }
    }
}

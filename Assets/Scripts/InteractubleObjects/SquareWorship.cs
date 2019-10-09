using UnityEngine;

public class SquareWorship : BaseObject
{
    public override ObjectType Type => ObjectType.Worship;

    [SerializeField] private float stayingWaitDuration = 1f;
    private float playerEnteringTime;
    private bool playerEntered;
    private PlayerMovement cachedPlayerTransform;
    private Vector3 previousPlayerPosition;

    private void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Player")
        {
            cachedPlayerTransform = other.GetComponent<PlayerMovement>();
            previousPlayerPosition = cachedPlayerTransform.transform.position;
            playerEnteringTime = Time.time;
            playerEntered = true;
        }
    }

    private void Update()
    {
        if(!playerEntered || IsActivated)
        {
            return;
        }
        ListenPlayerMovement();
        if ((Time.time - playerEnteringTime) >= stayingWaitDuration)
        {
            ActionCompleted();
        }
        
    }

    protected override void Failed()
    {
        base.Failed();
        playerEnteringTime = Time.time;
    }

    private void ListenPlayerMovement()
    {
        if (cachedPlayerTransform != null && cachedPlayerTransform.IsMoving)
        {
            playerEnteringTime = Time.time;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerEntered = false;
            playerEnteringTime = 0f;
        }
    }
}

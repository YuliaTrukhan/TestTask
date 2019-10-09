using System;
using UnityEngine;

public enum ObjectType
{
    Obelisk,
    Vase,
    Worship
}

public abstract class BaseObject : MonoBehaviour
{

    public static event Action<BaseObject> OnTriggered;

    public abstract ObjectType Type { get; }
    public bool IsActivated { get; protected set; }

    protected ParticleSystemEffect cachedEffect;

    protected virtual void Awake()
    {
        GameController.OnLevelRestarted += GameController_OnLevelRestarted;
    }


    private void GameController_OnLevelRestarted()
    {
        Deactivate();
    }

    protected virtual void OnDestroy()
    {
        GameController.OnLevelRestarted -= GameController_OnLevelRestarted;
    }

    protected virtual void Activate()
    {
        if (!IsActivated)
        {
            cachedEffect = EffectsController.Instance.PlayRightSequenceEffect(transform.position);
            IsActivated = true;
        }
    }

    protected virtual void ActionCompleted()
    {
        OnTriggered?.Invoke(this);
    }

    protected virtual void Failed()
    {
        cachedEffect = EffectsController.Instance.PlayWrongSequenceEffect(transform.position);
    }

    public virtual void ActivatedResult(bool isSucceed)
    {
        if (isSucceed) {
            Activate();
        }
        else
        {
            Failed();
        }
    }


    public virtual void Deactivate()
    {
        if (!IsActivated) { return; }
        IsActivated = false;
        if (cachedEffect != null)
        {
            cachedEffect.ReturnToPool();
            cachedEffect = null;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    public static EffectsController Instance { get; private set; }

    [SerializeField] private ParticleSystemEffect activatedObjectEffectTemplate;
    [SerializeField] private ParticleSystemEffect wrongSequenceEffectTemplate;

    private List<ParticleSystemEffect> pooledEffects = new List<ParticleSystemEffect>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public ParticleSystemEffect PlayRightSequenceEffect(Vector3 position)
    {
        var effect = GetPooledEffect(activatedObjectEffectTemplate);
        effect.transform.position = position;
        effect.PlayEffect();
        return effect;
    }

    public ParticleSystemEffect PlayWrongSequenceEffect(Vector3 position)
    {
        var effect = GetPooledEffect(wrongSequenceEffectTemplate);
        effect.transform.position = position;
        effect.PlayEffect();
        return effect;
    }

    private ParticleSystemEffect GetPooledEffect(ParticleSystemEffect template)
    {
        for(int i=0; i< pooledEffects.Count; i++)
        {
            if(!pooledEffects[i].IsActive && pooledEffects[i].name == template.name)
            {
                return pooledEffects[i];
            }
        }

        var newEffect = Instantiate(template);
        newEffect.name = template.name;
        pooledEffects.Add(newEffect);
        return newEffect;
    }
}

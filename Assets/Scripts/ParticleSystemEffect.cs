using UnityEngine;

public class ParticleSystemEffect : MonoBehaviour
{
    private ParticleSystem[] particleSystem;
    public bool IsActive { get; private set; }

    private void Awake()
    {
        particleSystem = GetComponentsInChildren<ParticleSystem>();
    }
    public void PlayEffect()
    {
        IsActive = true;

        for (int i = 0; i < particleSystem.Length; i++)
        {
            particleSystem[i].Clear();
            particleSystem[i].Play();
        }
    }

    public void StopEffect()
    {
        for (int i = 0; i < particleSystem.Length; i++)
        {
            particleSystem[i].Stop();
        }
    }

    public void ReturnToPool()
    {
        IsActive = false;
        StopEffect();
    }
}

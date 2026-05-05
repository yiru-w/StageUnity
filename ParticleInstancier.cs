using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInstancier : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;


    public void PlayParticle(Vector2 position, Sprite sprite)
    {
        GameObject effect = Instantiate(particlePrefab, position, Quaternion.identity);

        ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();

        var textureSheet = particleSystem.textureSheetAnimation;
        textureSheet.SetSprite(0, sprite);

        float totalTime = particleSystem.main.duration + particleSystem.main.startLifetime.constantMax;
        Destroy(effect, totalTime);
    }

}

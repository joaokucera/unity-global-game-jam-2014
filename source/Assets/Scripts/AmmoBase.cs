using UnityEngine;
using System.Collections;

public enum AmmoPowerHit
{
    PROJECTILLE = 1,
    GRANADE = 10
}

public class AmmoBase : MonoBehaviour
{
    public AmmoPowerHit ammoPowerHit = AmmoPowerHit.PROJECTILLE;
    public AudioClip explosionClip;

    private ParticleSystem granadeParticle;

    void Start()
    {
        granadeParticle = Resources.Load<ParticleSystem>("GranadeParticle");
        explosionClip = Resources.Load<AudioClip>("explosion");
    }

    void OnTriggerEnter(Collider hit)
    {
        Debug.Log(hit.name);

        if (hit.tag == "SavePoint" || hit.tag == "Player" || hit.tag == "Projectille" || hit.tag == "Granade")
        {
            return;
        }

        collider.enabled = false;
        renderer.enabled = false;

        if (hit.tag != "KillBox")
        {
            audio.PlayOneShot(explosionClip);

            if (hit.tag == "Enemy")
            {
                hit.GetComponent<EnemyBase>().RemoveHealth((int)ammoPowerHit);
            }

            if (ammoPowerHit == AmmoPowerHit.GRANADE && granadeParticle)
            {
                ParticleSystem instantiatedParticle = (ParticleSystem)Instantiate(granadeParticle, transform.position, transform.rotation);
                Destroy(instantiatedParticle.gameObject, 1f);
            }
        }

        Destroy(gameObject, explosionClip.length);
    }
}

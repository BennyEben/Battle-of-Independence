using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tendangan : MonoBehaviour
{
    public GameObject kickHitbox;

    public void ActivateKickHitbox() => kickHitbox.SetActive(true);
    public void DeactivateKickHitbox() => kickHitbox.SetActive(false);

    public float damage = 20f;
    public float destroyAfter = 0.2f;

    private void Start()
    {
        Destroy(gameObject, destroyAfter); // auto-destroy setelah sekian detik
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hitbox = other.GetComponent<Ilumisoft.HealthSystem.Hitbox>();
        if (hitbox != null)
        {
            hitbox.ApplyDamage(damage);
        }
    }
}

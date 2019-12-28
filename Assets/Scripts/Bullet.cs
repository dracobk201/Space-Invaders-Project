using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Bullet Variables
    [Header("Bullet Variables")]
    [SerializeField]
    private FloatReference BulletVelocity;
    [SerializeField]
    private FloatReference BulletTimeOfLife;
    [SerializeField]
    private GameEvent EnemyImpacted;
    [SerializeField]
    private GameEvent PlayerImpacted;
    #endregion

    private void OnEnable()
    {
        TryGetComponent(out Rigidbody2D rb);
        StartCoroutine(AutoDestruction());
        rb.velocity = Vector2.zero;
        rb.AddForce(transform.up * BulletVelocity.Value, ForceMode2D.Impulse);
    }

    private void Destroy()
    {
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    private IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(BulletTimeOfLife.Value);
        Destroy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string targetTag = other.tag;

        if (targetTag.Equals(Global.ENEMYTAG) && gameObject.tag.Equals(Global.PLAYERBULLETTAG))
        {
            EnemyImpacted.Raise();
            Destroy();
        }
        else if (targetTag.Equals(Global.PLAYERTAG) && gameObject.tag.Equals(Global.ENEMYBULLETTAG))
        {
            PlayerImpacted.Raise();
            Destroy();
        }
    }
}

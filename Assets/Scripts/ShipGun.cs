using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour
{
    [Header("Bullet Variables")]
    [SerializeField]
    private IntReference BulletsPooled;
    [SerializeField]
    private GameEvent PlayerShot;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletMagazine;
    [SerializeField]
    private Transform bulletPosition;
    private List<GameObject> bullets;

    private void Awake()
    {
        InstantiateBullets();
    }

    public void ShootBullet()
    {
        Vector2 initialPosition = Vector2.zero;
        Quaternion initialRotation = Quaternion.identity;

        initialPosition = bulletPosition.position;
        initialRotation = bulletPosition.rotation;

        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].transform.position = initialPosition;
                bullets[i].transform.rotation = initialRotation;
                bullets[i].SetActive(true);
                PlayerShot.Raise();
                break;
            }
        }
    }

    private void InstantiateBullets()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < BulletsPooled.Value; i++)
        {
            GameObject bang = Instantiate(bulletPrefab) as GameObject;
            bang.GetComponent<Transform>().SetParent(bulletMagazine.transform);
            bang.SetActive(false);
            bullets.Add(bang);
        }
    }
}

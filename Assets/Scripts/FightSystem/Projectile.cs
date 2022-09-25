using System;
using System.Collections;
using UnityEngine;

namespace FightSystem
{
    public class Projectile : MonoBehaviour
    {
        public float damage;
        public float projectileSpeed;
        public float projectileLifetime;
        public ProjectileDirection direction = ProjectileDirection.Left;

        private void Start()
        {
            print(1);
            StartCoroutine(StartProjectileTimer());
            print(transform.position);
        }

        private void FixedUpdate()
        {
            if (direction == ProjectileDirection.Left) transform.position += Vector3.left * projectileSpeed * Time.fixedDeltaTime;
            else transform.position += Vector3.right * projectileSpeed * Time.fixedDeltaTime;
        }

        private IEnumerator StartProjectileTimer()
        {
            yield return new WaitForSeconds(projectileLifetime);
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            print("die(");
            if (col.gameObject.CompareTag("Player")) col.GetComponent<ProgrammingPlayerFightSystem>().PlayerDamageTaking(damage);
            Destroy(gameObject);
        }
    }
    public enum ProjectileDirection {Right, Left}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyExploder : EnemyBase
    {
        [Header("Exploder settings")]
        public float explosionRadius = 5f;
        public int explosionDamage = 20;
        public float explosionDelay = 1f;
        public GameObject explosionEffect;

        [Header("Perseguição")]
        public float chaseSpeed = 3f;
        public float chaseRange = 10f;
        public float minDistanceToPlayer = 2f;

        private bool isExploding = false;
        private bool isChasing = false;
        private Player explosionPlayer;
        public bool isActive = true;

        protected override void Start()
        {
            explosionPlayer = GameObject.FindObjectOfType<Player>();
            Debug.Log("💣 Exploder iniciado! Player: " + (explosionPlayer != null));
        }

        protected override void Update()
        {
            if (!isActive || isExploding || explosionPlayer == null)
                return;

            float distanceToPlayer = Vector3.Distance(transform.position, explosionPlayer.transform.position);

            //  Se estiver perto o suficiente, explode
            if (distanceToPlayer <= explosionRadius)
            {
                Debug.Log(" Player no raio de explosão!");
                StartCoroutine(ExplodeAfter());
                return;
            }

            // Se estiver no alcance de perseguição, persegue o player
            if (distanceToPlayer <= chaseRange && distanceToPlayer > minDistanceToPlayer)
            {
                ChasePlayer();
            }
            // Se estiver muito perto, para de perseguir (para não empurrar o player)
            else if (distanceToPlayer <= minDistanceToPlayer)
            {
                Debug.Log("Muito perto do player, parando perseguição");
                isChasing = false;
            }
            else
            {
                isChasing = false;
            }
        }

        private void ChasePlayer()
        {
            isChasing = true;

            // Move-se em direção ao player
            Vector3 direction = (explosionPlayer.transform.position - transform.position).normalized;
            transform.position += direction * chaseSpeed * Time.deltaTime;

            // Olha para o player
            transform.LookAt(explosionPlayer.transform.position);

            Debug.Log(" Perseguindo player... Distância: " +
                     Vector3.Distance(transform.position, explosionPlayer.transform.position));
        }

        private IEnumerator ExplodeAfter()
        {
            isExploding = true;
            isChasing = false; // Para de perseguir quando começa a explodir

            Debug.Log($" Explosão em {explosionDelay} segundos...");

            // Efeito visual de alerta antes da explosão (piscar, por exemplo)
            StartCoroutine(FlashWarning());

            yield return new WaitForSeconds(explosionDelay);

            Debug.Log(" CHAMANDO EXPLODE()");
            Explode();
        }

        private IEnumerator FlashWarning()
        {
            // Faz o inimigo piscar em vermelho como aviso
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                Color originalColor = renderer.material.color;

                for (int i = 0; i < 3; i++)
                {
                    renderer.material.color = Color.red;
                    yield return new WaitForSeconds(0.2f);
                    renderer.material.color = originalColor;
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        private void Explode()
        {
            Debug.Log(" EXPLOSÃO INICIADA!");

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            Debug.Log($" Encontrou {colliders.Length} colliders no raio");

            foreach (Collider hit in colliders)
            {
                Player player = hit.GetComponent<Player>();
                if (player != null)
                {
                    Debug.Log($" ACERTOU O PLAYER! Causando {explosionDamage} de dano");
                    player.healthBase.Damage(explosionDamage);
                }

                EnemyBase enemy = hit.GetComponent<EnemyBase>();
                if (enemy != null && enemy != this)
                {
                    Debug.Log($" ACERTOU INIMIGO! Causando {explosionDamage} de dano");
                    enemy.Damage(explosionDamage);
                }
            }

            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            OnKill();
        }

        private void OnDrawGizmosSelected()
        {
            // Raio de explosão (vermelho)
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);

            // Raio de perseguição (amarelo)
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseRange);

            // Distância mínima (verde)
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, minDistanceToPlayer);
        }

        [ContextMenu("TESTAR Perseguição")]
        public void TestChase()
        {
            explosionPlayer = GameObject.FindObjectOfType<Player>();
            if (explosionPlayer != null)
            {
                Debug.Log("Testando perseguição...");
                chaseRange = 50f; // Aumenta temporariamente o alcance para testar
            }
        }
    }
}
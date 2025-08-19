using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;
namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider enemyCollider;
        public float startLife = 10f;
        [SerializeField] private float _currentLife;
        public bool lookAtPlayer = false;


        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;
        public ParticleSystem particleSystems;
        public int particlesAmount = 25;
        public FlashColor flashColor;

        [Header("Start Animation")]
        public float startAnimationDuration = 0.2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private Player _player;

        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }
        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation)
            {
                BornAnimation();
            }

        }
        protected virtual void Kill()
        {

            OnKill();

        }
        protected virtual void OnKill()
        {
            if(enemyCollider != null)
            {
                enemyCollider.enabled = false;
            }
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            if (flashColor != null)
            {
                flashColor.Flash();
            }
            if (GetComponent<ParticleSystem>() != null)
            {
                GetComponent<ParticleSystem>().Emit(particlesAmount);
            }
            _currentLife -= f;
            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion
        public void Damage(float damage)
        {
            Debug.Log("Damage dealt");
            OnDamage(damage);
        }
        public void Damage(float damage , Vector3 dir)
        {
           OnDamage(damage);
           transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.gameObject.GetComponent<Player>();

            if (p != null)
            {
                p.Damage(1);
            }
        }
        public virtual void Update()
        {
            if (lookAtPlayer) 
            {
                transform.LookAt(_player.transform.position);
            }
        }
    }
}

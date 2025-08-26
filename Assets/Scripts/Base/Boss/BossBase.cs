// BossBase.cs
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDDLE,
        WALK,
        ATTACK,
        DEATH
    }
    public class BossBase : MonoBehaviour, IDamageable
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;
        public float lookAtDuration = 0.5f;
        public Ease lookAtEase = Ease.OutQuad;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = 0.5f;

        [Header("Stats")]
        public float speed = 5f;
        public List<Transform> wayPoints;
        public HealthBase healthBase;

        [Header("Ranged Attack")]
        public GameObject projectilePrefab;
        public Transform projectileSpawnPoint;

        private StateMachine<BossAction> stateMachine;
        private Player _player;
        private bool isActive = false;
        private bool isInitialized = false;

        private void Awake()
        {
          
        }

        private void Start()
        {
        }

        public Player Player
        {
            get
            {
                if (_player == null) 
                {
                    _player = GameObject.FindObjectOfType<Player>();
                }
                return _player;
            }
        }

        private void Init()
        {
            if (isInitialized) return;
            isInitialized = true;

            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());

            if(healthBase != null)
            {
                healthBase.OnKill += OnBossKill;
            }
            else
            {
                Debug.LogWarning("HealthBase not assigned in " + gameObject.name);
            }
        }

        public void StartBoss()
        {
            if (!isActive)
            {
                isActive = true;
                Init();
                SwitchState(BossAction.INIT);
            }
        }

        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
        }

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
                ShootProjectile();
            }
            endCallback?.Invoke();
        }

        public void ShootProjectile()
        {
            if (projectilePrefab && projectileSpawnPoint)
            {
                Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            }
        }
        #endregion

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(wayPoints[UnityEngine.Random.Range(0, wayPoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInite()
        {
            SwitchState(BossAction.INIT);
        }
        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }
        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
        [NaughtyAttributes.Button]
        private void ThrowAttack()
        {
            if (projectilePrefab && projectileSpawnPoint)
            {
                Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            }
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            if (stateMachine != null)
            {
                stateMachine.SwitchState(state, this);
            }
            else
            {
                Debug.LogWarning("State machine not initialized");
            }
        }

        #endregion

        #region DAMAGE SYSTEM
        public void Damage(float damage)
        {
            throw new NotImplementedException();
        }

        public void Damage(float damage, Vector3 dir)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
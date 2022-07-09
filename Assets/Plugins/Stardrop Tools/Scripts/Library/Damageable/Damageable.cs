

namespace StardropTools
{
    public class Damageable : BaseComponent, IDamageable
    {
        [UnityEngine.Min(0)][UnityEngine.SerializeField] int hitPoints;
        [UnityEngine.Min(0)][UnityEngine.SerializeField] int maxHitPoints;
        [UnityEngine.Range(0f, 1f)][UnityEngine.SerializeField] float percent;
        [UnityEngine.SerializeField] float invulnerabilityTime = 1f;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool isDead;
        [UnityEngine.SerializeField] bool isImmortal;
        [UnityEngine.SerializeField] bool isInvulnerable;

#if UNITY_EDITOR
        [UnityEngine.Space]
        [UnityEngine.SerializeField] bool testInvulnerability;
#endif

        float timeInInvulnerability;

        public int HitPoints { get => hitPoints; }
        public int MaxHitPoints { get => maxHitPoints; }
        public float Percent { get => percent; }
        
        public bool IsDead { get => isDead; }
        public bool IsImmortal { get => isImmortal; set => isImmortal = value; }
        public bool IsInvulnerable { get => isInvulnerable; set => isInvulnerable = value; }


        public readonly BaseEvent<int> OnHitPointsChange = new BaseEvent<int>();

        public readonly BaseEvent OnDamage = new BaseEvent();
        public readonly BaseEvent<int> OnDamageAmount = new BaseEvent<int>();
        public readonly BaseEvent<float> OnDamagePercent = new BaseEvent<float>();

        public readonly BaseEvent OnHeal = new BaseEvent();
        public readonly BaseEvent<int> OnHealAmount = new BaseEvent<int>();
        public readonly BaseEvent<float> OnHealPercent = new BaseEvent<float>();

        public readonly BaseEvent OnDeath = new BaseEvent();

        public readonly BaseEvent OnRevive = new BaseEvent();
        public readonly BaseEvent<int> OnReviveAmount = new BaseEvent<int>();

        public readonly BaseEvent OnInvulnerabilityStart = new BaseEvent();
        public readonly BaseEvent OnInvulnerabilityUpdate = new BaseEvent();
        public readonly BaseEvent OnInvulnerabilityComplete = new BaseEvent();

        public void Initialize(int maxHitPoints)
        {
            base.Initialize();
            SetValues(maxHitPoints, maxHitPoints);
        }

        public void Initialize(int maxHitPoints, int hitPoints)
        {
            base.Initialize();

            this.maxHitPoints = maxHitPoints;
            SetValues(maxHitPoints, hitPoints);
        }

        public void Initialize(int maxHitPoints, float percent)
        {
            base.Initialize();

            this.maxHitPoints = maxHitPoints;
            SetValues(maxHitPoints, (int)(maxHitPoints * percent));
        }

        public void SetValues(int maxHitPoints, int hitPoints)
        {
            this.maxHitPoints = maxHitPoints;
            this.hitPoints = hitPoints;

            percent = (float)hitPoints / maxHitPoints;

            OnHitPointsChange?.Invoke(hitPoints);
        }

        public void SetValues(int maxHitPoints)
        {
            this.maxHitPoints = maxHitPoints;
            this.hitPoints = maxHitPoints;

            percent = (float)hitPoints / maxHitPoints;

            OnHitPointsChange?.Invoke(hitPoints);
        }


        // Damage
        public void ApplyDamage(int amount)
        {
            if (isImmortal || isInvulnerable)
                return;

            hitPoints = UnityEngine.Mathf.Clamp(hitPoints - amount, 0, maxHitPoints);
            percent = (float)hitPoints / maxHitPoints;

            OnDamage?.Invoke();
            OnDamageAmount?.Invoke(amount);
            OnDamagePercent?.Invoke(percent);

            OnHitPointsChange?.Invoke(hitPoints);

            if (hitPoints <= 0)
                Die();
        }

        public void ApplyDamage(Damage damage)
        {
            if (damage.DamageType == Damage.EDamageType.Value)
                ApplyDamage(damage.DamageAmount);

            else if (damage.DamageType == Damage.EDamageType.PercentMaxHealth)
                ApplyDamagePercentMaxHealth(damage.DamagePercentage);

            else if (damage.DamageType == Damage.EDamageType.PercentRemainHealth)
                ApplyDamagePercentRemainingHealth(damage.DamagePercentage);
        }

        void ApplyDamagePercentMaxHealth(float percentage)
        {
            int amount = (int)(maxHitPoints * percentage);
            ApplyDamage(amount);
        }

        void ApplyDamagePercentRemainingHealth(float percentage)
        {
            int amount = (int)(hitPoints * percentage);
            ApplyDamage(amount);
        }



        // Heal
        public void ApplyHeal(int amount)
        {
            hitPoints = UnityEngine.Mathf.Clamp(hitPoints + amount, 0, maxHitPoints);
            percent = (float)hitPoints / maxHitPoints;

            OnHeal?.Invoke();
            OnHealAmount?.Invoke(amount);
            OnHealPercent?.Invoke(percent);

            OnHitPointsChange?.Invoke(hitPoints);
        }
        
        public void ApplyHeal(Heal heal)
        {
            if (heal.HealType == Heal.EHealType.Value)
                ApplyDamage(heal.HealAmount);

            else if (heal.HealType == Heal.EHealType.PercentMaxHealth)
                ApplyHealPercentMaxHealth(heal.HealPercentage);

            else if (heal.HealType == Heal.EHealType.PercentRemainHealth)
                ApplyHealPercentRemainingHealth(heal.HealPercentage);
        }

        void ApplyHealPercentMaxHealth(float percentage)
        {
            int amount = (int)(maxHitPoints * percentage);
            ApplyDamage(amount);
        }

        void ApplyHealPercentRemainingHealth(float percentage)
        {
            int amount = (int)(hitPoints * percentage);
            ApplyDamage(amount);
        }


        public void Die()
        {
            hitPoints = 0;
            percent = 0;
            isDead = true;

            OnHitPointsChange?.Invoke(hitPoints);
            OnDeath?.Invoke();
        }

        public void Revive()
        {
            hitPoints = maxHitPoints;
            isDead = false;

            OnRevive?.Invoke();
            OnReviveAmount?.Invoke(hitPoints);

            OnHitPointsChange?.Invoke(hitPoints);
        }

        public void Revive(int amount)
        {
            hitPoints = amount;
            isDead = false;

            OnRevive?.Invoke();
            OnReviveAmount?.Invoke(amount);

            OnHitPointsChange?.Invoke(hitPoints);
            
        }

        public void Revive(float percentage)
        {
            hitPoints = (int)(maxHitPoints * percentage);
            isDead = false;

            OnRevive?.Invoke();
            OnReviveAmount?.Invoke(hitPoints);
        }

        public void InvulnerableFor(float time)
        {
            if (IsInvulnerable)
                return;

            isInvulnerable = true;

            invulnerabilityTime = time;
            timeInInvulnerability = 0;

            OnInvulnerabilityStart?.Invoke();
            LoopManager.OnUpdate.AddListener(InvulnerabilityTimer);
        }

        void InvulnerabilityTimer()
        {
            timeInInvulnerability += UnityEngine.Time.deltaTime;
            OnInvulnerabilityUpdate?.Invoke();

            if (timeInInvulnerability > invulnerabilityTime)
            {
                isInvulnerable = false;
                OnInvulnerabilityComplete?.Invoke();
                LoopManager.OnUpdate.RemoveListener(InvulnerabilityTimer);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (maxHitPoints > 0)
                percent = (float)hitPoints / maxHitPoints;
            else
                percent = 0;

            if (UnityEngine.Application.isPlaying)
            {
                if (testInvulnerability)
                    InvulnerableFor(invulnerabilityTime);
                testInvulnerability = false;
            }
        }
#endif
    }
}

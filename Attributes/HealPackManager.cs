using UnityEngine;


namespace RPG.Attributes
{
    public class HealPackManager : MonoBehaviour
    {

        public static HealPackManager instance;
        [SerializeField] GameObject levelUpParticleEffect = null;
        Health health;
        Animator animator;

        GameObject player;

        public int heathPacks;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }


        private void Start()
        {
            instance = this;
            health = player.GetComponent<Health>();
            animator = player.GetComponent<Animator>();
        }

        private void Update()
        {
        }

        public int GetHealthPackAmount()
        {
            return heathPacks;
        }

        public void consumeHealth()
        {

            if (heathPacks <= 0) return;
            //playHealthAnimation();
            //heathPacks--;
            health.healPlayer(20);

        }

        public void playHealthAnimation()
        {
            AudioManager.instance.PlaySFX(9);
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("heal");
            LevelUpEffect();
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, player.transform);
        }

        public void addHealthPack()
        {
            heathPacks++;
        }

        public void subtractHealthPack()
        {
            heathPacks--;
        }
    }

}
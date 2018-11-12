using UnityEngine;

//Класс получения урона
public class TakingDamage
{
    //Название клипа анимации получения урона
    private const string HitNameAnim = "Hit";
    
    private readonly GameObject self;
    private readonly UnitStats stats;
    private readonly Animator animator;
    private readonly AnimatorStateInfo HitLayerAnim; 

    public TakingDamage(GameObject self)
    {
        this.self = self;
        stats = self.GetComponent<UnitStats>();
        animator = self.GetComponent<Animator>();
        HitLayerAnim = animator.GetCurrentAnimatorStateInfo(1); //Слой в аниматоре с получением урона (1)значит 2 слой.
    }
    
    public void DoDamage(GameObject from, float damage)
    {
        float damageReducedByArmor = DamageArmorReduce(damage);
        
        stats.HealthCur -= damageReducedByArmor;

        PlayHitAnimation();

        //Если это AI то добавить в список атакующих
        self.GetComponent<AttackersList>().AddAttackerToEnemyList(from);
    }

    public void PlayHitAnimation()
    {
        if (!HitLayerAnim.IsName(HitNameAnim)) //Проигрывается ли анимация получения удара?
        {
            animator.SetTrigger(HitNameAnim);
        }
    }
    
    float DamageArmorReduce(float damage)
    {
        return (damage -= stats.armor) > 0 ? damage : 0;
    }
}

public class Attack
{
    public float Damage { get; set; }
    public bool IsCritical { get; set; }

    public Attack()
    {
    }

    public Attack(float damage, bool isCritical)
    {
        Damage = damage;
        IsCritical = isCritical;
    }
}
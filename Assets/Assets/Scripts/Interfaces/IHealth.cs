public interface IHealth
{
    void TakeDamage(int amount);
    void Heal(int amount);
    float GetCurrentHealth();

}
namespace ECommerceProject.Service.UserBaseServices
{
    public abstract class UserBaseService
    {
        /// <summary>
        /// Maaş hesaplaması yönetici ve personele göre farklılık gösterecek prim ve maaş ayrı hesaplanıp kullanıcıyı maaş  ve prim gösteri artı toplamda bilgisi versin istiyorum.
        /// </summary>

        public virtual decimal CalculateSalary(decimal salary)
        {
            return salary;
        }

        public abstract decimal CalculateBonus(decimal salary);

        public decimal GetTotalEarnings(decimal salary)
        {
            return CalculateBonus(salary) + CalculateSalary(salary);
        }
    }
}
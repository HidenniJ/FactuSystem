using FactuSystem.Data.Interfaces;

namespace FactuSystem.Data.Service
{
    public class ContadorServices : IContadorServices
    {
        public int ContadorVentas { get; set; }

        public void IncrementarVenta()
        {
            ContadorVentas++;
        }
    }
}

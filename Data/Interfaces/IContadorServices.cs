namespace FactuSystem.Data.Interfaces
{
    public interface IContadorServices
    {
        int ContadorVentas { get; set; }

        void IncrementarVenta();
    }
}
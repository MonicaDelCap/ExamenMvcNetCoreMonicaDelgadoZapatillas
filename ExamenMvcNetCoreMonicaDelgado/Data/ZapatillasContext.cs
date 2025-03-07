using ExamenMvcNetCoreMonicaDelgado.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenMvcNetCoreMonicaDelgado.Data
{
    public class ZapatillasContext: DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options) : base(options) { }
        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<Imagen> imagenes { get; set; }
    }
}

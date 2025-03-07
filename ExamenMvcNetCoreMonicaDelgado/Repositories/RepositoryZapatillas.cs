using ExamenMvcNetCoreMonicaDelgado.Data;
using ExamenMvcNetCoreMonicaDelgado.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
#region PROCEDURE
/*create procedure SP_GETIMAGEN_POSICION
(@posicion int ,@idproducto int)
as
	select IDIMAGEN ,IDPRODUCTO ,IMAGEN  from 
	(select cast(ROW_NUMBER() over (order by IDIMAGEN) as int )as POSICION, IDIMAGEN ,IDPRODUCTO ,IMAGEN 
	from IMAGENESZAPASPRACTICA where IDPRODUCTO = @idproducto) query where POSICION = @posicion
go*/
#endregion
namespace ExamenMvcNetCoreMonicaDelgado.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;
        public RepositoryZapatillas(ZapatillasContext con)
        {
            this.context = con;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.context.Zapatillas.ToListAsync();
        }

        public async Task<Zapatilla> FindZapatillaByIdAsync(int idzapatilla)
        {
            var consulta = from datos in this.context.Zapatillas where datos.IdProducto == idzapatilla select datos;
            Zapatilla zapatilla = await consulta.FirstOrDefaultAsync();
            return zapatilla;
        }

        public async Task<Imagen> GetImagenByPosAsync(int idzapatilla, int posicion)
        {
            string sql = "SP_GETIMAGEN_POSICION @posicion ,@idproducto";
            SqlParameter parampos = new SqlParameter("@posicion", posicion);
            SqlParameter paramid = new SqlParameter("@idproducto", idzapatilla);
            
            var consulta = this.context.imagenes.FromSqlRaw(sql, parampos, paramid);
            List<Imagen> emp = await consulta.ToListAsync();
            if (emp.Count() != 0)
            {
                return emp[0];

            }
            else
            {
                return new Imagen();
            }
        }

        public async Task<int> GetNumeroImagenesByIdZapatillaAsync(int idzapatilla)
        {
            return this.context.imagenes.Where(x => x.IdProducto == idzapatilla).Count();
        }
    }
}

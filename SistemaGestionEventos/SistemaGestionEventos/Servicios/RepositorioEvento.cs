using Dapper;
using Microsoft.Data.SqlClient;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Servicios
{
    public class RepositorioEvento : IRepositorioEvento
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //Conectar a base de datos
        public RepositorioEvento(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener Datos
        public async Task<IEnumerable<Evento>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Evento>
                (@"SELECT  
	                ev.Id,
	                us.Email AS Usuarios,
	                ev.Nombre,
	                ub.Nombre AS Ubicaciones,
	                te.Nombre AS TiposEventos,
	                ev.FechaInicio,ev.FechaFinal,ev.PrecioDeEntrada,
	                pat.Nombre AS Patrocinadores,
	                ev.Descripcion
                FROM Eventos ev
                INNER JOIN Ubicaciones ub
                ON ub.Id = ev.UbicacionId
                INNER JOIN TiposEventos te
                ON te.Id = ev.TipoEventoId
                INNER JOIN Patrocinadores pat
                ON pat.Id = ev.PatrocinadorId
                INNER JOIN Usuarios us
                ON us.Id = ev.UsuarioId");
        }

        //Obtener por id
        public async Task<Evento> ObtenerId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Evento>
                (@"SELECT  
	                ev.Id,
	                us.Email AS Usuarios,
	                ev.Nombre,
	                ub.Nombre AS Ubicaciones,
	                te.Nombre AS TiposEventos,
	                ev.FechaInicio,ev.FechaFinal,ev.PrecioDeEntrada,
	                pat.Nombre AS Patrocinadores,
	                ev.Descripcion
                FROM Eventos ev
                INNER JOIN Ubicaciones ub
                ON ub.Id = ev.UbicacionId
                INNER JOIN TiposEventos te
                ON te.Id = ev.TipoEventoId
                INNER JOIN Patrocinadores pat
                ON pat.Id = ev.PatrocinadorId
                INNER JOIN Usuarios us
                ON us.Id = ev.UsuarioId
                WHERE ev.Id = @Id",
                new { id });
        }

        //Un Evento ya existe
        public async Task<bool> Existe(string nombre)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>
                (@"SELECT 1 FROM Eventos WHERE Nombre = @Nombre", new { nombre });
            return existe == 1;
        }


        //Crear nuevo evento
        public async Task Crear(Evento evento)
        {
            using var connection = new SqlConnection(connectionString);
            var crear = await connection.QuerySingleAsync<int>
                ("Evento_Insertar",
                new
                {
                    UsuarioId = evento.UsuarioId,
                    Nombre = evento.Nombre,
                    UbicacionId = evento.UbicacionId,
                    TipoEventoId = evento.TipoEventoId,
                    FechaInicio = evento.FechaInicio,
                    FechaFinal = evento.FechaFinal,
                    PrecioDeEntrada = evento.PrecioDeEntrada,
                    PatrocinadorId = evento.PatrocinadorId,
                    Descripcion = evento.Descripcion,

                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        //Eliminar un evento
        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Eventos WHERE Id = @Id", new { id });
        }

        //Editar un evento
        public async Task Editar (EventoCrearViewModel modelo) 
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync
                (@"UPDATE Eventos
	                SET UsuarioId = @UsuarioId,
		                Nombre = @Nombre,
		                UbicacionId = @UbicacionId,
		                TipoEventoId = @TipoEventoId,
		                FechaInicio = @FechaInicio,
		                FechaFinal = @FechaFinal,
		                PrecioDeEntrada = @PrecioDeEntrada,
		                PatrocinadorId = @PatrocinadorId,
		                Descripcion = @Descripcion
	                WHERE Id = @Id", modelo);
        }
    }
}
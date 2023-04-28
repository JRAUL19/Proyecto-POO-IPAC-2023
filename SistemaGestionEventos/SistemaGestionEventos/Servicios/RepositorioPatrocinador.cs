using Dapper;
using Microsoft.Data.SqlClient;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Servicios
{
    public class RepositorioPatrocinador : IRepositorioPatrocinador
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        //Conectar a base de datos
        public RepositorioPatrocinador(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener Datos
        public async Task<IEnumerable<Patrocinador>> Obtener() 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Patrocinador>
                (@"SELECT
                Id, Nombre, Email, EmailNormalisado, Telefono, Direccion, Descripcion
                FROM Patrocinadores");
        }

        //Obtener por Id
        public async Task<Patrocinador> ObtenerId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Patrocinador>
                (@"SELECT 
                Id, Nombre, Email, EmailNormalisado, Telefono, Direccion, Descripcion
                FROM Patrocinadores WHERE Id = @Id", new { id });
        }

        //Nombre Repetido
        public async Task<bool> Existe(string nombre)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>
                (@"SELECT 1 FROM Patrocinadores WHERE Nombre = @Nombre", new { nombre });
            return existe == 1;
        }

        //Crear Nueva Ubicacion
        public async Task Crear(Patrocinador patrocinador)
        {
            using var connection = new SqlConnection(connectionString);
            var crear = await connection.QuerySingleAsync<int>
                ("Patrocinador_Insertar",
                new
                {
                    Nombre = patrocinador.Nombre,
                    Email = patrocinador.Email,
                    EmailNormalisado = patrocinador.EmailNormalisado,
                    Telefono = patrocinador.Telefono,
                    Direccion = patrocinador.Direccion,
                    Descripcion = patrocinador.Descripcion,
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        //Editar Patrocinador

        public async Task Editar(EditarPatrocinadorViewModel patrocinador)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync
                (@"
                UPDATE Patrocinadores
                SET Nombre = @Nombre,
	                Email = @Email,
	                EmailNormalisado = @EmailNormalisado,
	                Telefono = @Telefono,
	                Direccion = @Direccion,
	                Descripcion = @Descripcion
                WHERE Id = @Id", patrocinador);
        }


        //Eliminar Patrocinador
        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM Patrocinadores WHERE Id = @Id;", new { id });
        }
    }
}

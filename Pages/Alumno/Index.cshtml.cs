using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DCUFINAL.Pages.Alumno
{
    public class IndexModel : PageModel
    {
        public List<alumnoInfo> ListAlumnos = new List<alumnoInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-E635RGF;Initial Catalog=DB_Gestion_Alumnos;Persist Security Info=True;User ID=userpro;Password=checa";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Obtener el valor de b�squeda del query string
                    string busqueda = Request.Query["busqueda"];

                    // Construir la consulta SQL con el criterio de b�squeda
                    string sql = "SELECT * FROM Alumno";
                    if (!string.IsNullOrEmpty(busqueda))
                    {
                        sql += " WHERE id_alu = @busqueda";
                    }

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Agregar el par�metro de b�squeda si es necesario
                        if (!string.IsNullOrEmpty(busqueda))
                        {
                            // Convertir el valor de busqueda a entero (asumiendo que es un valor num�rico)
                            int idBusqueda = int.Parse(busqueda);
                            command.Parameters.AddWithValue("@busqueda", idBusqueda);
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                alumnoInfo alumnoInfo = new alumnoInfo();
                                alumnoInfo.id = "" + reader.GetInt32(0);
                                alumnoInfo.ced = reader.GetString(1);
                                alumnoInfo.nombre = reader.GetString(2);
                                alumnoInfo.apellido = reader.GetString(3);
                                alumnoInfo.correo = reader.GetString(4);
                                alumnoInfo.telefono = reader.GetString(5);

                                ListAlumnos.Add(alumnoInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
     
        }
    }
    public class alumnoInfo
    {
        public string id;
        public string ced;
        public string nombre;
        public string apellido;
        public string correo;
        public string telefono;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DCUFINAL.Pages.Alumno
{
    public class EditarModel : PageModel
    {
        public alumnoInfo alumnoInfo = new alumnoInfo();
        public string errorMessage = "";
        public string Messag = "";

        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
				String connectionString = "Data Source=DESKTOP-E635RGF;Initial Catalog=DB_Gestion_Alumnos;Persist Security Info=True;User ID=userpro;Password=checa";
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					String sql = "SELECT * FROM Alumno WHERE id_alu=@id";
					using (SqlCommand command = new SqlCommand(sql, con))
					{
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                alumnoInfo.id = "" + reader.GetInt32(0);
                                alumnoInfo.ced = reader.GetString(1);
                                alumnoInfo.nombre = reader.GetString(2);
                                alumnoInfo.apellido = reader.GetString(3);
                                alumnoInfo.correo = reader.GetString(4);
                                alumnoInfo.telefono = reader.GetString(5);

                            }
                        }
					}
				}
			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost() 
        {
			alumnoInfo.id = Request.Form["id"];
			alumnoInfo.ced = Request.Form["cedula"];
			alumnoInfo.nombre = Request.Form["nombre"];
			alumnoInfo.apellido = Request.Form["apellido"];
			alumnoInfo.correo = Request.Form["correo"];
			alumnoInfo.telefono = Request.Form["telefono"];
			if (alumnoInfo.id.Length == 0 || alumnoInfo.ced.Length == 0 || alumnoInfo.nombre.Length == 0 || alumnoInfo.apellido.Length == 0 ||
			  alumnoInfo.correo.Length == 0 || alumnoInfo.telefono.Length == 0)
			{
				errorMessage = "Debe llenar todos las casillas";
				return;
			}
            try
            {
				String connectionString = "Data Source=DESKTOP-E635RGF;Initial Catalog=DB_Gestion_Alumnos;Persist Security Info=True;User ID=userpro;Password=checa";
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					String sql = @$"UPDATE Alumno 
						SET ced_alu=@cedula, 
						nombre_alu=@nombre, 
						apellidos_alu=@apellido, 
						correo_alu=@correo, 
						telefono_alu=@telefono
						WHERE id_alu=@id";
					using (SqlCommand command = new SqlCommand(sql, con))
					{
						command.Parameters.AddWithValue("@cedula", alumnoInfo.ced);
						command.Parameters.AddWithValue("@nombre", alumnoInfo.nombre);
						command.Parameters.AddWithValue("@apellido", alumnoInfo.apellido);
						command.Parameters.AddWithValue("@correo", alumnoInfo.correo);
						command.Parameters.AddWithValue("@telefono", alumnoInfo.telefono);
						command.Parameters.AddWithValue("@id", alumnoInfo.id);

						command.ExecuteNonQuery();
					}
				}
			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
			Response.Redirect("/Alumno/Index");
		}
    }
}

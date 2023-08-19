using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DCUFINAL.Pages.Alumno
{
    public class CrearModel : PageModel
    {
        public alumnoInfo alumnoInfo = new alumnoInfo();
        public string errorMessage = "";
        public string Messag = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            alumnoInfo.ced = Request.Form["cedula"];
			alumnoInfo.nombre = Request.Form["nombre"];
			alumnoInfo.apellido = Request.Form["apellido"];
			alumnoInfo.correo = Request.Form["correo"];
			alumnoInfo.telefono = Request.Form["telefono"];
            if (alumnoInfo.ced.Length == 0 || alumnoInfo.nombre.Length == 0 || alumnoInfo.apellido.Length == 0 || 
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
                    String sql = "INSERT INTO Alumno " +
                        "(ced_alu, nombre_alu, apellidos_alu, correo_alu, telefono_alu) VALUES " +
                        "(@cedula, @nombre, @apellido, @correo, @telefono);";
                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@cedula", alumnoInfo.ced);
                        command.Parameters.AddWithValue("@nombre", alumnoInfo.nombre);
                        command.Parameters.AddWithValue("@apellido", alumnoInfo.apellido);
                        command.Parameters.AddWithValue("@correo", alumnoInfo.correo);
                        command.Parameters.AddWithValue("@telefono", alumnoInfo.telefono);
                    
                        command.ExecuteNonQuery();
					}
                }
            }
            catch(Exception ex)
            { 
                errorMessage += ex.Message;
                return;
            }
            alumnoInfo.ced = "";
			alumnoInfo.nombre = "";
			alumnoInfo.apellido = "";
			alumnoInfo.correo = "";
			alumnoInfo.telefono = "";
            Messag = "Alumno agregado";
            
            Response.Redirect("/Alumno/Index");
		}
    }
}

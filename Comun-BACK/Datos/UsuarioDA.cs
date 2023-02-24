using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ComunBACK.Datos
{
    public class UsuarioDA
    {
        private readonly IConfiguration configuration;

        public UsuarioDA(IConfiguration config)
        { 
            this.configuration = config;
        }

        public DataSet Usuario_Traer_Sistemas(string USR_LOGIN, string USR_PASSWORD)
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("USUARIO_Traer_Sistemas_V2", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;

                    cmd.SelectCommand.Parameters.AddWithValue("@USR_LOGIN", USR_LOGIN);
                    cmd.SelectCommand.Parameters.AddWithValue("@USR_PASSWORD", USR_PASSWORD);
                    
                    cmd.Fill(Resultados);
                }

                return Resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }

        public DataSet TRABAJADOR_TraeListadoEmpresas(string USR_LOGIN)
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("LOGIN_recuperacionClave_alternativas_V2", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmd.SelectCommand.Parameters.AddWithValue("@USR_LOGIN", USR_LOGIN);
                    cmd.Fill(Resultados);
                }

                return Resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }

        public DataSet Usuario_Traer_Email(string USR_LOGIN)
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("USUARIO_MAIL_Traer", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmd.SelectCommand.Parameters.AddWithValue("@USR_LOGIN", USR_LOGIN);
                    cmd.Fill(Resultados);
                }

                return Resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }

        public DataSet EMAIL_Solicitud_Nueva_Contraseña(string de, string para, string cc, string asunto, string cuerpo, string adjuntos, int eshtml, string nombreSistema = "", string metodoEnvio = "")
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("EMAIL_Insertar", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                    cmd.SelectCommand.Parameters.AddWithValue("@DE", de);
                    cmd.SelectCommand.Parameters.AddWithValue("@PARA", para);
                    cmd.SelectCommand.Parameters.AddWithValue("@CC", cc);
                    cmd.SelectCommand.Parameters.AddWithValue("@ASUNTO", asunto);
                    cmd.SelectCommand.Parameters.AddWithValue("@CUERPO", cuerpo);
                    cmd.SelectCommand.Parameters.AddWithValue("@ADJUNTOS", adjuntos);
                    cmd.SelectCommand.Parameters.AddWithValue("@ESHTML", eshtml);
                    cmd.SelectCommand.Parameters.AddWithValue("@NOMBRE_SISTEMA", nombreSistema);
                    cmd.SelectCommand.Parameters.AddWithValue("@METODO_ENVIA", metodoEnvio);
                    cmd.Fill(Resultados);
                }

                return Resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.Source);
            }
        }
        
        
    }

}


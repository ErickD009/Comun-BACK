using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ComunBACK.Datos;

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

    }




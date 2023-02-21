using System.Data;
using System.Data.SqlClient;
using System.Globalization;


namespace ComunBack.Datos
{
    public class VersionSISDA
    {

        private readonly IConfiguration configuration;

        public VersionSISDA(IConfiguration config)
        {
            this.configuration = config;
        }

        public DataSet Version_Traer(int SIS_AUTOID)
        {
            DataSet Resultados = new DataSet();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(configuration.GetConnectionString("cyg~comun")))
                {
                    sqlConn.Open();
                    SqlDataAdapter cmd = new SqlDataAdapter("VERSION_X_SISTEMA_Traer", sqlConn);
                    cmd.SelectCommand.CommandType = CommandType.StoredProcedure;

                    if (SIS_AUTOID != 0)
                    {
                        cmd.SelectCommand.Parameters.AddWithValue("@SIS_AUTOID", SIS_AUTOID);
                    }

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

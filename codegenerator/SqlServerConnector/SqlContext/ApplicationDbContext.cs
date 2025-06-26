using Microsoft.EntityFrameworkCore;
using SqlServerConnector.SqlContext;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SqlServerConnector;

public class ApplicationDbContext : DbContext,IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public async Task< List<DbTables>> TablesAvailable(string? connectionString)
    {
            //"Server=201.184.140.162,21433;Initial Catalog=DBDevTest;Persist Security Info=False;User ID=mmartinez;Password=Confirmar123*;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
            List<DbTables> tables = new List<DbTables>();
            //TODO: LLevar a db locar
            string queryString = "SELECT s.name AS SchemaName,"+
                                        "t.name AS TableName ,"+
                                            "c.name AS ColumnName,"+
                                            "ty.name AS DataType"+
                                " FROM sys.schemas AS s JOIN sys.tables AS t ON t.schema_id = s.schema_id"+
                                    " JOIN sys.columns AS c ON c.object_id = t.object_id"+
                                    " JOIN sys.types AS ty ON c.user_type_id = ty.user_type_id"+
                                " WHERE t.name not Like '%AspNet%'"+ 
                                " ORDER BY SchemaName,TableName,ColumnName;";

            string cs = connectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        tables.Add(new DbTables(){
                            Name = Convert.ToString(reader["TableName"])??"",
                            Schemma = Convert.ToString(reader["SchemaName"])??"",
                            Property = Convert.ToString(reader["ColumnName"])??"",
                            Type = Convert.ToString(reader["DataType"])??""
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return await Task.FromResult(tables);
    }

    public async Task<List<DbTables>> FielTablesAvailable(string? connectionString,string tableName)
    {
            List<DbTables> tables = new List<DbTables>();
            string queryString = "SELECT s.name AS SchemaName,t.name AS TableName ,c.name AS ColumnName"+ 
            "FROM sys.schemas AS s JOIN sys.tables AS t ON t.schema_id = s.schema_id JOIN sys.columns AS c ON c.object_id = t.object_id"+ 
            "WHERE  t.name='"+tableName+"'";
            string cs = connectionString?? "Server=201.184.140.162,21433;Initial Catalog=DBDevTest;Persist Security Info=False;User ID=mmartinez;Password=Confirmar123*;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        tables.Add(new DbTables(){
                            Name = Convert.ToString(reader["TableName"])??"",
                            Schemma = Convert.ToString(reader["SchemaName"])??"",
                            Property = Convert.ToString(reader["ColumnName"])??""
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return await Task.FromResult(tables);
    }
}

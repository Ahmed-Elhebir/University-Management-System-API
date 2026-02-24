using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using University_Management_System_APIs_Global;

namespace University_Management_System_APIs_DAL
{
    /// <summary>
    /// Database Helper - Handles all ADO.NET operations
    /// </summary>
    internal class DBHelper
    {
        private static readonly string _connectionString = "Server=.;Database=UniversityDB;Integrated Security=True;User Id=sa;Password=sa123456;Encrypt=False;";

        public static OperationResult<List<T>> ExecuteQuery<T>(string storedProcedure, Dictionary<string, object>? parameters, Func<SqlDataReader, T> mapper)
        {
            List<T> results = new List<T>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(mapper(reader));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return OperationResult<List<T>>.Failure($"Database error: {ex.Message}");
                    }
                }
            }
            return OperationResult<List<T>>.Success(results, "Data retrieved successfully"); ;
        }

        public static OperationResult ExecuteNonQuery(string storedProcedure, Dictionary<string, object> parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        return OperationResult.Failure($"Database error: {ex.Message}");
                    }

                    return OperationResult.Success("Operation completed successfully");
                    
                }
            }
        }
        
        public static OperationResult<Dictionary<string, object>> ExecuteNonQueryWithOutput(
            string storedProcedure, 
            Dictionary<string, object> parameters, 
            Dictionary<string, OutputParameter> outputParameters
            )
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    var outputParams = new Dictionary<string, SqlParameter>();
                    if (outputParameters != null)
                    {
                        foreach (var outputParam in outputParameters)
                        {
                            var sqlParam = new SqlParameter(outputParam.Key, outputParam.Value.Type)
                            {
                                Direction = ParameterDirection.Output
                            };

                            if (outputParam.Value.size.HasValue)
                                sqlParam.Size = outputParam.Value.size.Value;

                            if (outputParam.Value.Precision.HasValue)
                                sqlParam.Precision = outputParam.Value.Precision.Value;

                            if (outputParam.Value.Scale.HasValue)
                                sqlParam.Scale = outputParam.Value.Scale.Value;

                            command.Parameters.Add(sqlParam);
                            outputParams.Add(outputParam.Key, sqlParam);

                        }
                    }

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        var results = new Dictionary<string, object>();
                        foreach (var param in outputParams)
                        {
                            results.Add(param.Key, param.Value.Value);
                        }

                        if (results.Count > 0)
                            return OperationResult<Dictionary<string, object>>.Success(results, "Record added successfully");
                        

                        return OperationResult<Dictionary<string, object>>.Failure("No output parameters returned");
                    }
                    catch (Exception ex)
                    {
                        return OperationResult<Dictionary<string, object>>.Failure($"Database error: {ex.Message}");
                    }
                }
            }
        }

        public static OperationResult<(List<T> Data, Dictionary<string, object> OutputValues)> ExecuteNonQueryWithOutput<T>(
            string storedProcedure,
            Dictionary<string, object> parameters,
            Dictionary<string, OutputParameter> outputParameters,
            Func<SqlDataReader, T> mapper)
        {

            List<T> results = new List<T>();
            var outputValues = new Dictionary<string, object>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    var outputParams = new Dictionary<string, SqlParameter>();
                    if (outputParameters != null)
                    {
                        foreach (var outputParam in outputParameters)
                        {
                            var sqlParam = new SqlParameter(outputParam.Key, outputParam.Value.Type)
                            {
                                Direction = ParameterDirection.Output
                            };

                            if (outputParam.Value.size.HasValue)
                                sqlParam.Size = outputParam.Value.size.Value;

                            command.Parameters.Add(sqlParam);
                            outputParams.Add(outputParam.Key, sqlParam);

                        }
                    }

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(mapper(reader));
                            }
                        }

                        foreach (var param in outputParams)
                        {
                            outputValues.Add(param.Key, param.Value.Value);
                        }

                    }
                    catch (Exception ex)
                    {
                        return OperationResult<(List<T>, Dictionary<string, object>)>.Failure($"Database error: {ex.Message}");
                    }
                }
                return OperationResult<(List<T>, Dictionary<string, object>)>.Success((results, outputValues), "Data retrieved successfully");
            }
        }

        public static OperationResult<object> ExecuteScalar(string storedProcedure, Dictionary<string, object> parameters)
        {
            object result;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    try
                    {
                        connection.Open();
                        result = command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        return OperationResult.Failure($"Database error: {ex.Message}");
                    }
                }
            }
            return OperationResult<object>.Success(result, "Operation completed successfully");
        }

        public static OperationResult<T> ExecuteSingleRow<T>(
            string storedProcedure,
            Dictionary<string, object> parameters,
            Func<SqlDataReader, T> mapper)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                T result = mapper(reader);
                                return OperationResult<T>.Success(result);
                            }
                            else
                            {
                                return OperationResult<T>.Failure("No data found");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return OperationResult<T>.Failure($"Unexpected error: {ex.Message}");
                    }
                }
            }
        }
    }

    public class OutputParameter
    {
        public SqlDbType Type { get; set; }
        public int? size { get; set; }
        public byte? Precision { get; set; } // <- Total number of digits
        public byte? Scale { get; set; } // <- Digits after decimal point

        public OutputParameter(SqlDbType type, int? size = null, byte? precision = null, byte? scale = null)
        {
            this.Type = type;
            this.size = size;
            this.Precision = precision;
            this.Scale = scale;
        }
    }
}

using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Core.Handlers
{
    public class VehicleProcessing : IVehicleProcessing
    {
        private readonly NpgsqlConnection _connection;
        private readonly ILogger<VehicleProcessing> _logger;

        public VehicleProcessing(NpgsqlConnection connection, ILogger<VehicleProcessing>  logger)
        {
            _connection = connection;
            _connection.Open();
            _logger = logger;
        }

        public VehicleResult GetVehicleInfo(Guid vehicleId)
        {
            var result = new VehicleResult();
            var query = $"select * from store.v_vehicles_info where guid = '{vehicleId}';";
            var myCommand = new NpgsqlCommand(query, _connection);
            try
            {
                using var reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    result.Guid = Guid.Parse(reader[0].ToString());
                    result.VehicleType = reader[1].ToString();
                    result.Marque = reader[2].ToString();
                    result.Model = reader[3].ToString();
                    result.Engine = reader[4].ToString();
                    result.EnginePowerBhp = (int)reader[5];
                    result.TopSpeedMph = (int)reader[6];
                    result.CostUsd = (decimal)reader[7];
                    result.Price = (decimal)reader[8];
                    result.Status = reader[9].ToString();
                    result.DatePurchase = (DateTime)reader[10];
                    result.DateInsert = (DateTime)reader[11];
                    result.DateUpdate = reader[12].GetType().FullName == "System.DBNull" ? null : (DateTime)reader[12];
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("Несуществующий p_guid"))
                {
                    _logger.LogInformation("Данного guid не существует");
                }
                else
                {
                    throw;
                }
            };
            return result;
        }

        public VehicleResult GetRandomReverseVehicleInfo()
        {
            var result = new VehicleResult();
            var query = $"select * from store.v_vehicles_info order by random() limit 1;";
            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);

            using var reader = myCommand.ExecuteReader();
            if (reader.Read())
            {
                result.Guid = Guid.Parse(reader[0].ToString());
                result.VehicleType = Reverse(reader[1].ToString());
                result.Marque = Reverse(reader[2].ToString());
                result.Model = Reverse(reader[3].ToString());
                result.Engine = Reverse(reader[4].ToString());
                result.EnginePowerBhp = (int)reader[5];
                result.TopSpeedMph = (int)reader[6];
                result.CostUsd = (decimal)reader[7];
                result.Price = (decimal)reader[8];
                result.Status = Reverse(reader[9].ToString());
                result.DatePurchase = (DateTime)reader[10];
                result.DateInsert = (DateTime)reader[11];
                result.DateUpdate = reader[12].GetType().FullName == "System.DBNull" ? null : (DateTime)reader[12];
            }

            return result;
        }

        public VehicleResult InsertVehicleInfo(VehicleParams vehicleParams)
        {
            var result = new VehicleResult();
            var query = $"select store.set_vehicles_info('" +
                        $"{vehicleParams.VehicleType}'::varchar, " +
                        $"'{vehicleParams.Marque}'::varchar, " +
                        $"'{vehicleParams.Model}'::varchar," +
                        $"'{vehicleParams.Engine}'::varchar, " +
                        $"{vehicleParams.EnginePowerBhp}, " +
                        $"{vehicleParams.TopSpeedMph}, " +
                        $"'{vehicleParams.DatePurchase:O}'::timestamp, " +
                        $"{vehicleParams.CostUsd}, " +
                        $"{vehicleParams.Price}, " +
                        $"'{vehicleParams.Status}');";

            var myCommand = new NpgsqlCommand(query, _connection);
            try
            {
                using var reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    result = GetResultFromReader(reader);
                }
            }
            catch (Exception)
            {
                _logger.LogInformation("Error while inserting");
                throw;
            };

            return result;
        }

        private VehicleResult GetResultFromReader(NpgsqlDataReader reader)
        {
            var result = new VehicleResult();
            var values = reader.GetFieldValue<object[]>(0);
            result.Guid = Guid.Parse(values[0].ToString());
            result.VehicleType = values[1].ToString();
            result.Marque = values[2].ToString();
            result.Model = values[3].ToString();
            result.Engine = values[4].ToString();
            result.EnginePowerBhp = (int)values[5];
            result.TopSpeedMph = (int)values[6];
            result.CostUsd = (decimal)values[7];
            result.Price = (decimal)values[8];
            result.Status = values[9].ToString();
            result.DateInsert = (DateTime)values[10];
            result.DateUpdate = (DateTime)values[11];
            result.DatePurchase = (DateTime)values[12];
            return result;
        }
        public VehicleResult UpdateVehicleInfo(VehicleParamsExtend vehicleParams)
        {
            var result = new VehicleResult();

            var query = $"select store.set_vehicles_info('" +
                $"{vehicleParams.VehicleType}'::varchar, " +
                $"'{vehicleParams.Marque}'::varchar, " +
                $"'{vehicleParams.Model}'::varchar," +
                $"'{vehicleParams.Engine}'::varchar, " +
                $"{vehicleParams.EnginePowerBhp}, " +
                $"{vehicleParams.TopSpeedMph}, " +
                $"'{vehicleParams.DatePurchase:O}'::timestamp, " +
                $"{vehicleParams.CostUsd}, " +
                $"{vehicleParams.Price}, " +
                $"'{vehicleParams.Status}'" + 
                (vehicleParams.Guid.HasValue ? $", '{vehicleParams.Guid}'::uuid);" : ");");

            var myCommand = new NpgsqlCommand(query, _connection);
            try
            {
                using var reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    result = GetResultFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("Несуществующий p_guid"))
                {
                    _logger.LogInformation("This guid doesn't exist");
                }
                else
                {
                    throw;
                }
            };

            return result;
        }

        private string Reverse(string str)
        {
            return new string(str.ToCharArray().Reverse().ToArray());
        }


        public IEnumerable<VehicleResult> FindVehicle(FindVehicleParams vehicleParams)
        {
            var result = new List<VehicleResult>();

            if (vehicleParams.Marque == null && 
                vehicleParams.VehicleType == null &&
                vehicleParams.Model == null &&
                vehicleParams.Status == null &&
                vehicleParams.Engine == null)
            {
                throw new Exception("There is no params for filter");
            }

            var query = $"select * from store.v_vehicles_info {GenerateQueryForFind(vehicleParams)};";
            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);
            using var reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new VehicleResult
                {
                    Guid = Guid.Parse(reader[0].ToString()),
                    VehicleType = reader[1].ToString(),
                    Marque = reader[2].ToString(),
                    Model = reader[3].ToString(),
                    Engine = reader[4].ToString(),
                    EnginePowerBhp = (int)reader[5],
                    TopSpeedMph = (int)reader[6],
                    CostUsd = (decimal)reader[7],
                    Price = (decimal)reader[8],
                    Status = reader[9].ToString(),
                    DatePurchase = (DateTime)reader[10],
                    DateInsert = (DateTime)reader[11],
                    DateUpdate = reader[12].GetType().FullName == "System.DBNull" ? null : (DateTime)reader[12]
                });
            }
            
            return result;
        }

        private string GenerateQueryForFind(FindVehicleParams vehicleParams)
        {
            var result = "";
            if (!string.IsNullOrEmpty(vehicleParams.Engine))
            {
                result += AddFilter(result, "engine", vehicleParams.Engine.ToLower());
            }
            if (!string.IsNullOrEmpty(vehicleParams.Marque))
            {
                result += AddFilter(result, "marque", vehicleParams.Marque.ToLower());
            }
            if (!string.IsNullOrEmpty(vehicleParams.Model))
            {
                result += AddFilter(result, "model", vehicleParams.Model.ToLower());
            }
            if (!string.IsNullOrEmpty(vehicleParams.Status))
            {
                result += AddFilter(result, "status", vehicleParams.Status.ToLower());
            }
            if (!string.IsNullOrEmpty(vehicleParams.VehicleType))
            {
                result += AddFilter(result, "type", vehicleParams.VehicleType.ToLower());
            }

            return result;
        }

        private string AddFilter(string query, string filter, string value)
        {
            return query != "" ? $" and {filter} ilike '{value}'" : $" where {filter} ilike '{value}'";
        }

        public void NullifyRandomPrice()
        {
            var query = $"select store.nullify_price();";

            var myCommand = new NpgsqlCommand(query, _connection);
            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Error while doing some jobs");
            }
        }
    }
}

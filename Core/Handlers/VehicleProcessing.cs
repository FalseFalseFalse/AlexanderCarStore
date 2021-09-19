using Core.Interfaces;
using Core.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Handlers
{
    public class VehicleProcessing : IVehicleProcessing, IDisposable
    {
        private readonly NpgsqlConnection _connection;

        public VehicleProcessing(NpgsqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public VehicleResult GetVehicleInfo(Guid vehicleId)
        {
            var result = new VehicleResult();
            var query = $"select * from store.v_vehicles_info where c_guid = '{vehicleId}';";
            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);
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
                    result.DateUpdate = (DateTime)reader[12];
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("Несуществующий p_guid"))
                {
                    result.ErrorMessage = "Данного guid не существует";
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
                result.DateUpdate = (DateTime)reader[12];
            }

            return result;
        }

        public VehicleResult InsertVehicleInfo(VehicleParams vehicleParams)
        {
            var result = new VehicleResult();

            var query = $"select store.set_vehicles_info('{vehicleParams.VehicleType}'::varchar, '{vehicleParams.Marque}'::varchar, '{vehicleParams.Model}'::varchar," +
                $" '{vehicleParams.Engine}'::varchar, {vehicleParams.EnginePowerBhp}, {vehicleParams.TopSpeedMph}, '{vehicleParams.DatePurchase}'::timestamp, " +
                $"{vehicleParams.CostUsd}, {vehicleParams.Price}, '{vehicleParams.Status}');";

            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);
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
                Console.WriteLine("Error while inserting");
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

            var query = $"select store.set_vehicles_info('{vehicleParams.VehicleType}'::varchar, '{vehicleParams.Marque}'::varchar, '{vehicleParams.Model}'::varchar," +
                $" '{vehicleParams.Engine}'::varchar, {vehicleParams.EnginePowerBhp}, {vehicleParams.TopSpeedMph}, '{vehicleParams.DatePurchase}'::timestamp, " +
                $"{vehicleParams.CostUsd}, {vehicleParams.Price}, '{vehicleParams.Status}'" + (vehicleParams.Guid.HasValue ? $", '{vehicleParams.Guid}'::uuid);" : ");");

            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);
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
                    result.ErrorMessage = "Данного guid не существует";
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

            var query = $"select * from store.v_vehicles_info {GenerateQueryForFind(vehicleParams)};";
            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);
            using var reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                result.Add(GetResultFromReader(reader));
            }
            return result;
        }

        private string GenerateQueryForFind(FindVehicleParams vehicleParams)
        {
            string result = "";
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
            if (query != "")
            {
                return $" and {filter} ilike '{value}'";
            }
            else
                return $" where {filter} ilike '{value}'";
        }

        public void NullifyRandomPrice()
        {
            var query = $"select store.nullify_price();";

            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);
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

using Core.Interfaces;
using Core.Models;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Core.Handlers
{
    public class StatisticHandler : IStatistic
    {
        private readonly NpgsqlConnection _connection;
        public StatisticHandler(NpgsqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }
        public IEnumerable<MarqueStatistic> GetStatisticOnMarques()
        {
            var result = new List<MarqueStatistic>();
            var query = $"select marque, count from store.v_statistic_on_marques;";

            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);
            using var reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new MarqueStatistic
                {
                    Marque = reader[0].ToString(),
                    CountType = Convert.ToInt32(reader[1])
                }
                );
            }
            return result;
        }

        public IEnumerable<StatusStatistic> GetStatisticOnStatuses()
        {
            var result = new List<StatusStatistic>();
            var query = $"select status, count from store.v_statistic_on_statuses;";

            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);
            using var reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new StatusStatistic
                {
                    Status = reader[0].ToString(),
                    CountType = Convert.ToInt32(reader[1])
                }
                );
            }
            return result;
        }

        public IEnumerable<TypeStatistic> GetStatisticOnTypes()
        {
            var result = new List<TypeStatistic>();
            var query = $"select type, count from store.v_statistic_on_types;";

            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);
            using var reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new TypeStatistic
                {
                    VehicleType = reader[0].ToString(),
                    CountType = Convert.ToInt32(reader[1])
                }
                );
            }
            return result;
        }

        public GeneralStatistic GetGeneralStatistic()
        {
            var result = new GeneralStatistic();
            var query = $"select type, status, marque from store.v_general_statistic;";

            NpgsqlCommand myCommand = new NpgsqlCommand(query, _connection);
            using var reader = myCommand.ExecuteReader();

            if (reader.Read())
            {
                result.VehicleType = reader[0].ToString();
                result.Status = reader[1].ToString();
                result.Marque = reader[2].ToString();
                
            }
            return result;
        }
    }
}

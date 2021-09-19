using Core.Interfaces;
using Core.Models;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Core.Handlers
{
    public class StatisticsHandler : IStatisticsHandler
    {
        private readonly NpgsqlConnection _connection;
        private readonly string GetStatisticsOnMarquesQuery = "select marque, count from store.v_statistic_on_marques;";
        private readonly string GetStatisticsOnStatusesQuery = "select status, count from store.v_statistic_on_statuses;";
        private readonly string GetStatisticsOnTypesQuery = "select type, count from store.v_statistic_on_types;";
        private readonly string GetGeneralStatisticsQuery = "select type, status, marque from store.v_general_statistic;";
        public StatisticsHandler(NpgsqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }
        public IEnumerable<MarqueStatistics> GetStatisticsOnMarques()
        {
            var result = new List<MarqueStatistics>();

            NpgsqlCommand myCommand = new NpgsqlCommand(GetStatisticsOnMarquesQuery, _connection);
            using var reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new MarqueStatistics
                {
                    Marque = reader[0].ToString(),
                    CountType = Convert.ToInt32(reader[1])
                });
            }

            return result;
        }

        public IEnumerable<StatusStatistics> GetStatisticsOnStatuses()
        {
            var result = new List<StatusStatistics>();

            NpgsqlCommand myCommand = new NpgsqlCommand(GetStatisticsOnStatusesQuery, _connection);
            using var reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new StatusStatistics
                {
                    Status = reader[0].ToString(),
                    CountType = Convert.ToInt32(reader[1])
                });
            }

            return result;
        }

        public IEnumerable<TypeStatistics> GetStatisticsOnTypes()
        {
            var result = new List<TypeStatistics>();

            NpgsqlCommand myCommand = new NpgsqlCommand(GetStatisticsOnTypesQuery, _connection);
            using var reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new TypeStatistics
                {
                    VehicleType = reader[0].ToString(),
                    CountType = Convert.ToInt32(reader[1])
                });
            }

            return result;
        }

        public GeneralStatistics GetGeneralStatistics()
        {
            var result = new GeneralStatistics();

            NpgsqlCommand myCommand = new NpgsqlCommand(GetGeneralStatisticsQuery, _connection);
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

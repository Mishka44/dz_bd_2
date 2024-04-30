using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz_bd_2
{
    public class DB
    {
        /*private static string filename = "test.db";*/
        private static string conn_string = "Data Source = \"WEATHER.db\";";


        private static string init_query =
            $"CREATE IF not exists TABLE Weather  (" +
            $"ID     INTEGER       PRIMARY KEY AUTOINCREMENT," +
            $"precipitation   VARCHAR (50)," +
            $"TIME VARCHAR (250)," +
            $"TEMPERATURE INTEGER);";

        private static string init_data = "INSERT INTO weather  (" +
            "precipitation," +
            "TIME," +
            "TEMPERATURE) " +
            " VALUES ('Дождливо'," +
            "                  '00000000'," +
            "25);";
        private static string select_of_data = "SELECT *  FROM weather;";

        public static void Prepare()
        {
            SQLiteConnection connection = new SQLiteConnection(conn_string);
            connection.Open();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = init_query;
            command.ExecuteNonQuery();
            connection.Close();

        }
        public static void InitData()
        {
            add_data(init_data);
        }

        public static void add_data(string query)
        {
            SQLiteConnection connection = new SQLiteConnection(conn_string);
            connection.Open();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = init_data;
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static string GetAllData(string separator)
        {
            return GetData(select_of_data, separator);
        }

        public static string GetData(string query, string separator = "\t|\t ")
        {
            string result = "";
            SQLiteConnection connection = new SQLiteConnection(conn_string);
            connection.Open();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = query;
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (i == reader.FieldCount - 1)
                    {
                        result += reader.GetValue(i);
                    }
                    else
                    {
                        result += reader.GetValue(i);
                    }

                }
                result += "\n";
            }
            connection.Close();
            return result;
        }
    }

    }

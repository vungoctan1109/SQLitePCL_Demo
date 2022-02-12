using SQLitePCL;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace Demo_SQLite.Data
{
    internal class DatabaseInitialize
    {
        public static bool CreateTable()
        {
            var conn = new SQLiteConnection("sqlitepcldemo.db");
            string sql = @"CREATE TABLE IF NOT EXISTS
                        PersonalTransaction (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                        Name VARCHAR( 140 ),
                        Description VARCHAR( 140 ),
                        Detail TEXT,
                        Money DOUBLE,
                        CreatedAt DATE,
                        Category INT
            );";

            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
            return true;
        }

        public static bool Save(Entity.PersonalTransaction personalTransaction)
        {
            var conn = new SQLiteConnection("sqlitepcldemo.db");

            using (var personaltrans = conn.Prepare("INSERT INTO PersonalTransaction (Name, Description, Detail, Money, CreatedAt, Category) VALUES (?, ?, ?, ?, ?, ?)"))
            {
                personaltrans.Bind(1, personalTransaction.Name);
                personaltrans.Bind(2, personalTransaction.Description);
                personaltrans.Bind(3, personalTransaction.Detail);
                personaltrans.Bind(4, personalTransaction.Money);
                personaltrans.Bind(5, personalTransaction.CreatedAt.ToString("dd/MM/yyyy"));
                personaltrans.Bind(6, personalTransaction.Category);
                personaltrans.Step();
            }
            return true;
        }

        public static List<Entity.PersonalTransaction> SelectAll()
        {
            var conn = new SQLiteConnection("sqlitepcldemo.db");
            var list = new List<Entity.PersonalTransaction>();
            using (var statement = conn.Prepare(@"SELECT Name,Description,Detail,Money,CreatedAt,Category FROM PersonalTransaction;"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    list.Add(new Entity.PersonalTransaction()
                    {
                        Name = (string)statement[0],
                        Description = (string)statement[1],
                        Detail = (string)statement[2],
                        Money = (double)statement[3],
                        CreatedAt = System.DateTime.ParseExact((string)statement[4], "dd/MM/yyyy", null),
                        Category = (int)(long)statement[5]
                    });
                }
                return list;
            }
        }
        public static List<Entity.PersonalTransaction> Filter(DateTime startDate, DateTime endDate, int? category)
        {
            var conn = new SQLiteConnection("sqlitepcldemo.db");
            var list = new List<Entity.PersonalTransaction>();
            var stringStatement = "SELECT Name,Description,Detail,Money,CreatedAt,Category FROM PersonalTransaction";


            if (category.ToString() != null)
            {
                stringStatement += $" WHERE (Category ={category})";
            }

            if (startDate != null && endDate != null)
            {
                stringStatement += $" AND (CreatedAt BETWEEN '{startDate.ToString("dd/MM/yyyy")}' AND '{endDate.ToString("dd/MM/yyyy")}');";
            }
            Debug.WriteLine(stringStatement);

            using (var statement = conn.Prepare(stringStatement))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    list.Add(new Entity.PersonalTransaction()
                    {
                        Name = (string)statement[0],
                        Description = (string)statement[1],
                        Detail = (string)statement[2],
                        Money = (double)statement[3],
                        CreatedAt = System.DateTime.ParseExact((string)statement[4], "dd/MM/yyyy", null),
                        Category = (int)(long)statement[5]
                    });
                }
                return list;
            }
        }
    }
}
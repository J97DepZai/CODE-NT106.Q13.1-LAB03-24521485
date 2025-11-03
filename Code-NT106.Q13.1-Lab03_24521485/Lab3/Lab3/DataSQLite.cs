using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;


namespace Lab3
{
    internal class DataSQLite
    {
        private const string Connection = "Data Source=RandomFood.db";
        public static string ConnectionString => Connection;

        public static void InitializeDatabase()
        {
            if (!File.Exists("RandomFood.db"))
            {
                using (var connection = new SqliteConnection(Connection))
                {
                    try
                    {
                        connection.Open();
                        string createUserTable = @"
                        CREATE TABLE NguoiDung (
                            IDNCC INTEGER PRIMARY KEY,
                            HoTen TEXT NOT NULL,
                            QuyenHan TEXT
                        );";
                        string createFoodTable = @"
                        CREATE TABLE MonAn (
                            IDMA INTEGER PRIMARY KEY,
                            TenMonAn TEXT NOT NULL,
                            HinhAnh TEXT,
                            IDNCC INTEGER,
                            FOREIGN KEY(IDNCC) REFERENCES NguoiDung(IDNCC)
                        );";
                        new SqliteCommand(createUserTable, connection).ExecuteNonQuery();
                        new SqliteCommand(createFoodTable, connection).ExecuteNonQuery();
                        InsertDefaultData(connection);
                        MessageBox.Show("Database đã được tạo thành công!", "Thông báo");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Lỗi khởi tạo Database", "Lỗi SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private static void InsertDefaultData(SqliteConnection connection)
        {
            string insertUsers = @"
                INSERT INTO NguoiDung (IDNCC, HoTen, QuyenHan) VALUES 
                (1, 'Nguyen Van A', 'Admin'),
                (2, 'Nguyen Hoang B', 'User'),
                (3, 'Nguyen Dinh Quoc', 'Consumer');";
            string insertFoods = @"
                INSERT INTO MonAn (IDMA, TenMonAn, HinhAnh, IDNCC) VALUES 
                (101, 'Phở', 'pho.jpg', 1),
                (102, 'Bún Chả', 'buncha.jpg', 1),
                (103, 'Cơm Tấm', 'comtam.jpg', 2),
                (104, 'Gỏi Cuốn', 'goicuon.jpg', 3),
                (105, 'Bún riêu', 'bunrieu.jpg', 3)";
            using (var command = new SqliteCommand(insertUsers, connection))
            {
                command.ExecuteNonQuery();
            }
            using (var command = new SqliteCommand(insertFoods, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}

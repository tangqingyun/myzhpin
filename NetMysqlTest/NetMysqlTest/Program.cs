using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace NetMysqlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server=192.168.7.88;database=qingyun;uid=root;pwd=admin";//连接字符串
            con.Open();//打开数据库连接
            if (con.State != System.Data.ConnectionState.Open) { 
                Console.WriteLine("数据库可以连接失败"); 
            }
            string sql = "insert into student(name,sex,createtime) values('lisi','女',now())";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            //MySqlDataReader myread = cmd.ExecuteReader();
            Console.WriteLine("添加成功");
            con.Close();
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;


namespace PWMS.DataClass
{
    
    class MyMeans
    {
        #region  全局变量
        public static string Login_ID = ""; //定义全局变量，记录当前登录的用户编号
        public static string Login_Name = "";  //定义全局变量，记录当前登录的用户名
        public static string Mean_SQL = "", Mean_Table = "", Mean_Field = "";  //定义全局变量，记录“基础信息”各窗体中的表名及SQL语句
        public static OleDbConnection My_con;  //定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        //public static string M_str_sqlcon = "Data Source=mrwxk\\wxk;Database=db_PWMS;User id=sa;PWD=";
        //public static string M_str_sqlcon = "Data Source=localhost;Database=db_PWMS;User id=sa;PWD=nawenyi@126.com";  //本机
        public static string M_str_sqlcon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Environment.CurrentDirectory + "\\linChuang.accdb'"; 

        public static int Login_n = 0;  //用户登录与重新登录的标识
        public static string AllSql = "Select * from tb_Stuffbusic";    //存储职工基本信息表中的SQL语句
        //public static int res = 0;

        //conn_str = null;
        // ole_connection = null;
        private OleDbCommand ole_command = null;
        private OleDbDataReader ole_reader = null;
        private DataTable dt = null;


        #endregion

        #region  建立数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回OleDbConnection对象</returns>
        public static OleDbConnection getcon()
        {
            My_con = new OleDbConnection(M_str_sqlcon);   //用SqlConnection对象与指定的数据库相连接
            My_con.Open();  //打开数据库连接
            return My_con;  //返回SqlConnection对象的信息
        }
        #endregion

        #region  测试数据库是否赋加
        /// <summary>
        /// 测试数据库是否赋加
        /// </summary>
        public void con_open()
        {
            getcon();
            //con_close();
        }
        #endregion

        #region  关闭数据库连接
        /// <summary>
        /// 关闭于数据库的连接.
        /// </summary>
        public void con_close()
        {
            if (My_con.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                My_con.Close();   //关闭数据库的连接
                My_con.Dispose();   //释放My_con变量的所有空间
            }
        }
        #endregion

        #region  读取指定表中的信息
        /// <summary>
        /// 读取指定表中的信息.
        /// </summary>
        /// <param name="SQLstr">SQL语句</param>
        /// <returns>返回bool型</returns>
        public OleDbDataReader getcom(string SQLstr)
        {
            getcon();   //打开与数据库的连接
            ole_command = new OleDbCommand();
            ole_command.Connection = My_con;
            ole_command.CommandText = SQLstr;    //获取指定的SQL语句
            ole_reader = ole_command.ExecuteReader(); //执行SQL语名句，生成一个SqlDataReader对象
            return ole_reader;
        }
        #endregion

        #region 执行SqlCommand命令
        /// <summary>
        /// 执行SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void getsqlcom(string SQLstr)
        {
            getcon();   //打开与数据库的连接
            
            //创建一个SqlCommand对象，用于执行SQL语句
            ole_command = new OleDbCommand();
            ole_command.Connection = My_con;
            ole_command.CommandText = SQLstr;
            
            ole_command.ExecuteNonQuery();   //执行SQL语句
            ole_command.Dispose();   //释放所有空间
            con_close();    //调用con_close()方法，关闭与数据库的连接
        }
        #endregion

        #region  创建DataSet对象
        /// <summary>
        /// 创建一个DataSet对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <param name="M_str_table">表名</param>
        /// <returns>返回DataSet对象</returns>
        public DataSet getDataSet(string SQLstr, string tableName)
        {
            getcon();   //打开与数据库的连接
            OleDbDataAdapter SQLda = new OleDbDataAdapter(SQLstr, My_con);  //创建一个SqlDataAdapter对象，并获取指定数据表的信息
            DataSet My_DataSet = new DataSet(); //创建DataSet对象
            SQLda.Fill(My_DataSet, tableName);  //通过SqlDataAdapter对象的Fill()方法，将数据表信息添加到DataSet对象中
            con_close();    //关闭数据库的连接
            return My_DataSet;  //返回DataSet对象的信息

            //WritePrivateProfileString(string section, string key, string val, string filePath);
        }
        #endregion
    }
}

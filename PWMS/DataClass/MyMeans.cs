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
        #region  ȫ�ֱ���
        public static string Login_ID = ""; //����ȫ�ֱ�������¼��ǰ��¼���û����
        public static string Login_Name = "";  //����ȫ�ֱ�������¼��ǰ��¼���û���
        public static string Mean_SQL = "", Mean_Table = "", Mean_Field = "";  //����ȫ�ֱ�������¼��������Ϣ���������еı�����SQL���
        public static OleDbConnection My_con;  //����һ��SqlConnection���͵Ĺ�������My_con�������ж����ݿ��Ƿ����ӳɹ�
        //public static string M_str_sqlcon = "Data Source=mrwxk\\wxk;Database=db_PWMS;User id=sa;PWD=";
        //public static string M_str_sqlcon = "Data Source=localhost;Database=db_PWMS;User id=sa;PWD=nawenyi@126.com";  //����
        public static string M_str_sqlcon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Environment.CurrentDirectory + "\\linChuang.accdb'"; 

        public static int Login_n = 0;  //�û���¼�����µ�¼�ı�ʶ
        public static string AllSql = "Select * from tb_Stuffbusic";    //�洢ְ��������Ϣ���е�SQL���
        //public static int res = 0;

        //conn_str = null;
        // ole_connection = null;
        private OleDbCommand ole_command = null;
        private OleDbDataReader ole_reader = null;
        private DataTable dt = null;


        #endregion

        #region  �������ݿ�����
        /// <summary>
        /// �������ݿ�����.
        /// </summary>
        /// <returns>����OleDbConnection����</returns>
        public static OleDbConnection getcon()
        {
            My_con = new OleDbConnection(M_str_sqlcon);   //��SqlConnection������ָ�������ݿ�������
            My_con.Open();  //�����ݿ�����
            return My_con;  //����SqlConnection�������Ϣ
        }
        #endregion

        #region  �������ݿ��Ƿ񸳼�
        /// <summary>
        /// �������ݿ��Ƿ񸳼�
        /// </summary>
        public void con_open()
        {
            getcon();
            //con_close();
        }
        #endregion

        #region  �ر����ݿ�����
        /// <summary>
        /// �ر������ݿ������.
        /// </summary>
        public void con_close()
        {
            if (My_con.State == ConnectionState.Open)   //�ж��Ƿ�������ݿ������
            {
                My_con.Close();   //�ر����ݿ������
                My_con.Dispose();   //�ͷ�My_con���������пռ�
            }
        }
        #endregion

        #region  ��ȡָ�����е���Ϣ
        /// <summary>
        /// ��ȡָ�����е���Ϣ.
        /// </summary>
        /// <param name="SQLstr">SQL���</param>
        /// <returns>����bool��</returns>
        public OleDbDataReader getcom(string SQLstr)
        {
            getcon();   //�������ݿ������
            ole_command = new OleDbCommand();
            ole_command.Connection = My_con;
            ole_command.CommandText = SQLstr;    //��ȡָ����SQL���
            ole_reader = ole_command.ExecuteReader(); //ִ��SQL�����䣬����һ��SqlDataReader����
            return ole_reader;
        }
        #endregion

        #region ִ��SqlCommand����
        /// <summary>
        /// ִ��SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        public void getsqlcom(string SQLstr)
        {
            getcon();   //�������ݿ������
            
            //����һ��SqlCommand��������ִ��SQL���
            ole_command = new OleDbCommand();
            ole_command.Connection = My_con;
            ole_command.CommandText = SQLstr;
            
            ole_command.ExecuteNonQuery();   //ִ��SQL���
            ole_command.Dispose();   //�ͷ����пռ�
            con_close();    //����con_close()�������ر������ݿ������
        }
        #endregion

        #region  ����DataSet����
        /// <summary>
        /// ����һ��DataSet����
        /// </summary>
        /// <param name="M_str_sqlstr">SQL���</param>
        /// <param name="M_str_table">����</param>
        /// <returns>����DataSet����</returns>
        public DataSet getDataSet(string SQLstr, string tableName)
        {
            getcon();   //�������ݿ������
            OleDbDataAdapter SQLda = new OleDbDataAdapter(SQLstr, My_con);  //����һ��SqlDataAdapter���󣬲���ȡָ�����ݱ����Ϣ
            DataSet My_DataSet = new DataSet(); //����DataSet����
            SQLda.Fill(My_DataSet, tableName);  //ͨ��SqlDataAdapter�����Fill()�����������ݱ���Ϣ��ӵ�DataSet������
            con_close();    //�ر����ݿ������
            return My_DataSet;  //����DataSet�������Ϣ

            //WritePrivateProfileString(string section, string key, string val, string filePath);
        }
        #endregion
    }
}

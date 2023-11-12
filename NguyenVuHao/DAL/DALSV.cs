using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NguyenVuHao.BO;
using System.IO;

namespace NguyenVuHao.DAL
{
    public class DALSV
    {
        private SqlConnection sqlConnection;
        private string connectionString = "Data Source=DESKTOP-2QL4GL9\\SQLEXPRESS;Initial Catalog=QUANLYSV;User ID=sa;Password=sa";
        List<SinhVien> sinhViens = new List<SinhVien>();


        public void KetNoi()
        {
            sqlConnection = new SqlConnection(connectionString);
            try
            {

                sqlConnection.Open();
                MessageBox.Show("Kết nối thành công!");
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
            finally
            {

                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
       


        public List<SinhVien> GetAll()
        {
            List<SinhVien> sinhViens = new List<SinhVien>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MaSV, SDT, TenSV, Email, Avatar FROM SinhVien";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SinhVien sv = new SinhVien
                            {
                                MaSV = reader.GetInt32(0),
                                SDT = reader.GetInt32(1),
                                Ten = reader.GetString(2),
                                Email = reader.GetString(3),
                                Avatar = (byte[])reader[4]
                        };
                            sinhViens.Add(sv);
                        }
                    }
                }
            }

            return sinhViens;
        }
        public SinhVien Get(int masv)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MaSV, SDT, TenSV, Email, Avatar FROM SinhVien WHERE Masv = "+masv.ToString();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SinhVien sv = new SinhVien
                            {
                                MaSV = reader.GetInt32(0),
                                SDT = reader.GetInt32(1),
                                Ten = reader.GetString(2),
                                Email = reader.GetString(3),
                                Avatar = (byte[])reader[4]
                            };
                          return sv;
                        }
                    }
                }
            }
            return null;
        }
        public void DeleteSV(int maSV)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM SinhVien WHERE MaSV = @MaSV";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void CreateSV(SinhVien sv)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO SinhVien (MASV,SDT, TenSV, Email,Avatar) VALUES (@MASV,@SDT, @TenSV, @Email,@Avatar)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MASV", sv.MaSV);
                    cmd.Parameters.AddWithValue("@SDT", sv.SDT);
                    cmd.Parameters.AddWithValue("@TenSV", sv.Ten);
                    cmd.Parameters.AddWithValue("@Email", sv.Email);
                    cmd.Parameters.AddWithValue("@Avatar", sv.Avatar);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateSV(SinhVien sv)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE SinhVien SET MaSV=@MaSV , SDT=@SDT,TenSV=@TenSV,Email=@Email, Avatar=@Avatar WHERE MaSV=@MaSV";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
                    cmd.Parameters.AddWithValue("@SDT", sv.SDT);
                    cmd.Parameters.AddWithValue("@TenSV", sv.Ten);
                    cmd.Parameters.AddWithValue("@Email", sv.Email);
                    cmd.Parameters.AddWithValue("@Avatar", sv.Avatar);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        
    }
}
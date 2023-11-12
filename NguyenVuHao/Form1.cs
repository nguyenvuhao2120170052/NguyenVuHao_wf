using NguyenVuHao.BLL;
using NguyenVuHao.BO;
using NguyenVuHao.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static NguyenVuHao.DAL.DALSV;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace NguyenVuHao
{
    public partial class Form1 : Form
    {
        BLLSV bLLSV = new BLLSV();
        
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
       
        public Form1()
        {
            InitializeComponent();
            loadsv();
            DataGridViewImageColumn pic = new DataGridViewImageColumn();
            pic = (DataGridViewImageColumn)dataGridView1.Columns[4];
            pic.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        
        private void loadsv()
        {
            dataGridView1.Rows.Clear();
            List<SinhVien> sinhViens = bLLSV.GetAll();

            foreach (SinhVien sv in sinhViens)
            {   
                dataGridView1.Rows.Add(sv.MaSV, sv.Ten, sv.SDT, sv.Email, sv.Avatar);
            }
            
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            bLLSV.KetNoi();
        }
        private byte[] ConvertImageToByteArray(PictureBox pictureBox)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                pictureBox.Image.Save(ms, pictureBox.Image.RawFormat);
                return ms.ToArray();
            }
        }
        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
        private void UploadImage()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
              
                openFileDialog.InitialDirectory = "D:\\NguyenVuHao";

            
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp|All Files (*.*)|*.*";

              
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {   
                        LoadImageToPictureBox(openFileDialog.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {                  
                        MessageBox.Show("Lỗi khi đọc hình ảnh: " + ex.Message);
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (bLLSV.Get(Convert.ToInt32(textMASV.Text)) != null)
            {
                MessageBox.Show("trùng id.");
                return;
            }
            byte[] imageBytes = ConvertImageToByteArray(pictureBox1);
          
            if (imageBytes != null && imageBytes.Length > 0)
            {
                SinhVien sv = new SinhVien
                {
                    MaSV = Convert.ToInt32(textMASV.Text),
                    SDT = Convert.ToInt32(textSDT.Text),
                    Ten = textTEN.Text,
                    Email = textEMAIL.Text,
                    Avatar = imageBytes
                };
                bLLSV.CreateSV(sv);
                dataGridView1.Rows.Add(sv.MaSV, sv.Ten, sv.SDT, sv.Email, sv.Avatar);
            }
            else
            {
               
                MessageBox.Show("Vui lòng chọn một hình ảnh.");
            }

           

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void btnsua_Click(object sender, EventArgs e)
        {


            byte[] imageBytes = ConvertImageToByteArray(pictureBox1);
            
            if (imageBytes != null && imageBytes.Length > 0)
            {
                SinhVien sv = new SinhVien
                {
                    MaSV = Convert.ToInt32(textMASV.Text),
                    SDT = Convert.ToInt32(textSDT.Text),
                    Ten = textTEN.Text,
                    Email = textEMAIL.Text,
                    Avatar = imageBytes
                };

                DialogResult h = MessageBox.Show
                  ("Bạn có chắc muốn sửa MaSV: " + sv.MaSV, "Error", MessageBoxButtons.OKCancel);
                if (h == DialogResult.OK)

                {
                    bLLSV.UpdateSV(sv); 
                    loadsv();
                   
                }
                
            }
            else
            {               
                MessageBox.Show("Vui lòng chọn một hình ảnh.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            
            
        }

      

        private void textMASV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("mssv vui lòng nhập số ", "Thông Báo ");
            }
        }

        private void textTEN_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Họ tên phải là kí tự chữ ", "Thông Báo ");
            }
        }

        private void textSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("sdt vui lòng nhập số ", "Thông Báo ");
            }
        }

        private void textEMAIL_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >= 0)
            {
                
                int maSV = int.Parse(textMASV.Text);
                
                DialogResult h = MessageBox.Show
                ("Bạn có chắc muốn xóa không?", "Thông Báo", MessageBoxButtons.OKCancel);
                if (h == DialogResult.OK)
                {
                    bLLSV.DeleteSV(maSV);
                }
                loadsv();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.");
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show
               ("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.OKCancel);
            if (h == DialogResult.OK)
                Application.Exit();
        }
        private void LoadImageToPictureBox(string imagePath)
        {
            try
            {
           
                if (File.Exists(imagePath))
                {
                
                    using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(fs);
                        
                    }
                }
                else
                {
              
                    MessageBox.Show("Tệp tin không tồn tại.");
                }
            }
            catch (Exception ex)
            {
        
                MessageBox.Show("Lỗi khi đọc hình ảnh: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            UploadImage();
        }

        private void textMASV_TextChanged(object sender, EventArgs e)
        {

        }
       
       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         
            











            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.RowIndex < dataGridView1.Rows.Count - 1)   
                {
                    textMASV.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    textTEN.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textSDT.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textEMAIL.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    object cellValue = dataGridView1.Rows[e.RowIndex].Cells[4].Value;

                    Image image = ByteArrayToImage((byte[])cellValue);
                    pictureBox1.Image = image;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một dòng hợp lệ.");
                }
            }

        }

        private void textTEN_MouseHover(object sender, EventArgs e)
        {
            if (textTEN.Text.Contains(" ")|| textEMAIL.Text.Contains(" "))
            {
                textTEN.Text = textTEN.Text.Replace(" ", "");
                textTEN.Select(textTEN.Text.Length, 0);
                textEMAIL.Text = textEMAIL.Text.Replace(" ", "");
            }
        }

        private void textEMAIL_MouseHover(object sender, EventArgs e)
        {
            if( textEMAIL.Text.Contains(" "))
            {
               
                textEMAIL.Text = textEMAIL.Text.Replace(" ", "");
                textEMAIL.Select(textEMAIL.Text.Length, 0);
            }
        }

        private void textEMAIL_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

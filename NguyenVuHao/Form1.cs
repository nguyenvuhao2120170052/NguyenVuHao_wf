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
namespace NguyenVuHao
{
    public partial class Form1 : Form
    {
        BLLSV bLLSV = new BLLSV();

        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        string filePath = "D:\\NguyenVuHao\\NguyenVuHao";
        public Form1()
        {
            InitializeComponent();
            List<SinhVien> sinhViens = bLLSV.GetAll();


            foreach (SinhVien sv in sinhViens)
            {
                dataGridView1.Rows.Add(sv.MaSV, sv.Ten, sv.SDT, sv.Email, Image.FromFile(filePath+ sv.Avatar));
            }
            
        }
       private void init()
        {
            List<SinhVien> sinhViens = bLLSV.GetAll();

            dataGridView1.DataSource = null;
         
            foreach (SinhVien sv in sinhViens)
            {
                dataGridView1.Rows.Add(sv.MaSV, sv.Ten, sv.SDT, sv.Email, Image.FromFile(filePath + sv.Avatar));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bLLSV.KetNoi();


        }
        private string UploadImage()
        {
            try
            {
                string filename = System.IO.Path.GetRandomFileName()+".jpg";
                if (filename == null)
                {
                    MessageBox.Show("Vui lòng chọn ảnh.");
                }
                string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                System.IO.File.Copy(openFileDialog1.FileName, path + "\\Image\\" + filename);
                return filename;
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn ảnh.");
                return null;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (bLLSV.Get(Convert.ToInt32(textMASV.Text)) != null)
            {
                MessageBox.Show("trugn id.");
                return;
            }

            //string fileName = UploadImage();
            SinhVien sv = new SinhVien
            {
                MaSV = Convert.ToInt32(textMASV.Text),
                SDT = Convert.ToInt32(textSDT.Text),
                Ten = textTEN.Text,

                Email = textEMAIL.Text,
                //Avatar = "\\Image\\" + fileName
                Avatar = "\\Image\\jclk51zi.eg0.jpg"
            };

            bLLSV.CreateSV(sv);

            dataGridView1.Rows.Add(sv.MaSV, sv.Ten, sv.SDT, sv.Email);


        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
           
          
            SinhVien sv = new SinhVien
            {
                MaSV = Convert.ToInt32(textMASV.Text),
                SDT = Convert.ToInt32(textSDT.Text),
                Ten = textTEN.Text,
                Email = textEMAIL.Text,
                Avatar = "\\Image\\jclk51zi.eg0.jpg"

            };


            bLLSV.UpdateSV(sv);
            MessageBox.Show("sửa thành công.");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            textMASV.Text = dataGridView1.Rows[idx].Cells[0].Value.ToString();
            textTEN.Text = dataGridView1.Rows[idx].Cells[1].Value.ToString();
            textSDT.Text = dataGridView1.Rows[idx].Cells[2].Value.ToString();
            textEMAIL.Text = dataGridView1.Rows[idx].Cells[3].Value.ToString();
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                int maSV = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                MessageBox.Show("bạn có muốn xóa không");
                bLLSV.DeleteSV(maSV);
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
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

        private void button3_Click(object sender, EventArgs e)
        {
         
            openFileDialog1.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog1.Title = "Select image to be upload.";
            //which type image format you want to upload in database. just add them.
            //openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label1.Text = path;
                        pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn ảnh.");
                }
            }
            catch (Exception ex)
            {
                //it will give if file is already exits..
                MessageBox.Show(ex.Message);
            }
        }

        private void textMASV_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

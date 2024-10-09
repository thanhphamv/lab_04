using BaiTapBuoi5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaiTapBuoi5.Models;


namespace BaiTapBuoi5
{
    public partial class Form1 : Form
    {
        Model1 model1 = new Model1();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Student> listStudent = model1.Student.ToList();
            List<Faculty> listFaculty = model1.Faculty.ToList();
            fillDGVSV(listStudent);
            fillCBBKHOA(listFaculty);
        }

        private void fillCBBKHOA(List<Faculty> listFaculty)
        {
            cbbkhoa.DataSource = listFaculty;
            //đặt tên hiển thị
            cbbkhoa.DisplayMember = "FacultyName";
            //đặt tên xử lí
            cbbkhoa.SelectedItem = "FacultyID";
        }

        private void fillDGVSV(List<Student> listStudent)
        {
            dgvSV.Rows.Clear();//Xóa trắng bảng
            foreach (Student student in listStudent)
            {
                int newRows = dgvSV.Rows.Add();
                dgvSV.Rows[newRows].Cells[0].Value = student.StudentID;
                dgvSV.Rows[newRows].Cells[1].Value = student.FullName;
                dgvSV.Rows[newRows].Cells[2].Value = student.Faculty.FacultyName;
                dgvSV.Rows[newRows].Cells[3].Value = student.AverageScore;

            }
        }

        private void dgvSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvSV.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    txtmssv.Text = dgvSV.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txthoten.Text = dgvSV.Rows[e.RowIndex].Cells[1].Value.ToString();
                    cbbkhoa.Text = dgvSV.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtdtb.Text = dgvSV.Rows[e.RowIndex].Cells[3].Value.ToString();
                }
            }
        }

        private void bttthem_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng sinh viên mới từ các thông tin nhập
            Student newStudent = new Student()
            {
                StudentID = txtmssv.Text,
                FullName = txthoten.Text,
                FacultyID = (cbbkhoa.SelectedItem as Faculty).FacultyID,
                AverageScore = float.Parse(txtdtb.Text)
            };

            // Thêm vào database context
            model1.Student.Add(newStudent);

            // Lưu thay đổi vào cơ sở dữ liệu
            model1.SaveChanges();

            // Cập nhật lại danh sách sinh viên trên giao diện
            fillDGVSV(model1.Student.ToList());
        }



        private void bttsua_Click(object sender, EventArgs e)
        {
            // Tìm sinh viên theo MSSV
            string studentID = txtmssv.Text;
            Student student = model1.Student.FirstOrDefault(s => s.StudentID == studentID);

            if (student != null)
            {
                // Cập nhật thông tin sinh viên
                student.FullName = txthoten.Text;
                student.FacultyID = (cbbkhoa.SelectedItem as Faculty).FacultyID;
                student.AverageScore = float.Parse(txtdtb.Text);

                // Lưu thay đổi vào cơ sở dữ liệu
                model1.SaveChanges();

                // Cập nhật lại danh sách sinh viên trên giao diện
                fillDGVSV(model1.Student.ToList());
            }
        }
            private void bttxoa_Click(object sender, EventArgs e)
            {
                // Tìm sinh viên theo MSSV
                string studentID = txtmssv.Text;
                Student student = model1.Student.FirstOrDefault(s => s.StudentID == studentID);

                if (student != null)
                {
                    // Xóa sinh viên khỏi database
                    model1.Student.Remove(student);

                    // Lưu thay đổi vào cơ sở dữ liệu
                    model1.SaveChanges();

                    // Cập nhật lại danh sách sinh viên trên giao diện
                    fillDGVSV(model1.Student.ToList());
                }
            }

        private void bttthoat_Click(object sender, EventArgs e)
        {
            // Hiển thị một thông báo xác nhận trước khi thoát
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Thoát ứng dụng
                Application.Exit();
            }
            }
    }
   }


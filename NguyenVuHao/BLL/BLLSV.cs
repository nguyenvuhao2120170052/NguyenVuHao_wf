using NguyenVuHao.BO;
using NguyenVuHao.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NguyenVuHao.BLL
{
    public class BLLSV : DALSV
    {
        
        private DALSV dal = new DALSV();
        public List<SinhVien> getall()
        {
            return dal.GetAll();
        }
        
        public void DeleteSV(int MaSV)
        {
            dal.DeleteSV(MaSV);
        }

        public void CreatSV(SinhVien sv)
        {
            dal.CreateSV(sv);
        }
    }
}

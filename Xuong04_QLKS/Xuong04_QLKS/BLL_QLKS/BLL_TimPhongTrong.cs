using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLKS;
using DTO_QLKS;

namespace BLL_QLKS
{
    public class BLL_TimPhongTrong
    {
        private readonly DAL_TimPhongTrong dal = new DAL_TimPhongTrong();

        public List<TimPhongTrong> LayPhongTrong()
        {
            return dal.LayDanhSachPhongTrong();
        }

        public List<TimPhongTrong> TimPhongTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            return dal.TimPhongTheoNgay(tuNgay, denNgay);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using DTO_QLKS;
using Microsoft.Data.SqlClient;

namespace DAL_QLKS
{
    public class DALQLDichVu : IDALQLDichVu
    {
        public List<DichVu> SelectBySql(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text)
        {
            List<DichVu> list = new List<DichVu>();
            try
            {
                DataTable table = DBUtil.Query(sql, args);
                foreach (DataRow row in table.Rows)
                {
                    DichVu entity = new DichVu();
                    entity.DichVuID = row["DichVuID"].ToString();
                    entity.HoaDonThueID = row["HoaDonThueID"].ToString();
                    entity.NgayTao = Convert.ToDateTime(row["NgayTao"]);
                    entity.TrangThai = Convert.ToBoolean(row["TrangThai"]);
                    entity.GhiChu = row["GhiChu"].ToString();
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<DichVu> selectAll()
        {
            string sql = "SELECT * FROM DichVu";
            return SelectBySql(sql, null);
        }

        public DichVu selectById(string id)
        {
            string sql = "SELECT * FROM DichVu WHERE DichVuID = @0";
            var thamSo = new Dictionary<string, object> { { "@0", id } };
            List<DichVu> list = SelectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }

        public void insertDichVu(DichVu dv)
        {
            try
            {
                string sql = @"INSERT INTO DichVu (DichVuID, HoaDonThueID, NgayTao, TrangThai, GhiChu) 
                               VALUES (@0, @1, @2, @3, @4)";
                var thamSo = new Dictionary<string, object>
                {
                    { "@0", dv.DichVuID },
                    { "@1", dv.HoaDonThueID },
                    { "@2", dv.NgayTao },
                    { "@3", dv.TrangThai },
                    { "@4", dv.GhiChu }
                };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void updateDichVu(DichVu dv)
        {
            try
            {
                string sql = @"UPDATE DichVu
                               SET HoaDonThueID = @1, NgayTao = @2, TrangThai = @3, GhiChu = @4
                               WHERE DichVuID = @0";
                var thamSo = new Dictionary<string, object>
                {
                    { "@0", dv.DichVuID },
                    { "@1", dv.HoaDonThueID },
                    { "@2", dv.NgayTao },
                    { "@3", dv.TrangThai },
                    { "@4", dv.GhiChu }
                };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void deleteDichVu(string DichVuID)
        {
            try
            {
                string sql = "DELETE FROM DichVu WHERE DichVuID = @0";
                var thamSo = new Dictionary<string, object> { { "@0", DichVuID } };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public string generateDichVuID()
        //{
        //    string prefix = "DVHD";
        //    string sql = "SELECT MAX(CAST(SUBSTRING(DichVuID, 5, LEN(DichVuID)) AS INT)) FROM DichVu";
        //    object result = DBUtil.ScalarQuery(sql, null);

        //    if (result != null && result != DBNull.Value)
        //    {
        //        int maxNumber = Convert.ToInt32(result);
        //        return $"{prefix}{(maxNumber + 1):D3}";
        //    }

        //    return $"{prefix}001"; // nếu chưa có mã nào
        //}
        public string GenerateNextMaDichVuID()
        {
            string prefix = "DVHD";
            string sql = "SELECT DichVuID FROM DichVu";
            DataTable table = DBUtil.Query(sql, null);

            List<int> existingNumbers = new List<int>();
            foreach (DataRow row in table.Rows)
            {
                string id = row["DichVuID"].ToString();
                if (id.StartsWith(prefix))
                {
                    if (int.TryParse(id.Substring(prefix.Length), out int number))
                    {
                        existingNumbers.Add(number);
                    }
                }
            }

            existingNumbers.Sort();

            int nextNumber = 1;
            foreach (int number in existingNumbers)
            {
                if (number == nextNumber)
                    nextNumber++;
                else if (number > nextNumber)
                    break;
            }

            return $"{prefix}{nextNumber:D3}";
        }



    }
}

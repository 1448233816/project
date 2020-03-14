using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseDal
    {

        /// <summary>
        /// 约束是为了正确的调用，才能int id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public T Find<T>(int id) where T : BaseModel
        {

            Type type = typeof(T);

            //List<SqlParameter> sql = new List<SqlParameter>();
            //foreach (var item in type.GetProperties())
            //{
            //    sql.Add(new SqlParameter($"@{item.Name}", item.Name));
            //}

            ////获取每一行列名称并用逗号分隔
            string columString = string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"));
            string FindTByID = $"select {columString} from [{type.Name}] where id = {id}";
            T t = (T)Activator.CreateInstance(type);
            List<T> listOne = new List<T>();
            using (SqlConnection con = new SqlConnection(StaticConstant.SqlConfig))
            {
                SqlCommand cmd = new SqlCommand(FindTByID, con);
                con.Open();
                //cmd.Parameters.AddRange(sql.ToArray());
                SqlDataReader reader = cmd.ExecuteReader();
                listOne = ReaderToList<T>(reader);
                t = listOne.FirstOrDefault();
                con.Close();
            }
            return t;
        }

        public List<T> FindAll<T>() where T : BaseModel
        {

            Type type = typeof(T);

            //List<SqlParameter> sql = new List<SqlParameter>();
            //foreach (var item in type.GetProperties())
            //{
            //    sql.Add(new SqlParameter($"@{item.Name}", item.Name));
            //}

            ////获取每一行列名称并用逗号分隔
            string columString = string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"));
            string findAll = $"select {columString} from [{type.Name}]";
            List<T> listAll = new List<T>();

            using (SqlConnection con = new SqlConnection(StaticConstant.SqlConfig))
            {
                SqlCommand cmd = new SqlCommand(findAll, con);
                con.Open();
                //cmd.Parameters.AddRange(sql.ToArray());
                SqlDataReader reader = cmd.ExecuteReader();

                //while (reader.Read())//表示有数据开始读
                //{
                //    T t = (T)Activator.CreateInstance(type);
                //    foreach (var prop in type.GetProperties())
                //    {
                //        prop.SetValue(t, reader[prop.Name] is DBNull ? null : reader[prop.Name]);
                //    }
                //    list.Add(t);
                //}
                listAll = ReaderToList<T>(reader);
                con.Close();

            }

            return listAll;

        }

        #region privateMethod
        private List<T> ReaderToList<T>(SqlDataReader reader) where T : BaseModel
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            while (reader.Read())//表示有数据开始读
            {
                T t = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    prop.SetValue(t, reader[prop.Name] is DBNull ? null : reader[prop.Name]);
                }
                list.Add(t);
            }
            return list;
        }
        #endregion


    }
}

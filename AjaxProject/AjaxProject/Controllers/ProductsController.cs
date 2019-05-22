using AjaxProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace AjaxProject.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult List(int pagesize=4,int pageindex=1)//列表页（视图）
        {
            //控制器-动作方法（不管业务逻辑）
            //通过传入参数，调用业务逻辑层方法后去分页信息
            //session,viewdata,viewbag,application,tempdata
            ViewBag.ProList=  GetPager(pagesize, pageindex);
            return View();
        }

        #region 业务逻辑层

        /// <summary>
        /// 产品业务逻辑层
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        private Pager<Products> GetPager(int pagesize, int pageindex)
        {
            //查询数据库
            //1。查询一共多少条数据
            //2。显示数据
            List<Products> list = GetProductsList(pagesize, pageindex);
            Pager<Products> pager = new Pager<Products>(pageindex,pagesize,GetCount(),list);
            return pager;

        }

        #endregion


        #region 数据访问层

        private int GetCount()
        {
            string sql = "select count(1) from Products";
            object obj = DBHelper.ExecuteScalar(sql);
            return (int)obj;
        }
        /// <summary>
        /// 根据页码大小查询并返回
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        private List<Products> GetProductsList(int pagesize, int pageindex)
        {
            List<Products> list = new List<Products>();
            string sql = @"select*from(select ROW_NUMBER()over(order by productid asc) as Id,
                         *from Products) Products where id between @start and @end";

            int start, end;
            start = (pageindex - 1) * pagesize + 1;
            end = pageindex * pagesize;

            SqlParameter[] paras = {
                new SqlParameter("@start",start),
                new SqlParameter("@end",end)
            };
            using (SqlDataReader reader = DBHelper.ExecuteReader(sql, paras))
            {
                while (reader.Read())
                {
                    Products pro = new Products();
                    pro.ProductId = (int)reader["productId"];
                    pro.TypeId = (int)reader["typeId"];
                    pro.ProductName = reader["productName"].ToString();
                    pro.Price =Convert.ToSingle( reader["price"]);
                    pro.Descripiton = reader["Descripiton"].ToString();
                    list.Add(pro);
                }

            }
            return list;
        }


        #endregion


    }
}
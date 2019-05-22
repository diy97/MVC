using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using AjaxProject.Models;

namespace AjaxProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUser(string username) //第一种获取方式
        {
            // Request["username"]       第二种获取方式
            // RouteData.Values["username"]  第三种获取方式

            JsonResult jr = new JsonResult();
            jr.Data = GetUserByName(username);//泛型集合
            //json有规则字符串中转
            //json字符串{键值对形式}
            //[{id:"123",name:"李四"},{id:"124",name:"zs"}] json数组

            return jr;//json类型
        }

        //模拟数据访问层
        private List<UserInfo> GetUserByName(string username)
        {
            List<UserInfo> list = new List<UserInfo>();
            string sql = @"select productId,productName from Products where productName like @productName";
            SqlParameter[] paras = {
                new SqlParameter("@productName",$"{username}%")
            };

            //SqlDataReader 连接式读取方式，占用Connection一直保持连接,执行效率高，但会占用连接对象，不能关闭.
            //SqlDataAdapter 适配器 操作时可以关闭Connection

            using (SqlDataReader reader = DBHelper.ExecuteReader(sql, paras))
            {
                while (reader.Read()) //游标读取
                {
                    UserInfo ui = new UserInfo();
                    ui.UserId = (int)reader["productId"];
                    ui.UserName = reader["productName"].ToString();
                    list.Add(ui);
                }

            }
            return list;

        }
    }
}
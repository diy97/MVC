using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjaxProject.Models
{
    /// <summary>
    /// 通用分页类，T点位符
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pager<T>
    {
        //类的成员 有三种：字段、属性、方法

        //当前页码：PageIndex
        // 当前显示条数：PageSize
        //总条数：DataCount
        //总页数：PageCount(总条数和每页显示条数计算出来)
        //存放数据集合：List<Products>

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int DataCount { get; set; }
        public int PageCount { get; set; }

        public List<T> InfoList { get; set; }

        //无参构造方法
        public Pager() { }
        //有参构造方法
        public Pager(int pageIndex, int pageSize, int dataCount, List<T> list)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.DataCount = dataCount;
            this.PageCount = dataCount / pageSize;
            if (dataCount % pageSize != 0)
            {
                this.PageCount++;
            }
            this.InfoList = list;
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using DoVanSang_5951071091_asp_mvc.Models;

namespace DoVanSang_5951071091_asp_mvc.Controllers
{
    public class HomeController : Controller
    {
        db dbop = new db();
        string msg;

        public IActionResult Index()
        {
            Employee emp = new Employee();
            emp.Flag = "get";
            DataSet ds = dbop.Empget(emp, out msg);
            List<Employee> list = new List<Employee>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new Employee
                {
                    Sr_no = Convert.ToInt32(dr["Sr_no"]),
                    Emp_name = dr["Emp_name"].ToString(),
                    City = dr["City"].ToString(),
                    State = dr["State"].ToString(),
                    Country = dr["Country"].ToString(),
                    Department = dr["Department"].ToString()
                });
                 
            }
            return View(list);
        }

        public IActionResult Create([Bind] Employee emp)
        {
            try
            {
                emp.Flag = "insert";
                dbop.Empdml(emp, out msg);
                TempData["msg"] = msg;
            }
            catch(Exception e)
            {
                TempData["msg"] = e.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Employee emp = new Employee();
            emp.Sr_no = id;
            DataSet ds = dbop.Empget(emp, out msg);
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                emp.Sr_no = Convert.ToInt32(dr["Sr_no"]);
                emp.Emp_name = (dr["Emp_name"].ToString());
                emp.City = (dr["City"].ToString());
                emp.State = (dr["State"].ToString());
                emp.Country = (dr["Country"].ToString());
                emp.Department = (dr["Department"].ToString());
            }
            return View(emp);
        }

        public IActionResult Update(int id, [Bind] Employee emp)
        {
            try
            {
                emp.Sr_no = id;
                emp.Flag = "update";
                dbop.Empdml(emp, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception e)
            {
                TempData["msg"] = e.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id, [Bind] Employee emp)
        {
            try
            {
                emp.Sr_no = id;
                emp.Flag = "delete";
                dbop.Empdml(emp, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception e)
            {
                TempData["msg"] = e.Message;
            }
            return RedirectToAction("Index");
        }
    }
}

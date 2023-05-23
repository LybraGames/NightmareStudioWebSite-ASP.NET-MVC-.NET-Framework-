using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Model;
using Account = Model.Account;

//ADO.NET library
using System.Data;
using System.Data.SqlClient;
using DataTable = System.Data.DataTable;

namespace NightmareStudioWebSite_ASP.NET_MVC_.NET_Framework_.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            Account.loginError = false;
            return View("Home");
        }

        public ActionResult Logout() 
        {
            Account.isLogin = false;
            Account.username = null;
            Account.password = null;
            Account.email = null;

            return View("Home");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        public ActionResult UserProfile() 
        {
            return View("UserProfile");
        }

        public ActionResult EditProfile() 
        {
            return View("EditProfile");
        }

        public ActionResult DeleteProfile()
        {
            int accountIndex = -1;

            for (int i = 0; i < SQL_local_db.rowsNumber; i++)
            {
                accountIndex++;

                if (Account.username == SQL_local_db.dataTable.Rows[i].ItemArray[1].ToString())
                    break;
            }

            SQL_local_db.dataTable.Rows.RemoveAt(accountIndex);

            SQL_local_db.rowsNumber--;

            for (int i = 0; i < SQL_local_db.rowsNumber; i++)
            {
                object[] itemArray = new object[4] { i + 1, SQL_local_db.dataTable.Rows[i].ItemArray[1], SQL_local_db.dataTable.Rows[i].ItemArray[2],
                                    SQL_local_db.dataTable.Rows[i].ItemArray[3] };

                SQL_local_db.dataTable.Rows[i].ItemArray = itemArray;
            }

            SQL_local_db.sqlCommand.CommandText = "delete from DataTable";
            SQL_local_db.sqlCommand.ExecuteNonQuery();

            string tableColumns = null;
            string tableValues = null;

            for (int i = 0; i < SQL_local_db.columnsNumber; i++)
            {
                tableColumns += SQL_local_db.columnNames[i] + ",";
                tableValues += "'{" + i + "}',";
            }

            tableColumns = tableColumns.Remove(tableColumns.Length - 1, 1);
            tableValues = tableValues.Remove(tableValues.Length - 1, 1);


            for (int i = 0; i < SQL_local_db.dataTable.Rows.Count; i++)
            {
                SQL_local_db.sqlCommand.CommandText = string.Format("insert into DataTable (" + tableColumns + ") values (" + tableValues + ")", SQL_local_db.dataTable.Rows[i].ItemArray);

                SQL_local_db.sqlCommand.ExecuteNonQuery();
            }

            Account.username = null;
            Account.password = null;
            Account.email = null;
            Account.isLogin = false;
            return View("Home");
        }

        //HttpPost

        [HttpPost]
        public ActionResult SaveProfile(Account user) 
        {
            int accountIndex = -1;
            bool email = true;
            bool username = true;

            for (int i = 0; i < SQL_local_db.rowsNumber; i++)
            {
                accountIndex++;
                if (Account.username == SQL_local_db.dataTable.Rows[i].ItemArray[1].ToString())
                    break;
            }


            for (int i = 0; i < SQL_local_db.rowsNumber; i++)
            {
                if (user.username_ != null) 
                {
                    if (user.username_ == SQL_local_db.dataTable.Rows[i].ItemArray[1].ToString())
                    {
                        username = false;
                    }
                }

                if (user.email_ != null)
                {
                    if (user.email_ == SQL_local_db.dataTable.Rows[i].ItemArray[2].ToString() || !user.email_.Contains("@") || !user.email_.Contains("."))
                    {
                        email = false;
                    }
                }
            }

            Account.isValid = (username && email);

            if (Account.isValid)
            {
                Account.username = (user.username_ != null) ? user.username_ : Account.username;
                Account.email = (user.email_ != null) ? user.email_ : Account.email;
                Account.password = (user.password_ != null) ? user.password_ : Account.password;

                SQL_local_db.dataTable.Rows[accountIndex].ItemArray = new object[4] { accountIndex + 1, Account.username, Account.email, Account.password };

                SQL_local_db.sqlCommand.CommandText = "delete from DataTable";
                SQL_local_db.sqlCommand.ExecuteNonQuery();

                string tableColumns = null;
                string tableValues = null;

                for (int i = 0; i < SQL_local_db.columnsNumber; i++)
                {
                    tableColumns += SQL_local_db.columnNames[i] + ",";
                    tableValues += "'{" + i + "}',";
                }

                tableColumns = tableColumns.Remove(tableColumns.Length - 1, 1);
                tableValues = tableValues.Remove(tableValues.Length - 1, 1);


                for (int i = 0; i < SQL_local_db.dataTable.Rows.Count; i++)
                {
                    SQL_local_db.sqlCommand.CommandText = string.Format("insert into DataTable (" + tableColumns + ") values (" + tableValues + ")", SQL_local_db.dataTable.Rows[i].ItemArray);

                    SQL_local_db.sqlCommand.ExecuteNonQuery();
                }

                Account.editProfileError = false;
                Account.isValid = false;
                return View("UserProfile");
            }
            else
            {
                Account.editProfileError = true;
                return View("EditProfile");
            }
        }

        [HttpPost]
        public ActionResult Loging(Account user)
        {
            for (int i = 0; i < SQL_local_db.rowsNumber; i++)
            {
                if (user.username_ == SQL_local_db.dataTable.Rows[i].ItemArray[1].ToString())
                {
                    if (user.password_ == SQL_local_db.dataTable.Rows[i].ItemArray[3].ToString())
                    {
                        Account.isValid = true;
                        Account.email = SQL_local_db.dataTable.Rows[i].ItemArray[2].ToString();
                        Account.isPremium = bool.Parse(SQL_local_db.dataTable.Rows[i].ItemArray[4].ToString());
                    }
                }
            }


            if (Account.isValid)
            {
                Account.username = user.username_;
                Account.password = user.password_;
                Account.isLogin = true;
                Account.isValid = false;
                Account.loginError = false;
                return View("Home");
            }
            else
            {
                Account.loginError = true;
                return View("Login");
            }
        }

        [HttpPost]
        public ActionResult Registring(Account user)
        {
            bool username = true;
            bool email = true;

            for (int i = 0; i < SQL_local_db.rowsNumber; i++)
            {
                if (user.username_ == SQL_local_db.dataTable.Rows[i].ItemArray[1].ToString())
                {
                    username = false;
                }
            }

            for (int i = 0; i < SQL_local_db.rowsNumber; i++)
            {
                if (user.email_ == SQL_local_db.dataTable.Rows[i].ItemArray[2].ToString())
                {
                    email = false;
                }
            }

            if (!user.email_.Contains("@") || !user.email_.Contains(".")) 
            {
                email = false;
            }

            Account.isValid = (username && email);

            if (user.username_ == null || user.password_ == null || user.email_ == null)
                Account.isValid = false;

            if (Account.isValid)
            {
                SQL_local_db.rowsNumber++;
                DataRow dataRow = SQL_local_db.dataTable.NewRow();
                object[] itemArray = new object[5] { SQL_local_db.rowsNumber, user.username_, user.email_, user.password_,"false" };
                dataRow.ItemArray = itemArray;
                SQL_local_db.dataTable.Rows.Add(dataRow);

                SQL_local_db.sqlCommand.CommandText = "delete from DataTable";
                SQL_local_db.sqlCommand.ExecuteNonQuery();

                string tableColumns = null;
                string tableValues = null;

                for (int i = 0; i < SQL_local_db.columnsNumber; i++)
                {
                    tableColumns += SQL_local_db.columnNames[i] + ",";
                    tableValues += "'{" + i + "}',";
                }

                tableColumns = tableColumns.Remove(tableColumns.Length - 1, 1);
                tableValues = tableValues.Remove(tableValues.Length - 1, 1);


                for (int i = 0; i < SQL_local_db.dataTable.Rows.Count; i++)
                {
                    SQL_local_db.sqlCommand.CommandText = string.Format("insert into DataTable (" + tableColumns + ") values (" + tableValues + ")", SQL_local_db.dataTable.Rows[i].ItemArray);

                    SQL_local_db.sqlCommand.ExecuteNonQuery();
                }

                Account.username = user.username_;
                Account.password = user.password_;
                Account.email = user.email_;
                Account.isLogin = true;
                Account.isValid = false;
                Account.registerError = false;
                return View("Home");
            }
            else
            {
                Account.registerError = true;
                return View("Register");
            }
        }
    }
}
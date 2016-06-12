using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using statements required for EF to DB access
using lab3_departments.Models;
using System.Web.ModelBinding;



namespace lab3_departments
{
    public partial class DepartmentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetDepartment();
            }

        }

        protected void GetDepartment()
        {
            //populated the form existing data from the database
            int DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

            //connect to the EF DB
            using(DefaultConnection db = new DefaultConnection())
            {
                //populate a department object instance wit the DepartmentID from the url Parameter
                Department updatedDepartment = (from department in db.Departments
                                                where department.DepartmentID == DepartmentID
                                                select department).FirstOrDefault();
                //map the department properties to the form controls
                if(updatedDepartment != null)
                {
                    DepartmentName.Text = updatedDepartment.Name;
                    DepartmentBudget.Text = updatedDepartment.Budget.ToString();
                }
            }
        }
 
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //redirect back to departments page
            Response.Redirect("~/Departments.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
          //Ef to connect to the server
          using(DefaultConnection db = new DefaultConnection())
            {
                //use the Department models to create a new deapartment object and 
                //save a new record
                Department newDepartment = new Department();

                int DepartmentID = 0;

                if(Request.QueryString.Count >0 ) //our URL has a DepartmentID in it 
                {
                    //get the id from the url
                    DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    //get the current department from EF DB
                    newDepartment = (from department in db.Departments
                                     where department.DepartmentID == DepartmentID
                                     select department).FirstOrDefault();
                }

                //add data to the new department record
                newDepartment.Name = DepartmentName.Text;
                newDepartment.Budget = Convert.ToDecimal(DepartmentBudget.Text);

                //use LINQ to ADO.NET to add/insert departments in to DB
                if(DepartmentID == 0) { 
                db.Departments.Add(newDepartment);
                }
                //save our changes also updates the DB
                db.SaveChanges();

                //redirect back to updated departments page
                Response.Redirect("~/Departments.aspx");
            }
        }
    }
}
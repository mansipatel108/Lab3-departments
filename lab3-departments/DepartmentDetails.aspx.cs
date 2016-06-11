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
                //add data to the new department record
                newDepartment.Name = DepartmentName.Text;
                newDepartment.Budget = Convert.ToDecimal(DepartmentBudget.Text);

                //use LINQ to ADO.NET to add/insert departments in to DB
                db.Departments.Add(newDepartment);

                //save our changes
                db.SaveChanges();

                //redirect back to updated departments page
                Response.Redirect("~/Departments.aspx");
            }
        }
    }
}
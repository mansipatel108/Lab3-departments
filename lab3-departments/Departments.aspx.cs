using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using statements are required to to connect to EF DB
using lab3_departments.Models;
using System.Web.ModelBinding;

namespace lab3_departments
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading the page for the first time populate the departements grid
            if (!IsPostBack)
            {
                //get the departments data
                this.GetDepartments();
            }
        }

        /***
         * <summary>
         * this method gets the departments data 
         * </summary>
         * 
         * @method GetDepartments
         * @return {void}
         * **/

            protected void GetDepartments()
        {
            //connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                //query the studnets table usinf EF and LINQ
                var Departments = (from allDepartments in db.Departments
                                   select allDepartments);

                //bind the result in GridView
                DepartmentsGridView.DataSource = Departments.ToList();
                DepartmentsGridView.DataBind();

                
            }
        }
        
    }
}
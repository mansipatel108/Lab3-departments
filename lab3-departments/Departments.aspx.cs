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

        /// <summary>
        /// this event handler deletes the departments form the DB using EF
        /// </summary>
        /// @method: DepartmentsGridView_RowDeleting
        /// @param {object} sender
        /// @param {GridViewDeleteEventArgs} e
        /// @returns {void}
        

        protected void DepartmentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store whic row was clicked
            int selectedRow = e.RowIndex;

            //get the selected departmentI using grids data key collection
            int DepartmentID = Convert.ToInt32(DepartmentsGridView.DataKeys[selectedRow].Values["DepartmentID"]);

            //use EF to find selected department in the DB and remove it
            using(DefaultConnection db = new DefaultConnection())
            {
                //create object of department class and store the query string inside of it
                Department deletedDepartment = (from departmentRecords in db.Departments
                                                where departmentRecords.DepartmentID == DepartmentID
                                                select departmentRecords).FirstOrDefault();

                //remove the selected deartment from DB
                db.Departments.Remove(deletedDepartment);

                //save my changes to back to DB
                db.SaveChanges();

                //refresh the grid
                this.GetDepartments();
            }
        }

        /**
         * <summary>
         * this event handler allows pagination to occure for the departments page
         * </summary>
         * 
         * @method DepartmentsGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventHandlerArgs} e
         * @returns {void}
         * */
        protected void DepartmentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the new page number
            DepartmentsGridView.PageIndex = e.NewPageIndex;

            //return the grid
            this.GetDepartments();
        }
    }
}
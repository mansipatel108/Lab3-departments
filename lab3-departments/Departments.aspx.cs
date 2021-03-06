﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using statements are required to to connect to EF DB
using lab3_departments.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace lab3_departments
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading the page for the first time populate the departements grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "DepartmentID"; //default sort column
                Session["SortDirection"] = "ASC";
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
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                //query the studnets table usinf EF and LINQ
                var Departments = (from allDepartments in db.Departments
                                   select allDepartments);

                //bind the result in GridView
                DepartmentsGridView.DataSource = Departments.AsQueryable().OrderBy(SortString).ToList();
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
            //store which row was clicked
            int selectedRow = e.RowIndex;

            //get the selected departmentID using grids data key collection
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

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the new page size
            DepartmentsGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            //refresh the grid
            this.GetDepartments();
        }

        /**
         * <summary>
         * this event handler allows sorting to occure for the departments page
         * </summary>
         * 
         * @method DepartmentsGridView_Sorting
         * @param {object} sender
         * @param {GridViewSortEventHandlerArgs} e
         * @returns {void}
         * */

        protected void DepartmentsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            //refresh the grid
            this.GetDepartments();

            //toggle the diretion 
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        /**
         * <summary>
         * this event handler allows sorting on each row with indication of whether the row is sorted in ASC or DESC order
         *  for the departments page
         * </summary>
         * 
         * @method DepartmentsGridView_RowDataBound
         * @param {object} sender
         * @param {GridViewRowDataBoundEventHandlerArgs} e
         * @returns {void}
         * */

        protected void DepartmentsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if(e.Row.RowType == DataControlRowType.Header) //if header row has been clicked 
                {
                    LinkButton linkButton = new LinkButton();

                    for(int index = 0; index<DepartmentsGridView.Columns.Count -1; index++)
                    {
                        if(DepartmentsGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if(Session["SortDirection"].ToString() == "ASC")
                            {
                                linkButton.Text = "<i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkButton.Text = "<i class='fa fa-caret-down fa-lg'></i>";
                            }
                            e.Row.Cells[index].Controls.Add(linkButton);
                        }
                    }
                }
            }
        }
    }
}
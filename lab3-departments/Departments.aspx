﻿<%@ Page Title="Departments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="lab3_departments.Departments" %>

<%--
File Name: Departments.aspx
Author Name: Mansi Patel(200303640) & Shweta Chavda(200326347)
Website Name: http://departments-lab.azurewebsites.net/
Description: This page contains detalis about the Departments name and how much budget each departments have in Contoso University
 @date: June 13, 2016
 @version: 0.0.1  --%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8"></div>
            <h1>Department List</h1>
            <a href="DepartmentDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add Department</a>

            <div>
                <label for="PageSizeDropDownList">Records per Page: </label>
                <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                    AutoPostBack="true" CssClass="btn btn-default btn-sm dropdown-toggle" OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                    <asp:ListItem Text="3" Value="3" />
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="10" Value="10" />
                    <asp:ListItem Text="All" Value="1000" />
                </asp:DropDownList>
            </div>

            <asp:GridView runat="server" CssClass="table table-bordered table-stripped table-hover" ID="DepartmentsGridView" AutoGenerateColumns="false"
                DataKeyNames="DepartmentID" OnRowDeleting="DepartmentsGridView_RowDeleting" AllowPaging="true" PageSize="3"
                OnPageIndexChanging="DepartmentsGridView_PageIndexChanging"
                AllowSorting="true" OnSorting="DepartmentsGridView_Sorting" OnRowDataBound="DepartmentsGridView_RowDataBound"
                 PagerStyle-CssClass="pagination-ys">
                <Columns>
                    <asp:BoundField DataField="DepartmentID" HeaderText="Department ID" Visible="true" SortExpression="DepartmentID" />
                    <asp:BoundField DataField="Name" HeaderText="Department Name" Visible="true" SortExpression="Name" />
                    <asp:BoundField DataField="Budget" HeaderText="Department Budget" Visible="true"
                        DataFormatString="{0:c}" SortExpression="Budget" />
                    <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" 
                        NavigateUrl="~/DepartmentDetails.aspx" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server"
                         DataNavigateUrlFields="DepartmentID" DataNavigateUrlFormatString="DepartmentDetails.aspx?DepartmentID={0}"  />
                    <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete" ShowDeleteButton="true" ButtonType="Link"
                        ControlStyle-CssClass="btn btn-danger btn-sm" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>


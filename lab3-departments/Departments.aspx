<%@ Page Title="Departments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="lab3_departments.Departments" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8"></div>
            <h1>Department List</h1>
            <a href="DepartmentList.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add Department</a>
            <asp:GridView runat="server" CssClass="table table-bordered table-stripped table-hover" ID="DepartmentsGridView" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="DepartmentID" HeaderText="Department ID" Visible="true" />
                    <asp:BoundField DataField="Name" HeaderText="Department Name" Visible="true" />
                    <asp:BoundField DataField="Budget" HeaderText="Department Budget" Visible="true"
                        DataFormatString="{0:###,###.00}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>


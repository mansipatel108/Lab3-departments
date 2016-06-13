<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentDetails.aspx.cs" Inherits="lab3_departments.DepartmentDetails" %>

<%--
File Name: DepartmentDetails.aspx
Author Name: Mansi Patel(200303640) & Shweta Chavda(200326347)
Website Name: http://departments-lab.azurewebsites.net/
Description: When user want to modify details for the Departments This page will allow them to change the data .
 @date: June 13, 2016
 @version: 0.0.1  --%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h1>Department Details</h1>
                <h5>All Fields are Required </h5>
                   <br />
                    <div class="form-group">
                        <label class="control-label" for="DepartmentName">Department Name</label>
                        <asp:TextBox  runat="server" Cssclass="form-control" ID="DepartmentName" placeholder="Department Name" required="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="DepartmentBudget">Department Budget</label>
                        <asp:TextBox  runat="server" Cssclass="form-control" ID="DepartmentBudget" placeholder="Department Budget" required="true"></asp:TextBox>
                    </div>
                    <div class="text-right">
                        <asp:Button Text="Cancel" ID="CancelButton" CssClass="btn btn-warning btn-lg" runat="server" UseSubmitBehavior="false" CausesValidation="false" OnClick="CancelButton_Click" />
                        <asp:Button Text="Save" ID="SaveButton" CssClass="btn btn-primary btn-lg" runat="server" OnClick="SaveButton_Click"  />
                    </div>
                </h5>
            </div>
        </div>
    </div>
</asp:Content>

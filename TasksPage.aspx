<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedUsers.Master" AutoEventWireup="true" CodeBehind="TasksPage.aspx.cs" Inherits="TasksTrackerWebApp.TasksPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
     <style type="text/css">
     
        #<%= cblStatus.ClientID %> .form-check-inline {
            margin-right: 50px; 
        }

        .checkbox-container {
            text-align: right; 
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 


    <h2>إدارة المهام</h2>

<div class="form-group checkbox-container">
     <h4>تصفية بحسب حالة الإنجاز:</h4>
    <asp:CheckBoxList ID="cblStatus" runat="server" AutoPostBack="true"
        OnSelectedIndexChanged="cblStatus_SelectedIndexChanged" 
        RepeatDirection="Horizontal" CssClass="form-check form-check-inline">
        <asp:ListItem Text="جديدة" Value="New" />
        <asp:ListItem Text="قيد العمل" Value="In Progress" />
        <asp:ListItem Text="مكتملة" Value="Completed" />
        <asp:ListItem Text="مؤرشفة" Value="Archived" />
    </asp:CheckBoxList>
</div>

    
    <asp:GridView ID="TasksGridView" runat="server" AutoGenerateColumns="true"
        CssClass="table" HeaderStyle-BackColor="#eeeeee" OnSelectedIndexChanged="TasksGridView_SelectedIndexChanged">
    </asp:GridView>




</asp:Content>

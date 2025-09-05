<%@ Page Title="تصدير المهام" Language="C#" MasterPageFile="~/LoggedUsers.Master" AutoEventWireup="true" CodeBehind="excelExport.aspx.cs" Inherits="TaskTrackerAUTH.excelExport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>قائمة المهام - التصدير لملف اكسل</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="mb-3">قائمة المهام</h2>

        <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true" CssClass="form-control w-25 mb-3"
            OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
            <asp:ListItem Text="-- فرز حسب --" Value="" />
            <asp:ListItem Text="الاسم (تصاعدي)" Value="TitleAsc" />
            <asp:ListItem Text="الاسم (تنازلي)" Value="TitleDesc" />
            <asp:ListItem Text="تاريخ الاستحقاق (الأحدث أولاً)" Value="DueDateDesc" />
            <asp:ListItem Text="تاريخ الاستحقاق (الأقدم أولاً)" Value="DueDateAsc" />
            <asp:ListItem Text="تاريخ الإنشاء (الأحدث أولاً)" Value="CreatedDesc" />
            <asp:ListItem Text="تاريخ الإنشاء (الأقدم أولاً)" Value="CreatedAsc" />
        </asp:DropDownList>

        <asp:GridView ID="gvTasks" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered mt-3" />

        <asp:Button ID="btnExportExcel" runat="server" Text="تصدير" CssClass="btn btn-primary mt-3"
            OnClick="btnExportExcel_Click" />
    </div>
</asp:Content>


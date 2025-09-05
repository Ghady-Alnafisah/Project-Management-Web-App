<%@ Page Title="" Language="C#" MasterPageFile="~/Authintication/LoggedOutMaster.Master" AutoEventWireup="true" CodeBehind="SigninPage.aspx.cs" Inherits="TasksTrackerWebApp.Authintication.SigninPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container mt-5">
        <h1 class="display-3" style="color:#3b82f6;">مرحبًا بعودتك!</h1>
        <hr />
          <hr />
          <hr />
        <div class="row">
            <!-- Image -->

            <div class="col-md-4 d-flex align-items-center justify-content-center">
                <img src="../Images/collabIMG.png" class="img-rounded img-thumbnail"/>
            </div>

            <!-- Form -->
            <div class="col-md-8">

                <!-- Email -->
                <div class="form-group row">
                         <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    <label class="col-md-3 col-form-label" for="emailtxt">البريد الإلكتروني:</label>
                    <div class="col-md-9">
                        <asp:TextBox ID="emailtxt" runat="server" CssClass="form-control" TextMode="Email" />
                    </div>
                </div>

                <!-- Password -->
                <div class="form-group row">
                    <label class="col-md-3 col-form-label" for="passwrdtxt">كلمة المرور:</label>
                    <div class="col-md-9">
                        <asp:TextBox ID="passwrdtxt" runat="server" CssClass="form-control" TextMode="Password" />
                    </div>
                </div>

                <!-- Error Label -->
                <asp:Label ID="errorMsg" runat="server" CssClass="text-danger"> </asp:Label>

                <!-- Login Button -->
               <hr />
                <hr />
                <hr />
                <div class="mt-3">
                    <asp:Button runat="server" ID="LoginBtn" CssClass="btn btn-primary btn-block" Text="تسجيل الدخول" OnClick="LoginBtn_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>


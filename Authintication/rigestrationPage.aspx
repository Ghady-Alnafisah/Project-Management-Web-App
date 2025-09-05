<%@ Page Title="" Language="C#" MasterPageFile="~/Authintication/LoggedOutMaster.Master" AutoEventWireup="true" CodeBehind="rigestrationPage.aspx.cs" Inherits="TasksTrackerWebApp.rigestrationPage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <h1 class="display-3 " style="color:#3b82f6;">يسعدنا انضمامك!</h1>
        <div class="row">

            <!-- Image -->
            <div class="col-md-4 d-flex align-items-center justify-content-center">
                <img src="../Images/heroPageIMG.png" class="img-thumbnail" alt="Sign Up" />
            </div>

            <!-- Form -->
            <div class="col-md-8">

                <asp:Label ID="errorMsg" runat="server" ForeColor="Red"></asp:Label>

                <!-- First Name -->
                <div class="form-group row">
                    <label class="col-md-3 col-form-label" for="fNametxt">الاسم الأول:</label>
                    <div class="col-md-9">
                        <asp:TextBox ID="fNametxt" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="fNametxt" ErrorMessage="الاسم الأول مطلوب" ForeColor="Red" />
                    </div>
                </div>

                <!-- Last Name -->
                <div class="form-group row">
                    <label class="col-md-3 col-form-label" for="lNametxt">الاسم الأخير:</label>
                    <div class="col-md-9">
                        <asp:TextBox ID="lNametxt" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="lNametxt" ErrorMessage="الاسم الأخير مطلوب" ForeColor="Red" />
                    </div>
                </div>

                <!-- Email -->
                <div class="form-group row">
                    <label class="col-md-3 col-form-label" for="emailtxt">البريد الإلكتروني:</label>
                    <div class="col-md-9">
                        <asp:TextBox ID="emailtxt" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="emailtxt" ErrorMessage="البريد الإلكتروني مطلوب" ForeColor="Red" />
                    </div>
                </div>

                <!-- Password -->
                <div class="form-group row">
                    <label class="col-md-3 col-form-label" for="passwrdtxt">كلمة المرور:</label>
                    <div class="col-md-9">
                        <asp:TextBox ID="passwrdtxt" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="passwrdtxt" ErrorMessage="كلمة المرور مطلوبة" ForeColor="Red" />
                    </div>
                </div>

                <!-- Role -->
                <div class="form-group row">
                    <label class="col-md-4 col-form-label">أستخدم الموقع بصفتي:</label>
                    <asp:RadioButtonList ID="roleList" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="مسؤول نظام" Value="admin"></asp:ListItem>
                        <asp:ListItem Text="مستخدم" Value="user"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="roleList" InitialValue="" ErrorMessage="*يرجى اختيار نوع المستخدم" ForeColor="Red" />
                </div>

                <!-- Submit -->
                <div>
                    <asp:Button runat="server" CssClass="btn btn-primary btn-block" Text="إنشاء الحساب" OnClick="SignUpSubmitBtn_Click" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>

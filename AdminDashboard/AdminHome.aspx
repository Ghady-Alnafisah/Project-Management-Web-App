<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/AdminDashboardMaster.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="TasksTrackerWebApp.AdminDashboard.AdminHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hero-section {
            min-height: 80vh;
            background-color: whitesmoke;
            display: flex;
            align-items: center;
            padding: 50px 20px;
        }

        .hero-title {
            font-size: 2.8rem;
            font-weight: bold;
            color: #333;
        }

        .hero-highlight {
            color: #007bff;
        }

        .hero-description {
            font-size: 1.3rem;
            color: #555;
            margin-top: 20px;
        }

        .hero-img {
            max-width: 100%;
            max-height: 300px;
        }

        .hero-btn {
            margin-top: 30px;
            font-size: 1.2rem;
            padding: 12px 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="container-fluid hero-section">
        <div class="row w-100">
            <div class="col-lg-7 text-center text-lg-start">
                <h1 class="hero-title">
                    مرحبًا بك في لوحة تحكم <span class="hero-highlight">مسؤول النظام</span>
                </h1>
                <p class="hero-description">
                    من هنا يمكنك متابعة أداء النظام، إدارة الوصول والصلاحيات، ومراقبة المستخدمين بكل سهولة.<br/>
                    تأكد من أن كل شيء يعمل بكفاءة.
                </p>
                <a href="systemDashboard.aspx" class="btn btn-primary hero-btn">لوحة التحكم</a>
            </div>
            <div class="col-lg-5 text-center mt-4 mt-lg-0">
                <img src="../Images/AdminPanelicon.PNG" class="img-fluid" alt="Admin Dashboard" />
            </div>
        </div>
    </div>
</asp:Content>

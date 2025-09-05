<%@ Page Title="الرئيسية" Language="C#" MasterPageFile="~/LoggedUsers.Master" AutoEventWireup="true" CodeBehind="userHome.aspx.cs" Inherits="TasksTrackerWebApp.userHome" %>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style>
      
        .hero-section {
            min-height: 80vh;

              background-color:#1F305E;
             
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
      
    @import url("https://fonts.googleapis.com/css?family=Raleway:300");

    .hero-section {
        min-height: 80vh;

    display: flex;
        align-items: center;
        padding: 50px 20px;
    }

    .hero-title {
        font-size: 2.8rem;
        font-weight: bold;
        color: azure;
    }

    .hero-highlight {
        color: #007bff;
    }

    .hero-description {
        font-size: 1.3rem;
        color: aliceblue;
        margin-top: 20px;
    }

    .hero-img {
        max-width: 100%;
        max-height: 300px;
    }

    /* 🔥 Animated glowing button */
    .glow-button {
        position: relative;
        height: 60px;
        width: 250px;
        border: none;
        outline: none;
        color: white;
        background: #6495ED;
        cursor: pointer;
        border-radius: 5px;
        font-size: 1.2rem;
        font-family: 'Raleway', sans-serif;
        z-index: 1;
        transition: background 0.3s ease;
    }

    .glow-button:before {
        position: absolute;
        content: '';
        top: -2px;
        left: -2px;
        height: calc(100% + 4px);
        width: calc(100% + 4px);
        border-radius: 5px;
        z-index: -1;
        filter: blur(5px);
        background: linear-gradient(45deg, #ff0000, #ff7300, #fffb00, #48ff00, #00ffd5, #002bff, #7a00ff, #ff00c8, #ff0000);
        background-size: 400%;
        animation: animate 20s linear infinite;
        opacity: 1;
    }

    .glow-button:active:before {
        filter: blur(2px);
    }

    @keyframes animate {
        0% {
            background-position: 0 0;
        }
        50% {
            background-position: 400% 0;
        }
        100% {
            background-position: 0 0;
        }
    }
</style>

 
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid hero-section">
        <div class="row w-100">
            <div class="col-lg-7 text-center text-lg-start">
                <h1 class="hero-title">
                    أنجز مشاريعك بذكاء مع <span class="hero-highlight">CollabSpace</span>
                </h1>
                <p class="hero-description">
                   
                    مساحة واحدة تجمع الطلاب مع زملائهم ، مدراء الأقسام مع موظفيهم، ومطوّري البرمجيات مع كافة مراحل تطوير برمجياتهم ..<br/>
                    خطط، تعاون، وتتبع دورة حياة مشاريعك بكفاءة.

                </p>
                <a href="TasksPage.aspx" class="glow-button btn-primary hero-btn ">
                    ابدأ رحلتك الآن
                </a>
            </div>
            <div class="col-lg-5 text-center mt-4 mt-lg-0">
                <img src="Images/userHomeTasksLogo.jpg" class="img-fluid" />
            </div>
        </div>
    </div>


</asp:Content>




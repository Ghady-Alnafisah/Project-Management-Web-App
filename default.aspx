
<%@ Page Title="الرئيسية" Language="C#" MasterPageFile="~/mainMaster.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="TasksTrackerWebApp._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hero-section {
            font-family: 'Tahoma', sans-serif;
            direction: rtl;
            padding: 80px 20px;
            background: linear-gradient(135deg, #111827, #1f2937, #0f172a); /* dark grey → black → dark blue */
            color: white;
        }

        .hero-container {
            max-width: 1200px;
            margin: 0 auto;
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            gap: 40px;
        }

        .hero-text {
            flex: 1;
            min-width: 320px;
            text-align: right;
        }

        .hero-text h1 {
            font-size: 3rem;
            margin-bottom: 20px;
            color: #f9fafb; /* white */
            line-height: 1.3;
        }

        .hero-text h1 span {
            color: #3b82f6; /* accent blue */
        }

        .hero-text p {
            font-size: 1.25rem;
            color: #d1d5db; /* light grey */
            margin-bottom: 25px;
            line-height: 1.8;
        }

        .hero-text p strong {
            color: #f9fafb; /* white strong */
        }

        .hero-sub {
            font-size: 1.3rem;
            color: #ffffff;
            margin-bottom: 30px;
        }

        .hero-sub span {
            color: #3b82f6;
            font-weight: bold;
        }

        .hero-btn {
            display: inline-block;
            padding: 14px 36px;
            font-size: 1.2rem;
            background: #3b82f6;
            color: white;
            border-radius: 14px;
            text-decoration: none;
            box-shadow: 0 6px 16px rgba(59,130,246,0.4);
            transition: all 0.3s;
        }

        .hero-btn:hover {
            background: #2563eb; 
            box-shadow: 0 8px 20px rgba(37,99,235,0.5);
        }

        .hero-img {
            max-width: 500px; 
            width: 100%;
            height: auto;
            border-radius: 20px;
            box-shadow: 0 12px 28px rgba(0,0,0,0.5);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="hero-section">
        <div class="hero-container">
            <div class="hero-text">
                <h1>
                    انجز أكثر مع <span>CollabSpace 🚀</span>
                </h1>

                <p>
                    🎓 <strong>للطلاب:</strong> اجعل مشروع التخرج تجربة منظمة وسلسة <br />
                    🏢 <strong>للشركات:</strong> وحّد فرقك المختلفة في مساحة عمل واحدة <br />
                    💻 <strong>لمطوّري البرمجيات:</strong> تتبّع دورة حياة التطوير خطوة بخطوة
                </p>

                <p class="hero-sub">
                    ابدأ رحلتك اليوم مع <span>CollabSpace</span>، حيث تلتقي الأفكار بالتنفيذ.
                </p>

                <a href="Authintication/rigestrationPage.aspx" class="hero-btn">
                    ابدأ الآن مجانًا
                </a>
            </div>

            <img src="Images/collabIMG.png" alt="CollabSpace" class="hero-img" />
        </div>
    </section>
</asp:Content>

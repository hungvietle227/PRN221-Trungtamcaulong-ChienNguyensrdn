using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.Common.DTO.Email
{
    public class EmailTemplate
    {
        public static string ModeratorInfoTemplate(string userEmail, string subject, string email, string name, string phone)
        {
            string htmlTemplate = @"<head>
    <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
    <title>
        {TITLE}
    </title>
    <style type=""text/css"">
        html {
            background-color: #FFF;
        }

        body {
            font-size: 120%;
            background-color: wheat;
            border-radius: 5px;
        }

        .logo {
            text-align: center;
            padding: 2% 0;
        }

        .logo img {
            width: 40%;
            height: 35%;
        }

        .content {
            padding: 2% 5%;
            background-color: #FFF;
            border-radius: 5px 5px 0 0;
        }

        .welcome-text {
            padding-bottom: 1%;
        }

        .features {
            padding-bottom: 1%;
        }

        .feature-header {
            font-weight: bold;
        }
        
        .end-text {
            padding-bottom: 1%;
        }

        .footer {
            padding: 2% 5%;
            text-align: center;
            font-size: 80%;
            opacity: 0.8;
        }
    </style>
</head>

<body>
    <div class=""logo"">
        <img src=""{LOGO_URL}"" />
    </div>
    <div class=""content"">
        <div class=""welcome-text"">
            <p>Welcome {USER_NAME}</p>
            <p>We are pleased to inform you that your account has been added to our GiaSuHocTap website.</p>
            
        </div>
        <div class=""features"">
            <p>This is your account information:</p>
            <p><span class=""feature-header"">Email: {PARENTS_EMAIL}.</span> </p>
            <p><span class=""feature-header"">Name: {PARENTS_NAME}.</span> </p>
            <p><span class=""feature-header"">Password: 12345Aa@</span></p>
        </div>

        <div class=""end-text"">
            <p>Please login soon to check all information. Contact with us as soon as possible for fixing information if it has problem.</p>
            <p>We hope you have exciting and satisfying day.</p>
        </div>
        <p>Best regards,</p>
        <p>The GiaSuHocTap Team</p>
        </p>
    </div>
    <div class=""footer"">
        <p>This is an automatic email. Please do not reply to this email.</p>
        <p>17th Floor LandMark 81, 208 Nguyen Huu Canh Street, Binh Thanh District, Ho Chi Minh 700000, Vietnam</p>
    </div>
</body>

</html>";

            htmlTemplate = htmlTemplate
                .Replace("{USER_NAME}", userEmail)
                .Replace("{PARENTS_NAME}", name)
                .Replace("{TITLE}", subject)
                .Replace("{PARENTS_EMAIL}", email)
                .Replace("{PARENTS_PHONE}", phone);

            return htmlTemplate;
        }
    }
}

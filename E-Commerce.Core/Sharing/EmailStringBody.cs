namespace E_Commerce.Core.Sharing
{
    public class EmailStringBody
    {
        public static string Send(string email, string token, string component, string message)
        {
            string encodedToken = Uri.EscapeDataString(token);
            string url = $"{component}?email={email}&token={encodedToken}";

            return $@"
            <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            color: #333;
                            background-color: #f9f9f9;
                            margin: 0;
                            padding: 20px;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: auto;
                            background: #fff;
                            padding: 20px;
                            border-radius: 10px;
                            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
                        }}
                        h2 {{
                            color: #4CAF50;
                            text-align: center;
                        }}
                        p {{
                            line-height: 1.6;
                        }}
                        .btn {{
                            display: inline-block;
                            background-color: #4CAF50;
                            color: white;
                            padding: 12px 25px;
                            text-decoration: none;
                            border-radius: 5px;
                            font-weight: bold;
                        }}
                        .footer {{
                            margin-top: 20px;
                            font-size: 12px;
                            color: #777;
                            text-align: center;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>{message}</h2>
                        <p>Please click the button below to continue:</p>
                        <p style='text-align: center;'>
                            <a href='{url}' class='btn'>Confirm Action</a>
                        </p>
                        <p>If the button doesn’t work, copy and paste this link into your browser:</p>
                        <p><a href='{url}'>{url}</a></p>
                        <div class='footer'>
                            <p>This is an automated message, please do not reply.</p>
                        </div>
                    </div>
                </body>
            </html>
        ";
        }
    }
}

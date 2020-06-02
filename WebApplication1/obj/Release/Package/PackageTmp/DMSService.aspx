<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DMSService.aspx.vb" Inherits="WebApplication1.DMSService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Topocean DMS Service</title>
    <style type="text/css">
        body {
            font-family: Arial;
            color: #666666;
        }

        table {
            width: 100%;
            border: solid 1px silver;
            text-align: left;
        }

        th, td {
            padding: 2px;
        }

        th {
            width: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <br />
            <br />
            <br />
            <br />
            <img src="images/gears_animated.gif" style="margin: auto;" />
            <br />
            <h2 style="color: maroon;">Loading.......</h2>
        </div>
        <div style="display: none;">
            <form id="form2" runat="server">
                <div>
                    <table>
                        <tr>
                            <th>Name</th>
                            <th>Value</th>
                        </tr>
                        <asp:Literal ID="args" runat="server"></asp:Literal>
                    </table>
                </div>
                <asp:Button ID="btnDisp" runat="server" Text="Dispatch" />
            </form>
            <asp:Literal ID="postjs" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>

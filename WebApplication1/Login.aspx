<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="WebApplication1.Login1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>The Topocean Group</title>
    <link href="login.css" rel="Stylesheet" />
</head>
<body>
    <form id="login" runat="server">
    <div style="left: 0px; position: absolute; top: 0px; width: 100%; height: 100%">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td colspan="2" style="height: 4%; margin-top: 0px; margin-left: 0px; width: 100%; vertical-align: top; text-align: left;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 45px;">
                        <tr>
                            <td style="width: 70px;">
                                <img src="Images/topocean01.jpg" border="0" width="55" alt=""/>
                            </td>
                            <td>
                                <font class="CompanyName">The Topocean Group</font>
                            </td>
                        </tr>
                    </table>    
                </td>
            </tr>
            <tr>
               <td align="center" valign="middle" height="400">
                   <asp:Label ID="lblNotice" runat="server" Width="192px" CssClass="notice" Text="Your username or password is invalid. Please try again."></asp:Label>
                   <br /><br />
                   <table class="Login">
                       <tr>
                           <td class="LoginHdr" colspan="3">
                               DMS System Login
                           </td>
                       </tr>
                       <tr>
                           <td class="LoginTitle">User ID</td>
                           <td style="width: 10px">&nbsp;</td>
                           <td class="LoginCtrl">
                               <asp:TextBox ID="UserName" CssClass="input" runat="server"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td class="LoginTitle">Password</td>
                           <td style="width: 10px">&nbsp;</td>
                           <td class="LoginCtrl">
                               <asp:TextBox ID="Password" CssClass="input" runat="server" TextMode="Password"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td colspan="2">&nbsp;</td>
                           <td class="LoginCtrl">
                               <asp:Button ID="LoginButton" runat="server" CssClass="btn_blue" Text="Login" />
                               <!--<input type="reset" class="btn_blue" name="Reset" value="Reset" />-->
                           </td>
                       </tr>
                   </table>
               </td>
            </tr>
        </table>
    </div>
    </form>
	<script type="text/javascript">
		if (document.getElementById("UserName") != null && document.getElementById("UserName") != undefined) {
			document.getElementById("UserName").focus();
		}
	</script>
</body>
</html>
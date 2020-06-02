<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DMSDownloader02.aspx.vb" Inherits="WebApplication1.DMSDownloader02" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
        .menu_heading {
            background-color: #D0CBDA;
            font-size: 10pt;
            font-weight: 700;
        }

        a:hover {
            font-size: 11px;
            color: #f60;
            text-decoration: underline;
        }

        a:link, a:active, a:visited {
            font-size: 11px;
            color: #03c;
        }

        .schform {
            font-family: Arial,Helvetica,sans-serif;
            color: #000;
            font-size: 9pt;
            font-weight: 400;
        }

        .schform th, .schform td {
            padding: 2px;
            border-spacing: 1px;
            vertical-align: middle;
        }

        .schform th {
            padding-right: 4px;
            text-align: right;
            width: 100px;
        }

        .schform td {
            padding-right: 4px;
        }

        .schform input[type="text"], .schform select {
            width: 100%;
            margin-right: 4px;
        }

        .schform input[type="text"]:focus {
            background-color: rgb(250, 238, 250);
        }

        .schform .search_label {
            font-size: 9pt;
            font-weight: 700;
            color: #666;
            background-color: #CCCCCC;
        }

        .schform .cmdButton {
            min-width: 60px;
            border-radius: 5px;
            padding: 2px;
            background-color: rgb(206, 206, 206);
            color: rgb(47, 130, 121);
            margin-right: 5px;
            font-weight: 700;
        }

        .downloadG {
            font-family: Arial,Helvetica,sans-serif;
            color: #000;
            font-size: 9pt;
            font-weight: 400;
        }

        .hidden {
            display: none;
        }

        .info {
            font-family: Arial;
            font-size: 11px;
        }

        .info table {
            width: 100%;
            text-align: left;
            color: #333333;
            background-color: #EEF1F6;
        }

        .info th, .info td {
            padding: 7px 7px 7px 7px;
            white-space: nowrap;
            line-height: 150%;
        }

        .info th {
            background-color: #5D7B9D;
            color: white;
        }

        #parentDiv {
            position:relative;
            height:500px;
            width:100%;
        }
        #childDiv {
            position:absolute;
            top:50%;
            height:250px;
            width: 50%;
            margin-top:0px;
            margin-left:550px;

        }

        .auto-style1 {
            height: 28px;
        }

        </style>
    <link href="Style.css" rel="Stylesheet" />
    <link rel="stylesheet" href="Scripts/jquery-ui/themes/smoothness/jquery-ui.css" />
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/jquery-ui/ui/jquery-ui.js"></script>
    <link rel="stylesheet" href="http://www.w3schools.com/lib/w3.css" />
</head>
<body>
    <form id="form1" runat="server">


        <%--<div style="margin-left: auto; margin-right: auto; text-align: center; ">--%>
<%--        <div style="margin-left: auto; margin-right: auto; text-align: center;">
            

        </div>--%>
 
        <div id="parentDiv" runat="server" Visible="False"> 
             <div id="childDiv">
                 

                 <table style="width:45% ;vertical-align:middle;">
                  <tr>
                    <td> 
                       <p style="text-align:center"> <asp:Label ID="lblMessage" runat="server" Text="Label" ForeColor="Red" Width="350px" ></asp:Label></p>
                    </td>
                  </tr>
                  <tr>
                      <td>
                          <p style="text-align:center"><asp:Button ID="BtnHome" runat="server" Text="Home"  /></p>
                      </td>
                  </tr>

                </table>

             </div>
        </div>


        <div id="checktimeout" runat="server">

        <div class="schform">
            <div class="w3-container w3-indigo w3-center">
                <h2>Batch Document Download</h2>
            </div>
            <table style="width: 100%; margin: auto;">
                <tr>
                    <td style="border: solid 1px silver;">
                        <table style="width: 100%; background-color: #FFFFFF;">
                            <colgroup>
                                <col style="width: 135px;" />
                                <col style="width: 30%;" />
                                <col style="width: 135px;" />
                                <col style="width: 30%;" />
                            </colgroup>
                            <tr>
                                <td class="search_label">MB/L# : </td>
                                <td>
                                    <asp:TextBox ID="sch_mbl" runat="server"></asp:TextBox>
                                </td>
                                <td class="search_label">Invoice#</td>
                                <td>
                                    <asp:TextBox ID="sch_inv" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="search_label">HB/L# : </td>
                                <td>
                                    <asp:TextBox ID="sch_hbl" runat="server"></asp:TextBox>
                                </td>
                                <td class="search_label">Voucher#</td>
                                <td>
                                    <asp:TextBox ID="sch_vou" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="search_label" style="height: 28px">Consignee : </td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="sch_cnee" runat="server"></asp:TextBox>
                                </td>
                                <td class="search_label" style="height: 28px">Freight List#</td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="sch_fre" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="search_label">
                                    <table style="width: 100%; height: 100%;">
                                        <tr>
                                            <td rowspan="2">ETD</td>
                                            <td style="text-align: right;">From :</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">To :</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:TextBox ID="sch_etd1" runat="server" CssClass="datepicker"></asp:TextBox><br />
                                    <asp:TextBox ID="sch_etd2" runat="server" CssClass="datepicker"></asp:TextBox>
                                </td>
                                <td class="search_label">
                                    <table style="width: 100%; height: 100%;">
                                        <tr>
                                            <td rowspan="2">ETA</td>
                                            <td style="text-align: right;">From :</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">To :</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:TextBox ID="sch_eta1" runat="server" CssClass="datepicker"></asp:TextBox><br />
                                    <asp:TextBox ID="sch_eta2" runat="server" CssClass="datepicker"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="search_label">Carrier SCAC : </td>
                                <td>
                                    <asp:TextBox ID="sch_scac" runat="server"></asp:TextBox>
                                </td>
                                <td class="search_label">File Type</td>
                                <td>
                                    <asp:DropDownList ID="DropDownListFType" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="search_label">Vessel : </td>
                                <td>
                                    <asp:TextBox ID="sch_vessel" runat="server"></asp:TextBox>
                                </td>
                                <td class="search_label">Voyage : </td>
                                <td>
                                    <asp:TextBox ID="sch_voyage" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="search_label">Booking Year : </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListYear" runat="server"></asp:DropDownList>
                                </td>
                                <td class="search_label" style="height: 28px">Booking Week : </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListWeek" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; padding: 4px;">
                                    <asp:Button ID="btnGo" runat="server" Text="Go!" CssClass="w3-btn w3-blue w3-hover-red w3-large" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="w3-btn w3-blue w3-hover-red w3-large" />
                                    <asp:Button ID="btnD1" runat="server" Text="Download Selected" CssClass="w3-btn w3-blue w3-hover-red w3-large" />
                                    <asp:Button ID="btnD2" runat="server" Text="Download All" CssClass="w3-btn w3-blue w3-hover-red w3-large" />
                                    <asp:Button ID="btnD3" runat="server" Text="Close" CssClass="w3-btn w3-blue w3-hover-red w3-large" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="white-space: nowrap; padding: 4px;">
                                    <div style="width: 150px; text-align: left; float: right;">
                                        <asp:CheckBox ID="txt_inchis" runat="server" Text="Include History Files" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td style="padding: 4px; text-align: left;">
                        <div style="line-height: 150%; overflow: auto; max-height: 100px;">
                            <asp:Literal ID="errmsg" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr id="candownload" runat="server">
                    <td style="padding: 4px; text-align: center;">
                        <div style="display: none;">
                            <asp:Literal ID="dbginfo" runat="server"></asp:Literal>
                            <asp:Label ID="token" runat="server" EnableViewState="true" Style="display: none;"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    <div id="doblk">
        <div class="w3-container w3-lime">
            <h4>Documents</h4>
        </div>
        <div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:GridView ID="G1" runat="server" AutoGenerateColumns="False" Width="100%" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="FilRefId" OnRowDataBound="G1_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="txt_FilRefId" Value='<%# Bind("FilRefId")%>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="*">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="txt_sel" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Type" SortExpression="ftypename">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_ftypename" runat="server" Text='<%# Bind("FileType")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File" SortExpression="FileName">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="txt_FileName" runat="server" Text='<%# Bind("FileName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="txt_FullFileName" Value='<%# Bind("FullFileName")%>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update Date" SortExpression="LastDate">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_LastUTC" Text='<%# Bind("LastDate")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="RevCount" HeaderText="Rev." SortExpression="RevCount">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Height="20px" HorizontalAlign="Left" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle Height="20px" HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="GridMessage" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;">
                        <div style="display: none;">
                            <asp:TextBox ID="txt_dmsuser" runat="server"></asp:TextBox>
                            <asp:Literal ID="debugmsg" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".datepicker").datepicker({ dateFormat: "dd/mm/yy" });
        });
    </script>
    </form>
</body>
</html>
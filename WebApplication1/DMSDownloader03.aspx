<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DMSDownloader03.aspx.vb" Inherits="WebApplication1.DMSDownloader03" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        body {
            color: #333333;
            font-family: arial,helvetica;
        }

        .documentTable {
            border-collapse: collapse;
            border-spacing: 0;
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-size: 12px;
            color: #000000;
            width: 90%;
            margin: auto;
        }

        .tableHeader {
            background-color: #F0E080;
            border-style: solid;
            border-width: 1px;
            font-weight: bold;
            padding: 5px 18px;
        }

        .sortHeader {
            background-color: #F0E080;
            border-style: solid;
            border-width: 1px;
            font-weight: bold;
            padding: 5px 18px;
            background-image: url("Images/sort_none.gif");
            background-position: right center;
            background-repeat: no-repeat;
            cursor: pointer;
        }

        .cmdButton {
            width: 150px;
            padding: 2px;
            border-radius: 5px;
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

        .upfile th, .upfile td {
            text-align: left;
            padding: 2px;
            border: none;
        }

        .upfile th {
            color: #FFFFFF;
            background-color: #5D7B9D;
            padding: 5px 18px;
            white-space: nowrap;
        }

        .upfile input[type='text'], .upfile input[type='file'], .upfile select {
            width: 98%;
        }

        .upfile select {
            border: 1px solid #C0C0C0;
            border-radius: 4px;
            display: inline-block;
            margin: 2px;
            padding: 2px;
        }

        .upfile input[type='text'] {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #FAEAFA;
            margin-right: 0.6%;
        }
        .auto-style1 {
            height: 37px;
        }
        .auto-style2 {
            width: 148px;
        }
        .auto-style3 {
            height: 32px;
        }
        .auto-style4 {
            width: 118px;
        }
        
        #datePicker1
        {
            display:none;
            position:absolute;
            border:solid 2px black;
            background-color:white;
        }
        #datePicker2
        {
            display:none;
            position:absolute;
            border:solid 2px black;
            background-color:white;
        }
        #datePicker3
        {
            display:none;
            position:absolute;
            border:solid 2px black;
            background-color:white;
        }
        #datePicker4
        {
            display:none;
            position:absolute;
            border:solid 2px black;
            background-color:white;
        }
        .content
        {
            width:400px;
            background-color:white;
            margin:auto;
            padding:10px;
        }


        #datePickerr1 {
            margin:0 auto;
            width: 205px;
        }
        #datePickerr2 {
            margin:0 auto;
            width: 205px;
        }
        #datePickerr3 {
            margin:0 auto;
            width: 205px;
        }
        #datePickerr4 {
            margin:0 auto;
            width: 205px;
        }
        #datePickerr5 {
            margin:0 auto;
            width: 205px;
        }
        #datePickere1 {
            margin:0 auto;
            width: 205px;
        }
        #datePickere2 {
            margin:0 auto;
            width: 205px;
        }
        #datePickere3 {
            margin:0 auto;
            width: 205px;
        }
        #datePickere4 {
            margin:0 auto;
            width: 205px;
        }
        #datePickere5 {
            margin:0 auto;
            width: 205px;
        }


        .auto-style5 {
            height: 31px;
        }


        .auto-style6 {
            height: 34px;
        }


        </style>
    <link href="Style.css" rel="Stylesheet" />
    <link rel="stylesheet" href="Scripts/jquery-ui/themes/smoothness/jquery-ui.css" />
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/jquery-ui/ui/jquery-ui.js"></script>
    <script src="Scripts/jquery-ui/ui/jquery.ui.datepicker.js"></script>
    <script type="text/javascript">
        var maxRequestLength = 4194304;
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <div class="documentTable">
            <table >
                <tr>
                    <td class="info">
                        <table>
                            <tr>
                                <th class="auto-style4" style="width: 30%;">Customer Name :</th>
                                <td class="auto-style2" style="width: 70%;" >
                                    <asp:Label ID="txt_customername" runat="server" Text=""  style="width:100%" ></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 6px;">
                        <div style="display: none;">
                            <asp:Literal ID="dbginfo" runat="server"></asp:Literal>
                        </div>
                        <asp:Label ID="token" runat="server" Style="display: none;"></asp:Label>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>

        <div id="doblk">
            <h3>Active Customer Documents</h3>
            <div>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:GridView ID="G1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1000px">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="txt_FilRefId" runat="server" Value='<%# Bind("FilRefId")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="txt_FullFileName" runat="server" Value='<%# Bind("FullFileName")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="*">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txt_sel" runat="server" />
                                                <br />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FileType" HeaderText="File Type">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="File Name" SortExpression="FileName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="txt_FileName" runat="server" Text='<%# Bind("FileName") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" Visible="False">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ImportDate" HeaderText="Upload Date">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UploadUsr" HeaderText="Upload User" />
                                        <asp:BoundField DataField="UploadFileEffDay" HeaderText="Effective Date" SortExpression="UploadFileEffDay" />
                                        <asp:BoundField DataField="UploadFileExpDay" HeaderText="Expiry Date" SortExpression="UploadFileExpDay" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="False" ForeColor="White" Height="10px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" class="auto-style1">
                                <asp:Button ID="btnD1" runat="server" Text="Download Selected" CssClass="cmdButton" />
                                &nbsp;<asp:Button ID="btnD2" runat="server" Text="Download All" CssClass="cmdButton" />
                                &nbsp;<asp:Button ID="btnD3" runat="server" Text="Close" CssClass="cmdButton" />
                                &nbsp;&nbsp;</td>
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

         <div id="trash">
            <h3>Customer Documents History</h3>
            <div>
                    <table style="width: 100%;">

                        <tr>
                            <td colspan="3">

                                <asp:GridView ID="G2" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1000px">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="txt_FilRefId0" runat="server" Value='<%# Bind("FilRefId")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="txt_FullFileName0" runat="server" Value='<%# Bind("FullFileName")%>' />
                                            </ItemTemplate>
                                           <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="*">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="txt_sel0" runat="server" />
                                                <br />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FileType" HeaderText="File Type">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="File Name" SortExpression="FileName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="txt_FileName" runat="server" Text='<%# Bind("FileName") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" Visible="False">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ImportDate" HeaderText="Upload Date">
                                       <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UploadUsr" HeaderText="Upload User" />
                                        <asp:BoundField DataField="UploadFileEffDay" HeaderText="Effective Date" SortExpression="UploadFileEffDay" />
                                        <asp:BoundField DataField="UploadFileExpDay" HeaderText="Expiry Date" SortExpression="UploadFileExpDay" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="False" ForeColor="White" Height="10px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>




                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" class="auto-style1">
                                <asp:Button ID="Button4" runat="server" Text="Download Selected" CssClass="cmdButton" />
                                &nbsp;<asp:Button ID="Button5" runat="server" Text="Download All" CssClass="cmdButton" />
                                &nbsp;<asp:Button ID="Button6" runat="server" Text="Close" CssClass="cmdButton" />
                                &nbsp;&nbsp;</td>
                        </tr>

                    </table>
                </div>
             </div>
        <asp:HiddenField ID="displayflag" runat="server" Value="0" />
        <div id="upblk">
            <h3>Uploader</h3>
                

                <div id="upd1">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%; padding: 0px; margin: 0px; height: 95px;">
                                    <tr>
                                        <td>
                                            <table class="upfile" style="width: 100%;">
                                                <colgroup>
                                                    <col style="width: 20%;" />
                                                    <col style="width: 40%;"/>
                                                    <col style="width: 20%;"/>
                                                    <col style="width: 20%;"/>
                                                </colgroup>
                                                <tr>
                                                    <th>File Type</th>
                                                    <th>File</th>
                                                    <th>Effective Date</th>
                                                    <th>Expiry Date</th>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style3">
                                                        <asp:DropDownList ID="ft1" runat="server" />
                                                    </td>
                                                    <td class="auto-style3">
                                                        <asp:FileUpload ID="updrr1" runat="server"></asp:FileUpload>
                                                    </td>

                                                    <td class="auto-style3">
                                                        
                                                         <div id="datePickerr1">
                                                             <asp:TextBox ID="dp1" runat="server" BackColor="White" Width="140px" style="text-align:center" BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px"></asp:TextBox>
                                                         </div>
          
                                                        
                                                    </td>

                                                    <td class="auto-style3">
                                                        
                                                         <div id="datePickere1">
                                                            <asp:TextBox ID="dpp1" runat="server" BackColor="White" Width="140px" style="text-align:center" BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px"></asp:TextBox>
                                                         
                                                             </div>
                                                         
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td class="auto-style5">
                                                        <asp:DropDownList ID="ft2" runat="server" />
                                                    </td>

                                                    <td class="auto-style5">
                                                        <asp:FileUpload ID="updrr2" runat="server"></asp:FileUpload>
                                                    </td>

                                                    <td class="auto-style5">
                                                        
                                                         <div id="datePickerr2">
                                                            <asp:TextBox ID="dp2" runat="server" BackColor="White" Width="140px" style="text-align:center" BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px"></asp:TextBox>
                                                             </div>
                                          
                                                    </td>

                                                    <td class="auto-style5">
                                                        
                                                         <div id="datePickere2">
                                                             <asp:TextBox ID="dpp2" runat="server" BackColor="White" Width="140px" style="text-align:center" BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px"></asp:TextBox>
                                                             </div>
                                       
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td class="auto-style5">
                                                        <asp:DropDownList ID="ft3" runat="server" />
                                                    </td>

                                                    <td class="auto-style5">
                                                        <asp:FileUpload ID="updrr3" runat="server"></asp:FileUpload>
                                                    </td>

                                                    <td class="auto-style5">
                                                        
                                                         <div id="datePickerr3">
                                                            <asp:TextBox ID="dp3" runat="server" BackColor="White" Width="140px" style="text-align:center" BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px"></asp:TextBox>
                                                             </div>
                                          
                                                    </td>

                                                    <td class="auto-style5">
                                                        
                                                         <div id="datePickere3">
                                                             <asp:TextBox ID="dpp3" runat="server" BackColor="White" Width="140px" style="text-align:center" BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px"></asp:TextBox>
                                                             </div>
                                       
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td class="auto-style5">
                                                        <asp:DropDownList ID="ft4" runat="server" />
                                                    </td>

                                                    <td class="auto-style5">
                                                        <asp:FileUpload ID="updrr4" runat="server"></asp:FileUpload>
                                                    </td>

                                                    <td class="auto-style5">
                                                        
                                                         <div id="datePickerr4">
                                                            <asp:TextBox ID="dp4" runat="server" BackColor="White" Width="140px" style="text-align:center" BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px"></asp:TextBox>
                                                             </div>
                                          
                                                    </td>

                                                    <td class="auto-style5">
                                                        
                                                         <div id="datePickere4">
                                                             <asp:TextBox ID="dpp4" runat="server" BackColor="White" Width="140px" style="text-align:center" BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px"></asp:TextBox>
                                                             </div>
                                       
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td class="auto-style5">
                                                        <asp:DropDownList ID="ft5" runat="server" />
                                                    </td>

                                                    <td class="auto-style5">
                                                        <asp:FileUpload ID="updrr5" runat="server"></asp:FileUpload>
                                                    </td>

                                                    <td class="auto-style5">
                                                        
                                                         <div id="datePickerr5">
                                                            <asp:TextBox ID="dp5" runat="server" BackColor="White" Width="140px" style="text-align:center" BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px"></asp:TextBox>
                                                             </div>
                                          
                                                    </td>

                                                    <td class="auto-style5">
                                                        
                                                         <div id="datePickere5">
                                                             <asp:TextBox ID="dpp5" runat="server" BackColor="White" Width="140px" style="text-align:center" BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px"></asp:TextBox>
                                                             </div>
                                       
                                                    </td>

                                                </tr>

                                            </table>
                                        </td>
                                    </tr>
                                
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" class="auto-style6">
                                <asp:Button ID="bunUpload" runat="server" Text="Upload" CssClass="cmdButton" Visible="True" OnClick="bunUpload_onClick" OnClientClick="this.disabled='true'; checkforupload();" UseSubmitBehavior="false"  />
                                
                                <asp:Button ID="bunSendMail" runat="server" Text="Send Notification" CssClass="cmdButton" Visible="False" />
                                
                                <asp:Button ID="bunRefresh" runat="server" CssClass="cmdButton" Text="Refresh" Visible="False" />

                            </td>
                        </tr>
                    </table>
                </div>

   
        </div>
        <script type="text/javascript">
            //var dropZone;
           // var dropZone1;
            var uploadedFiles = 0;
            var filesDropped;


            function checkforupload() {
                //var ftyp = document.getElementById('updrr1');
                //alert(ftyp.value);
                
                var ftyp1 = document.getElementById('ft1').value;
                var ftyp2 = document.getElementById('ft2').value;
                var ftyp3 = document.getElementById('ft3').value;
                var ftyp4 = document.getElementById('ft4').value;
                var ftyp5 = document.getElementById('ft5').value;

                var dpyp1 = document.getElementById('dp1').value;
                var dpyp2 = document.getElementById('dp2').value;
                var dpyp3 = document.getElementById('dp3').value;
                var dpyp4 = document.getElementById('dp4').value;
                var dpyp5 = document.getElementById('dp5').value;

                var dppyp1 = document.getElementById('dpp1').value;
                var dppyp2 = document.getElementById('dpp2').value;
                var dppyp3 = document.getElementById('dpp3').value;
                var dppyp4 = document.getElementById('dpp4').value;
                var dppyp5 = document.getElementById('dpp5').value;

                var updrryp1 = document.getElementById('updrr1').value;
                var updrryp2 = document.getElementById('updrr2').value;
                var updrryp3 = document.getElementById('updrr3').value;
                var updrryp4 = document.getElementById('updrr4').value;
                var updrryp5 = document.getElementById('updrr5').value;

                if (ftyp1 == "QUOTATION") {

                    if(!updrryp1){
                        alert('Upload file is missing');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dpyp1) {
                        alert('Effective Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dppyp1) {
                        alert('Expiry Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }


                }

                if (ftyp1 == "SOP" || ftyp1 == "POA" || ftyp1 == "ISF enrollment form" || ftyp1 == "Bond Certificate") {
                    if (!updrryp1) {
                        alert('Upload file is missing');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dpyp1) {
                        alert('Effective Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                }


                

                if (ftyp3 == "QUOTATION") {
                    if (!updrryp3) {
                        alert('Upload file is missing');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dpyp3) {
                        alert('Effective Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dppyp3) {
                        alert('Expiry Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                }

                if (ftyp3 == "SOP" || ftyp3 == "POA" || ftyp3 == "ISF enrollment form" || ftyp3 == "Bond Certificate") {

                    if (!updrryp3) {
                        alert('Upload file is missing');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dpyp3) {
                        alert('Effective Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                }

                if (ftyp4 == "QUOTATION") {
                    if (!updrryp4) {
                        alert('Upload file is missing');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dpyp4) {
                        alert('Effective Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dppyp4) {
                        alert('Expiry Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                }

                if (ftyp4 == "SOP" || ftyp4 == "POA" || ftyp4 == "ISF enrollment form" || ftyp4 == "Bond Certificate") {

                    if (!updrryp4) {
                        alert('Upload file is missing');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dpyp4) {
                        alert('Effective Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                }

                if (ftyp5 == "QUOTATION") {
                    if (!updrryp5) {
                        alert('Upload file is missing');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dpyp5) {
                        alert('Effective Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dppyp5) {
                        alert('Expiry Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                }

                if (ftyp5 == "SOP" || ftyp5 == "POA" || ftyp5 == "ISF enrollment form" || ftyp5 == "Bond Certificate") {

                    if (!updrryp5) {
                        alert('Upload file is missing');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                    if (!dpyp5) {
                        alert('Effective Date is missing!');
                        document.getElementById('bunUpload').disabled = false;
                        return false;
                    }

                }

                return true;

               
            }
          


            function filetypeHelper(ft) {

                var aaid = ft[0].id.replace("ft", "datePickerr");
                var aid = ft[0].id.replace("ft", "datePickere");
                var uid = ft[0].id.replace("ft", "updrr");

                var imgid = document.getElementById(aaid);
                var imgid2 = document.getElementById(aid);
                var up = document.getElementById(uid);

                var ftyp = ft.val();
                if ((ftyp == "SOP") || (ftyp == "QUOTATION") || (ftyp == "POA") || (ftyp == "ISF enrollment form") || (ftyp == "Bond Certificate")) {
                    $(up).css("display", "");
                    $(imgid).css("display", "");
                    $(imgid2).css("display", "");
                }
                else {
                    $(up).css("display", "none");
                    $(imgid).css("display", "none");
                    $(imgid2).css("display", "none");
                };

            };

            // Upload the file
            function uploadFile(file, i) {
                var xhr = new XMLHttpRequest();
                //xhr.upload.addEventListener('progress', uploadProgress, false);
                xhr.upload.addEventListener('progress', function (event) {
                    $('#progress' + i).html(' \
                <div class="fileName"> ' + trimString(file.name) + '</div> \
                <div class="barHolder"> \
                    <div id="barFiller' + i + '" class="barFiller" style="width: ' + parseInt(event.loaded / event.total * 100) + '%"></div> \
                </div> \
                <div id="uploadProgress' + i + '" class="uploadProgress">Uploading: ' + parseInt(event.loaded / event.total * 100) + '%</div>');
                }, false);
                xhr.onreadystatechange = function (event) {
                    return function () {
                        if (event.target.readyState == 4) {
                            if (event.target.status == 200 || event.target.status == 304) {
                                $('#uploadProgress' + i).html('Upload Complete.');
                                uploadedFiles++;

                            }
                            else {
                                $('#barFiller' + i).addClass('barError');
                                $('#uploadProgress' + i).html('Error: XMLHttpRequest Error!');
                                $('#uploadProgress' + i).addClass('uploadError');
                                uploadedFiles++;
                            }
                            // If all files have been uploaded show drop files here text
                            //if (uploadedFiles >= filesDropped) {
                            //    dropZone.removeClass('hover');
                            //    dropZone.text('Drop Files Here.');
                            //}
                        }
                    }(event);
                };
                xhr.open('POST', '/Default.aspx?UploadRegion=0', true);
                xhr.setRequestHeader('X-FILE-NAME', file.name);
                xhr.send(file);
            }

            // Upload the file
            function uploadFile1(file, i) {
                var xhr = new XMLHttpRequest();
                //xhr.upload.addEventListener('progress', uploadProgress, false);
                xhr.upload.addEventListener('progress', function (event) {
                    $('#progress1' + i).html(' \
                <div class="fileName"> ' + trimString(file.name) + '</div> \
                <div class="barHolder"> \
                    <div id="barFiller' + i + '" class="barFiller" style="width: ' + parseInt(event.loaded / event.total * 100) + '%"></div> \
                </div> \
                <div id="uploadProgress1' + i + '" class="uploadProgress">Uploading: ' + parseInt(event.loaded / event.total * 100) + '%</div>');
                }, false);
                xhr.onreadystatechange = function (event) {
                    return function () {
                        if (event.target.readyState == 4) {
                            if (event.target.status == 200 || event.target.status == 304) {
                                $('#uploadProgress1' + i).html('Upload Complete.');
                                uploadedFiles++;

                            }
                            else {
                                $('#barFiller' + i).addClass('barError');
                                $('#uploadProgress1' + i).html('Error: XMLHttpRequest Error!');
                                $('#uploadProgress1' + i).addClass('uploadError');
                                uploadedFiles++;
                            }
                            // If all files have been uploaded show drop files here text
                            if (uploadedFiles >= filesDropped) {
                                dropZone1.removeClass('hover');
                                dropZone1.text('Drop Files Here.');
                            }
                        }
                    }(event);
                };
                xhr.open('POST', '/Default.aspx?UploadRegion=1', true);
                xhr.setRequestHeader('X-FILE-NAME', file.name);
                xhr.send(file);
            }

            function trimString(sentSting) {
                allowedLength = 27;
                var newString;
                if (sentSting.length > allowedLength) {
                    newString = sentSting.substring(0, allowedLength) + '...';
                } else {
                    newString = sentSting;
                }
                return newString;
            }

            

            $('#dp1').datepicker({
                rangeSelect: true, firstDay: 1, showOn: 'both',
                buttonImageOnly: true, buttonImage: 'images/Calendar.gif'
            });

            $('#dpp1').datepicker({
                rangeSelect: true, firstDay: 1, showOn: 'both',
                buttonImageOnly: true, buttonImage: 'images/Calendar.gif'
            });

            $('#dp2').datepicker({
                rangeSelect: true, firstDay: 1, showOn: 'both',
                buttonImageOnly: true, buttonImage: 'images/Calendar.gif'
            });

            $('#dpp2').datepicker({
                rangeSelect: true, firstDay: 1, showOn: 'both',
                buttonImageOnly: true, buttonImage: 'images/Calendar.gif'
            });

            $('#dp3').datepicker({
                rangeSelect: true, firstDay: 1, showOn: 'both',
                buttonImageOnly: true, buttonImage: 'images/Calendar.gif'
            });

            $('#dpp3').datepicker({
                rangeSelect: true, firstDay: 1, showOn: 'both',
                buttonImageOnly: true, buttonImage: 'images/Calendar.gif'
            });

            $('#dp4').datepicker({
                rangeSelect: true, firstDay: 1, showOn: 'both',
                buttonImageOnly: true, buttonImage: 'images/Calendar.gif'
            });

            $('#dpp4').datepicker({
                rangeSelect: true, firstDay: 1, showOn: 'both',
                buttonImageOnly: true, buttonImage: 'images/Calendar.gif'
            });

            $('#dp5').datepicker({
                rangeSelect: true, firstDay: 1, showOn: 'both',
                buttonImageOnly: true, buttonImage: 'images/Calendar.gif'
            });

            $('#dpp5').datepicker({
                rangeSelect: true, firstDay: 1, showOn: 'both',
                buttonImageOnly: true, buttonImage: 'images/Calendar.gif'
            });


            //function displayCalendar2() {
            //    var datePicker2 = document.getElementById('datePicker2');
            //    datePicker2.style.display = 'block';
            //}
            //function displayCalendar3() {
            //    var datePicker3 = document.getElementById('datePicker3');
            //    datePicker3.style.display = 'block';
            //}
            //function displayCalendar4() {
            //    var datePicker4 = document.getElementById('datePicker4');
            //    datePicker4.style.display = 'block';
            //}

            $(document).ready(function () {

                $("#doblk").accordion({ collapsible: true, active: 0 });
                $("#trash").accordion({ collapsible: true, active: 0 });
                $("#upblk").accordion({ collapsible: true, active: 0 });
                $("#tabs").tabs({ heightStyle: "fill" });


                $('[id*="datePickerr"]').css("display", "none");
                $('[id*="datePickere"]').css("display", "none");
                $('[id*="updrr"]').css("display", "none");
                $('[id*="ft"]').on("change", function (e) {
                 filetypeHelper($(this));
                });

                if (document.getElementById('displayflag').value == "1")
                {
                    $("#upblk").css("display", "none");
                }

            });
                //$('[id*="agt"]').css("display", "none");
                //$('[id*="updr"]').css("display", "none");
                //$('[id*="ft"]').on("change", function (e) {
                   // filetypeHelper($(this));
                //});

                //dropZone = $('#dropZone');
                //dropZone1 = $('#dropZone1');
                //dropZone.removeClass('error');
                //dropZone1.removeClass('error');

                // Check if window.FileReader exists to make 
                // sure the browser supports file uploads
                //if (typeof (window.FileReader) == 'undefined') {
                //    dropZone.text('Browser Not Supported!');
                //    dropZone1.text('Browser Not Supported!');
                //    dropZone.addClass('error');
                //    dropZone1.addClass('error');
                //    return;
                //}

                // Add a nice drag effect
                //dropZone[0].ondragover = function () {
                //    dropZone.addClass('hover');
                //    return false;
                //};

                //dropZone1[0].ondragover = function () {
                //    dropZone1.addClass('hover');
                //    return false;
                //};

                // Remove the drag effect when stopping our drag
                //dropZone[0].ondragend = function () {
                //    dropZone.removeClass('hover');
                //    return false;
                //};

                //dropZone1[0].ondragend = function () {
                //    dropZone1.removeClass('hover');
                //    return false;
                //};

                // The drop event handles the file sending
                //dropZone[0].ondrop = function (event) {
                //    // Stop the browser from opening the file in the window
                //    event.preventDefault();

                //    // Get the file and the file reader
                //    var files = event.dataTransfer.files;

                //    // Assign how many files were dropped to a variable
                //    filesDropped = files.length;

                //    // Empty the progressZone div
                //    $('#progressZone').empty();

                //    // Loop through dropped files
                //    for (var i = 0; i < files.length; i++) {
                //        var file = files[i];

                //        // Add a div for each file being uploaded
                //        $('#progressZone').append('<div id="progress' + i + '"></div>');

                //        // Validate file size
                //        if (file.size > maxRequestLength) {
                //            $('#progress' + i).html(' \
                //            <div class="fileName">' + trimString(file.name) + '</div> \
                //            <div class="barError"></div> \
                //            <div class="uploadError">Error: File Too Large!</div>');
                //            uploadedFiles++;
                //            continue;
                //        }

                //        // Report upload in progress
                //        dropZone.text('Upload In Progress...');

                //        // Send the file
                //        uploadFile(file, i);
                //    }
                //};

                //// The drop event handles the file sending
                //dropZone1[0].ondrop = function (event) {
                //    // Stop the browser from opening the file in the window
                //    event.preventDefault();

                //    // Get the file and the file reader
                //    var files = event.dataTransfer.files;

                //    // Assign how many files were dropped to a variable
                //    filesDropped = files.length;

                //    // Empty the progressZone div
                //    $('#progressZone1').empty();

                //    // Loop through dropped files
                //    for (var i = 0; i < files.length; i++) {
                //        var file = files[i];

                //        // Add a div for each file being uploaded
                //        $('#progressZone1').append('<div id="progress1' + i + '"></div>');

                //        // Validate file size
                //        if (file.size > maxRequestLength) {
                //            $('#progress1' + i).html(' \
                //            <div class="fileName">' + trimString(file.name) + '</div> \
                //            <div class="barError"></div> \
                //            <div class="uploadError">Error: File Too Large!</div>');
                //            uploadedFiles++;
                //            continue;
                //        }

                //        // Report upload in progress
                //        dropZone1.text('Upload In Progress...');

                //        // Send the file
                //        uploadFile1(file, i);
                //    }
                //};
            
        </script>
        <asp:Literal ID="postjs" runat="server"></asp:Literal>
    </form>
</body>
</html>

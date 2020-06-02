<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DMSDownloader01.aspx.vb" Inherits="WebApplication1.DMSDownloader01" %>

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
            border: none;
            height: 100%;
            background-color: #FAEAFA;
            border: none;
            margin-right: 0.6%;
        }
        .auto-style1 {
            height: 37px;
        }

        </style>
    <link href="Style.css" rel="Stylesheet" />
    <link rel="stylesheet" href="Scripts/jquery-ui/themes/smoothness/jquery-ui.css" />
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/jquery-ui/ui/jquery-ui.js"></script>
    <script type="text/javascript">
        var maxRequestLength = 4194304;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="documentTable">
            <table style="width: 100%;">
                <tr>
                    <td class="info">
                        <table>
                            <tr>
                                <th>MBL#/MAWB#</th>
                                <td>
                                    <asp:Label ID="txt_mblno" runat="server" Text=""></asp:Label></td>
                                <th>SUB HOUSE BL#</th>
                                <td>
                                    <asp:Label ID="txt_sblno" runat="server" Text=""></asp:Label></td>
                                <th>HBL#/HAWB#</th>
                                <td>
                                    <asp:Label ID="txt_hblno" runat="server" Text=""></asp:Label></td>                                
                                <th>Login User</th>
                                <td>&nbsp;<asp:Label ID="txt_username" runat="server" Text=""></asp:Label></td>
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
                    </td>
                </tr>
            </table>
        </div>
        <div id="doblk">
            <h3>Documents</h3>
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
                            <td></td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" class="auto-style1">
                                <asp:Button ID="btnD1" runat="server" Text="Download Selected" CssClass="cmdButton" />
                                &nbsp;<asp:Button ID="btnD2" runat="server" Text="Download All" CssClass="cmdButton" />
                                &nbsp;<asp:Button ID="btnD3" runat="server" Text="Close" CssClass="cmdButton" />
                                &nbsp;<asp:Button ID="btnD4" runat="server" Text="Delete Selected" CssClass="cmdButton" Visible="False"  />
                                &nbsp;</td>
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
            <h3>Trash</h3>
            <div>
                    <table style="width: 100%;">

                        <tr>
                            <td colspan="3">

                                <asp:GridView ID="G2" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="txt_FilRefId2" runat="server" Value='<%# Bind("FilRefId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FileType" HeaderText="File Type" />
                                        <asp:TemplateField HeaderText="FileName" SortExpression="FileName">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="txt_FileName2" runat="server" Text='<%# Bind("FileName") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FullFileName" HeaderText="File Full Name" Visible="False" />
                                        <asp:BoundField DataField="DeletedBy" HeaderText="Deleted By" />
                                        <asp:BoundField DataField="DeletedDate" HeaderText="Deleted Date" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>




                            </td>
                        </tr>


                    </table>
                </div>
             </div>

        <div id="upblk">
            <h3>Uploader</h3>
            <div id="tabs">
                <ul>
                    <li><a href="#upd1">Traditional Uploader</a></li>
                    <li><a href="#upd2">Drag&Drop Uploader</a></li>
                </ul>
                <div id="upd1">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%; padding: 0px; margin: 0px;">
                                    <tr>
                                        <td>
                                            <table class="upfile" style="width: 100%;">
                                                <colgroup>
                                                    <col style="width: 80px;" />
                                                    <col style="width: 80px; text-align: left;" />
                                                    <col />
                                                    <col style="width: 80px;" />
                                                </colgroup>
                                                <tr>
                                                    <th>File Type</th>
                                                    <th>Issue To</th>
                                                    <th>File</th>
                                                    <th>Status</th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ft1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="agt1" runat="server"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="updr1" runat="server"></asp:FileUpload>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="sts1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ft2" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="agt2" runat="server"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="updr2" runat="server"></asp:FileUpload>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="sts2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ft3" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="agt3" runat="server"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="updr3" runat="server"></asp:FileUpload>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="sts3" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ft4" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="agt4" runat="server"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="updr4" runat="server"></asp:FileUpload>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="sts4" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ft5" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="agt5" runat="server"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="updr5" runat="server"></asp:FileUpload>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="sts5" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="bunUpload" runat="server" Text="Upload" CssClass="cmdButton" />&nbsp;
                                <asp:Button ID="bunRefresh" runat="server" CssClass="cmdButton" Text="Refresh" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="upd2">
                    <div id="dropZone">
                        Master Bill
                    </div>
                    <div id="progressZone"></div>
                    <div id="dropZone1">
                        Packing List
                    </div>
                    <div id="progressZone1"></div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            var dropZone;
            var dropZone1;
            var uploadedFiles = 0;
            var filesDropped;

            function filetypeHelper(ft) {
                var aid = ft[0].id.replace("ft", "agt");
                var uid = ft[0].id.replace("ft", "updr");
                var agt = document.getElementById(aid);
                var up = document.getElementById(uid);

                var ftyp = ft.val();
                if ((ftyp == "FL") || (ftyp == "USV")) {
                    $(agt).css("display", "");
                }
                else {
                    $(agt).css("display", "none");
                };

                agt.selectedIndex = 0;
                $(up).css("display", (ft[0].selectedIndex > 0 ? "" : "none"));
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
                            if (uploadedFiles >= filesDropped) {
                                dropZone.removeClass('hover');
                                dropZone.text('Drop Files Here.');
                            }
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



            $(document).ready(function () {
                //$(".datepicker").datepicker();
                $("#doblk").accordion({ collapsible: true, active: 0 });
                $("#trash").accordion({ collapsible: true, active: 1 });
                $("#upblk").accordion({ collapsible: true, active: 0 });
                $("#tabs").tabs({ heightStyle: "fill" });

                $('[id*="agt"]').css("display", "none");
                $('[id*="updr"]').css("display", "none");
                $('[id*="ft"]').on("change", function (e) {
                    filetypeHelper($(this));
                });

                dropZone = $('#dropZone');
                dropZone1 = $('#dropZone1');
                dropZone.removeClass('error');
                dropZone1.removeClass('error');

                // Check if window.FileReader exists to make 
                // sure the browser supports file uploads
                if (typeof (window.FileReader) == 'undefined') {
                    dropZone.text('Browser Not Supported!');
                    dropZone1.text('Browser Not Supported!');
                    dropZone.addClass('error');
                    dropZone1.addClass('error');
                    return;
                }

                // Add a nice drag effect
                dropZone[0].ondragover = function () {
                    dropZone.addClass('hover');
                    return false;
                };

                dropZone1[0].ondragover = function () {
                    dropZone1.addClass('hover');
                    return false;
                };

                // Remove the drag effect when stopping our drag
                dropZone[0].ondragend = function () {
                    dropZone.removeClass('hover');
                    return false;
                };

                dropZone1[0].ondragend = function () {
                    dropZone1.removeClass('hover');
                    return false;
                };

                // The drop event handles the file sending
                dropZone[0].ondrop = function (event) {
                    // Stop the browser from opening the file in the window
                    event.preventDefault();

                    // Get the file and the file reader
                    var files = event.dataTransfer.files;

                    // Assign how many files were dropped to a variable
                    filesDropped = files.length;

                    // Empty the progressZone div
                    $('#progressZone').empty();

                    // Loop through dropped files
                    for (var i = 0; i < files.length; i++) {
                        var file = files[i];

                        // Add a div for each file being uploaded
                        $('#progressZone').append('<div id="progress' + i + '"></div>');

                        // Validate file size
                        if (file.size > maxRequestLength) {
                            $('#progress' + i).html(' \
                            <div class="fileName">' + trimString(file.name) + '</div> \
                            <div class="barError"></div> \
                            <div class="uploadError">Error: File Too Large!</div>');
                            uploadedFiles++;
                            continue;
                        }

                        // Report upload in progress
                        dropZone.text('Upload In Progress...');

                        // Send the file
                        uploadFile(file, i);
                    }
                };

                // The drop event handles the file sending
                dropZone1[0].ondrop = function (event) {
                    // Stop the browser from opening the file in the window
                    event.preventDefault();

                    // Get the file and the file reader
                    var files = event.dataTransfer.files;

                    // Assign how many files were dropped to a variable
                    filesDropped = files.length;

                    // Empty the progressZone div
                    $('#progressZone1').empty();

                    // Loop through dropped files
                    for (var i = 0; i < files.length; i++) {
                        var file = files[i];

                        // Add a div for each file being uploaded
                        $('#progressZone1').append('<div id="progress1' + i + '"></div>');

                        // Validate file size
                        if (file.size > maxRequestLength) {
                            $('#progress1' + i).html(' \
                            <div class="fileName">' + trimString(file.name) + '</div> \
                            <div class="barError"></div> \
                            <div class="uploadError">Error: File Too Large!</div>');
                            uploadedFiles++;
                            continue;
                        }

                        // Report upload in progress
                        dropZone1.text('Upload In Progress...');

                        // Send the file
                        uploadFile1(file, i);
                    }
                };
            });
        </script>
        <asp:Literal ID="postjs" runat="server"></asp:Literal>
    </form>
</body>
</html>

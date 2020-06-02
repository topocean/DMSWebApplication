<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TUploader.ascx.vb" Inherits="WebApplication1.TUploader" %>

<style type="text/css">
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
</style>

<div>
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
                            <asp:DropDownList ID="ft3" runat="server" /></td>
                        <td>
                            <asp:DropDownList ID="agt3" runat="server">
                            </asp:DropDownList>
                        </td>

                        <td>
                            <asp:FileUpload ID="updr3" runat="server"></asp:FileUpload></td>

                        <td>
                            <asp:Label ID="sts3" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ft4" runat="server" /></td>
                        <td>
                            <asp:DropDownList ID="agt4" runat="server">
                            </asp:DropDownList>
                        </td>

                        <td>
                            <asp:FileUpload ID="updr4" runat="server"></asp:FileUpload></td>

                        <td>
                            <asp:Label ID="sts4" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ft5" runat="server" /></td>
                        <td>
                            <asp:DropDownList ID="agt5" runat="server">
                            </asp:DropDownList>
                        </td>

                        <td>
                            <asp:FileUpload ID="updr5" runat="server"></asp:FileUpload></td>

                        <td>
                            <asp:Label ID="sts5" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <asp:Button ID="bunUpload" runat="server" Text="Upload" CssClass="cmdButton" OnClick="<script language='javascript'> { self.close() }</script>" />
                &nbsp;<asp:Button ID="bunRefresh" runat="server" CssClass="cmdButton" Text="Refresh" />
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
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

    $(document).ready(function () {
        $('[id*="agt"]').css("display", "none");
        $('[id*="updr"]').css("display", "none");
        $('[id*="ft"]').on("change", function (e) {
            filetypeHelper($(this));
        });
    });
</script>

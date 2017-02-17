<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rptpassportexp.aspx.cs" Inherits="Employee_System.ReportPage.Rptpassportexp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="//code.jquery.com/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            debugger;
            $("#buttonLoadUploadStatus").click(function () {
                debugger;
                var ClientId;
                ClientId = $("#[id*=dropDownListCountry]").val();
                var challanMonth = $("#[id*=dropDownListUPStatusChallanMonth]").val();
                if (isNaN(ClientId)) {
                    ClientId = 0;
                }
                $.ajax({
                    url: "ReportView.aspx/GetDashBoardItem",
                    data: {
                        iMonth: parseInt(challanMonth),
                        iClientID: parseInt(ClientId),
                    },
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    type: "GET",
                    success: function (data) {

                        //$("#tableUPStatus tr").empty();
                        //$("#tableUPStatus").append("<tr><td>Sr. No.</td><td>Contractor Name</td><td>Branch Name</td><td>Frequency</td><td>Activity</td></tr>");
                        if (data.d.length > 0) {
                            for (var iCount = 0; iCount < data.d.length; iCount++) {
                            }
                        }
                        //        $("#tableUPStatus").append("<tr><td>" + data.d[iCount].SrNo + "</td><td>" + data.d[iCount].strContractorName + "</td><td>" + data.d[iCount].strBranchName + "</td><td>" + data.d[iCount].strFrequency + "</td><td>" + data.d[iCount].strActivityName + "</td></tr>");
                        //    }
                        //}
                        //else {
                        //    $("#tableUPStatus").append("<tr><td colspan='5'>No records found</td></tr>");
                        //}
                    },
                    error: function (xhr) {

                        console.log(xhr.responseText);
                    },
                    always: function () {

                    }

                });
            });






            function BindDropDownList(dropDownListId, strTableName, strTextColumn, strValueColumn, strWhere, strDefaultValue) {
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "Handler/DropDownListHandler.ashx",
                    data: {
                        strTableName: strTableName,
                        strTextColumn: strTextColumn,
                        strValueColumn: strValueColumn,
                        strWhere: strWhere
                    },
                    dataType: "json",
                    success: function (data) {

                        $('#' + dropDownListId).empty();
                        $('#' + dropDownListId).append($("<option></option>").val('0').html(strDefaultValue));
                        $.each(data, function (key, value) {
                            $('#' + dropDownListId).append($("<option></option>").val(value.Value).html(value.Text));
                        });
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });
            }
        });
    </script>
</head>
<body>
    <script src="//code.jquery.com/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            debugger;
            $("#buttonLoadUploadStatus").click(function () {
                debugger;
                var ClientId;
                ClientId = $("#[id*=dropDownListCountry]").val();
                var challanMonth = $("#[id*=dropDownListUPStatusChallanMonth]").val();
                if (isNaN(ClientId)) {
                    ClientId = 0;
                }
                $.ajax({
                    url: "ReportView.aspx/GetDashBoardItem",
                    data: {
                        iMonth: parseInt(challanMonth),
                        iClientID: parseInt(ClientId),
                    },
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    type: "GET",
                    success: function (data) {

                        //$("#tableUPStatus tr").empty();
                        //$("#tableUPStatus").append("<tr><td>Sr. No.</td><td>Contractor Name</td><td>Branch Name</td><td>Frequency</td><td>Activity</td></tr>");
                        if (data.d.length > 0) {
                            for (var iCount = 0; iCount < data.d.length; iCount++) {
                            }
                        }
                        //        $("#tableUPStatus").append("<tr><td>" + data.d[iCount].SrNo + "</td><td>" + data.d[iCount].strContractorName + "</td><td>" + data.d[iCount].strBranchName + "</td><td>" + data.d[iCount].strFrequency + "</td><td>" + data.d[iCount].strActivityName + "</td></tr>");
                        //    }
                        //}
                        //else {
                        //    $("#tableUPStatus").append("<tr><td colspan='5'>No records found</td></tr>");
                        //}
                    },
                    error: function (xhr) {

                        console.log(xhr.responseText);
                    },
                    always: function () {

                    }

                });
            });






            function BindDropDownList(dropDownListId, strTableName, strTextColumn, strValueColumn, strWhere, strDefaultValue) {
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "Handler/DropDownListHandler.ashx",
                    data: {
                        strTableName: strTableName,
                        strTextColumn: strTextColumn,
                        strValueColumn: strValueColumn,
                        strWhere: strWhere
                    },
                    dataType: "json",
                    success: function (data) {

                        $('#' + dropDownListId).empty();
                        $('#' + dropDownListId).append($("<option></option>").val('0').html(strDefaultValue));
                        $.each(data, function (key, value) {
                            $('#' + dropDownListId).append($("<option></option>").val(value.Value).html(value.Text));
                        });
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });
            }
        });
    </script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="Scrpt" runat="server"></asp:ScriptManager>


        <table class="table table-striped table-bordered table-hover">
            <%--<tr>
                <td>
                    <asp:DropDownList ID="dropDownListCountry" runat="server">
                        <asp:ListItem Text="--Select Month--" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <%--<tr>
                <td class="style1">
                    <em><span class="style2">Activity Status</span> </em>
                </td>
                <td></td>
                <td></td>
            </tr>--%>
            <tr>
                <td>Comapny:
                <asp:DropDownList ID="drpcompany" runat="server" CssClass="form-control">
                    <%-- <asp:ListItem Text="--Select Month--" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                    <asp:ListItem Text="30" Value="30"></asp:ListItem>--%>
                </asp:DropDownList>
                </td>
                <td>Passport Expired in <asp:TextBox ID="txtdays" runat="server" CssClass="form-control"></asp:TextBox> Days</td>
                <td>
                    <asp:Button ID="btn" runat="server" Text="Go" OnClick="btn_Click" />
                </td>

            </tr>
            <tr>

                <td>
                    <%--<input type="button" id="buttonLoadUploadStatus" value="GO" />--%>
                </td>
            </tr>

        </table>


        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>






    </form>


</body>
</html>

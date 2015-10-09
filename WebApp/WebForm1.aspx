<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblShow" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnOpenDB" runat="server" Text="打开Redis" OnClick="btnOpenDB_Click" />&nbsp;&nbsp;
        <br />
            <asp:Button ID="btnSetValue" runat="server" Text="显示全部" OnClick="btnSetValue_Click" />&nbsp;&nbsp;
    <asp:Label ID="lblPeople" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label1" runat="server" Text="姓名："></asp:Label><asp:TextBox ID="txtName" runat="server" Width="100px"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="部门："></asp:Label><asp:TextBox ID="txtPosition" runat="server" Width="100px"></asp:TextBox>
            <asp:Button ID="btnInsert" runat="server" Text="写入数据" OnClick="btnInsert_Click" />
            <br />
            <asp:Label ID="Label3" runat="server" Text="ID："></asp:Label><asp:TextBox ID="txtRedisId" runat="server" Width="100px"></asp:TextBox>
            <asp:Button ID="btnDel" runat="server" Text="删除数据" OnClick="btnDel_Click" />
            <br />
            <asp:Label ID="Label4" runat="server" Text="部门："></asp:Label><asp:TextBox ID="txtScreenPosition" runat="server" Width="100px"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="查询数据" OnClick="btnSearch_Click" />

        </div>
    </form>
</body>
</html>

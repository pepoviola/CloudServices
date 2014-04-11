<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="UI.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <!-- Bootstrap core CSS -->
    <link href="content/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="content/css/login/login.css" rel="stylesheet"/>
</head>
<body>
    <div class="container">
	<div class="login-container">
            <div id="output"></div>
            <div class="form-box">
                <form id="login" runat="server">
                    <input name="user" type="text" placeholder="username" />
                    <input name="password" type="password" placeholder="password" />
                    <button class="btn btn-info btn-block login" type="submit">Login</button>
                </form>
            </div>
        </div>
        
    </div>
    <script src="scripts/jquery-2.1.0.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="scripts/login/login.js"></script>
</body>
</html>

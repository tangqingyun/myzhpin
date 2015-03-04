<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="JqueryMobileTest.WebForm1" %>
<!DOCTYPE html>
<html>
<head>
<link rel="stylesheet" href="/css/jquery.mobile-1.3.2.min.css">
<script src="/js/jquery-1.8.3.min.js"></script>
<script src="/js/jquery.mobile-1.3.2.min.js"></script>
</head>
<body>
<div data-role="page">

  <div data-role="header">
    <div data-role="navbar">
    <ul>
      <li><a href="#anylink" data-icon="home">首页</a></li>
      <li><a href="#anylink">公寓</a></li>
      <li><a href="#anylink">写字楼</a></li>
    </ul>
    </div>
  </div>

  <div data-role="content">
    <p>
       <a href="#anylink" data-role="button" data-icon="search">Search</a>
    </p>
  </div>

  <div data-role="footer">
    <h1>页脚文本</h1>
  </div>

</div>
</body
</html>


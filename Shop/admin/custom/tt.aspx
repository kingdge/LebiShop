<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shop.Bussiness.PageBase.AdminCustomPageBase.cs" Inherits="Shop.Bussiness.ShopPage" %>
<%@ Import Namespace="Shop.Bussiness" %>
<%@ Import Namespace="Shop.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%Table="Lebi_User";Where="";PageSize=20;pageindex=Rint("page");RecordCount=B_Lebi_User.Counts(Where);int FtJF_index=1;
List<Lebi_User> FtJFs = B_Lebi_User.GetList(Where, Order,PageSize ,pageindex);foreach (Lebi_User FtJF in FtJFs){%>
<%=FtJF.UserName%>
<%FtJF_index++;}%>
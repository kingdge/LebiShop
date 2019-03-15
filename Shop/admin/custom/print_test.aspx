<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shop.Bussiness.PageBase.AdminCustomPageBase.cs" Inherits="Shop.Bussiness.AdminCustomPageBase" %>
<%@ Import Namespace="Shop.Bussiness" %>
<%@ Import Namespace="Shop.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%=site.title%>
<%Table="Lebi_User";Where="";PageSize=20;pageindex=Rint("page");RecordCount=B_Lebi_User.Counts(Where);int nwhV_index=1;
List<Lebi_User> nwhVs = B_Lebi_User.GetList(Where, Order,PageSize ,pageindex);foreach (Lebi_User nwhV in nwhVs){%>
<%=nwhV.UserName%>
<%nwhV_index++;}%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Hotcakes.Modules.ProcessImagesModule.View" %>
<h1><%=GetLocalizedString("ProcessImagesTitle") %></h1>
<p><%=GetLocalizedString("ProcessImagesMessage") %></p>
<p><asp:Button runat="server" ID="btnProcessImages" CssClass="dnnPrimaryAction" OnClick="lnkProcessImages_OnClick" CausesValidation="false"/></p>
<asp:Panel runat="server" ID="pnlImportSummary">
    <asp:PlaceHolder runat="server" ID="phImportSummary"/>
</asp:Panel>
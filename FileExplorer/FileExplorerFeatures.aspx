<%@ Page Language="C#" MasterPageFile="~/Site.Master" Title="FileExplorer" AutoEventWireup="true" CodeBehind="FileExplorerFeatures.aspx.cs" Inherits="SyncfusionASPNETApplication11.FileExplorerFeatures" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>FileExplorer with support to open document files</h2>
    <br />
    <div id="ControlRegion">
        <ej:FileExplorer ID="fileexplorer" runat="server" CssClass="myFileBrowser" IsResponsive="true" Width="100%" AjaxAction="FileExplorerFeatures.aspx/FileActionDefault" Path="~/content/images/FileExplorer/" ClientSideOnBeforeOpen="beforeOpen" Layout="Tile">
        </ej:FileExplorer>
        <script>    
            function beforeOpen(args) {
                if (args.path.includes(".pdf")) {
                    var items = ~ej.PdfViewer.ToolbarItems.PrintTools & ~ej.PdfViewer.ToolbarItems.DownloadTool;
                    var ws = window.open("", '_blank', "width=800,height=600,location=no,menubar=no,status=no,titilebar=no,resizable=no")
                    ws.document.write('<!DOCTYPE html><html><head><title>PdfViewer</title><link rel="stylesheet" type="text/css" href="https://cdn.syncfusion.com/16.4.0.54/js/web/flat-azure/ej.web.all.min.css"><script src="https://cdn.syncfusion.com/js/assets/external/jquery-2.1.4.min.js"><\/script><script src="https://cdn.syncfusion.com/16.4.0.54/js/web/ej.web.all.js"><\/script><\/head><body>');
                    ws.document.write('<div style="width:100%;min-height:570px"><div id="container"><\/div><\/div>')
                    ws.document.write("<script>$(function(){ $('#container').ejPdfViewer({ serviceUrl: '../api/PdfViewer', documentPath: '" + args.path + "', toolbarSettings: { toolbarItem:'" + items + "'}})})<\/script>")
                    ws.document.write('<\/body><\/html>');
                    ws.document.close();
                } 
            }
        </script>
    </div>
</asp:Content>

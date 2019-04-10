#region Copyright Syncfusion Inc. 2001-2016.
// Copyright Syncfusion Inc. 2001-2016. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Newtonsoft.Json;
using Syncfusion.EJ.PdfViewer;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SyncfusionASPNETApplication11
{
    public class PdfViewerController : ApiController
    {
        public object Load(Dictionary<string, string> jsonResult)
        {
            PdfViewerHelper helper = new PdfViewerHelper();
            //load the multiple document from client side 
            if (jsonResult.ContainsKey("newFileName"))
            {
                var name = jsonResult["newFileName"];
                var pdfName = name.ToString();
                string urlLink = Url.Content("~/") + "content/images/FileExplorer/" + pdfName;

                var WebClient = new WebClient();

                //converts the url to byte array 

                byte[] pdfDoc = WebClient.DownloadData(urlLink);

                //loads the byte array in PDF Viewer  

                helper.Load(pdfDoc);
            }
            else
            {
                if (jsonResult.ContainsKey("isInitialLoading"))
                {
                    if (jsonResult.ContainsKey("file"))
                    {
                        string urlLink = Url.Content("~/") + jsonResult["file"].ToString().Substring(jsonResult["file"].ToString().IndexOf("/") + 1);

                        var WebClient = new WebClient();

                        //converts the url to byte array 

                        byte[] pdfDoc = WebClient.DownloadData(urlLink);

                        //loads the byte array in PDF Viewer  

                        helper.Load(pdfDoc);
                    }
                    else
                    {
                        string urlLink = Url.Content("~/") + "content/images/FileExplorer/pdf/HTTP Succinctly.pdf";

                        var WebClient = new WebClient();

                        //converts the url to byte array 

                        byte[] pdfDoc = WebClient.DownloadData(urlLink);

                        //loads the byte array in PDF Viewer  

                        helper.Load(pdfDoc);
                    }
                }
            }

            string output = JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult));
            return output;
        }
        public object Download(Dictionary<string, string> jsonResult)
        {
            PdfViewerHelper helper = new PdfViewerHelper();
            return helper.GetDocumentData(jsonResult);
        }
        public object FileUpload(Dictionary<string, string> jsonResult)
        {
            PdfViewerHelper helper = new PdfViewerHelper();

            if (jsonResult.ContainsKey("uploadedFile"))
            {
                var fileurl = jsonResult["uploadedFile"];
                byte[] byteArray = Convert.FromBase64String(fileurl);
                MemoryStream stream = new MemoryStream(byteArray);
                helper.Load(stream);
            }
            string output = JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult));
            return output;
        }
    }
}
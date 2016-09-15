/*
' Copyright (c) 2016 Hotcakes Commerce, LLC
'  All rights reserved.
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy 
' of this software and associated documentation files (the "Software"), to deal 
' in the Software without restriction, including without limitation the rights 
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
' copies of the Software, and to permit persons to whom the Software is 
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all 
' copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
' SOFTWARE.
*/

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using DotNetNuke.Services.Exceptions;
using Hotcakes.Commerce.Catalog;
using Hotcakes.Commerce.Extensions;
using Hotcakes.Commerce.Orders;
using Hotcakes.Commerce.Storage;
using Hotcakes.Commerce.Urls;

namespace Hotcakes.Modules.ProcessImagesModule
{
    public partial class View : ProcessImagesModuleBase
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack) BindData();
            }
            catch (Exception exc) 
            {
                // Module failed to load
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void lnkProcessImages_OnClick(object sender, EventArgs e)
        {
            ProcessImages();
        }

        #endregion

        #region Helper Methods

        private void BindData()
        {
            LocalizeModule();

            
        }

        private void LocalizeModule()
        {
            btnProcessImages.Text = GetLocalizedString("btnProcessImages");
        }

        private void ProcessImages()
        {
            // get an instance of the store application
            var HccApp = HccAppHelper.InitHccApp();
            // get a collection of the products in the store
            var products = HccApp.CatalogServices.Products.FindAllPaged(1, int.MaxValue);
            var sb = new StringBuilder();

            // a title simply to give context of the text to follow
            sb.Append("<blockquote style=\"background-color: #f1f1f1; padding: 1.5em;\">");
            sb.AppendFormat("<h2>{0}</h2>", GetLocalizedString("ProcessTitle"));
            
            if (products != null && products.Count > 0)
            {
                // begin processing images for each image
                var importPath = DiskStorage.GetStoreDataVirtualPath(HccApp.CurrentStore.Id) + "import/";

                // display where the module is looking for images
                sb.AppendFormat("<p><strong>{0}:</strong> {1}</p>", GetLocalizedString("ImportPathLabel"), importPath);

                // iterate through each product in the store to find images
                foreach (var product in products)
                {
                    // display the product being processed
                    sb.AppendFormat("<h3>Importing ({0}) {1}</h3>", product.Sku, product.ProductName);

                    // put together the expected path of the image
                    var filePath = importPath + product.ImageFileMedium;

                    // display the image path to the user
                    sb.AppendFormat("<p><strong>{0}</strong>: <a href=\"{1}\" target=\"_blank\">{1}</a></p>", GetLocalizedString("FilePathLabel"), filePath.Replace("~", string.Empty), filePath);

                    // determine if the import file/path exists
                    if (File.Exists(Server.MapPath(filePath)))
                    {
                        // let the user know the image import is beginning
                        sb.AppendFormat("<p>{0}</p>", GetLocalizedString("FileImportBegin"));

                        // import the image from the import location
                        DiskStorage.CopyProductImage(HccApp.CurrentStore.Id, product.Bvin, filePath, product.ImageFileMedium);

                        // let the user know the import was completed
                        sb.AppendFormat("<p>{0}</p>", GetLocalizedString("FileImported"));
                    }
                    else
                    {
                        // the image was not found in the import location
                        sb.AppendFormat("<p style=\"color: #ff0000\">{0}</p>", GetLocalizedString("FileNotFound"));
                    }
                }
            }
            else
            {
                // no products were found
                sb.AppendFormat("<p>{0}</p>", GetLocalizedString("NoProducts"));
            }

            sb.Append("</blockquote>");

            // render the output to the user
            phImportSummary.Controls.Add(new LiteralControl(sb.ToString()));
        }

        #endregion
    }
}
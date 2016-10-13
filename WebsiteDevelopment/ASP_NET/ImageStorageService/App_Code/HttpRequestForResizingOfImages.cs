using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for HttpRequestForResizingOfImages
/// </summary>
public class HttpRequestForResizingOfImages : IHttpHandler
{
	public HttpRequestForResizingOfImages()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        string ImageUrl = context.Request.QueryString["url"];
        //get image width to be resized from querystring
        string ImageWidth = context.Request.QueryString["width"];
        //get image height to be resized from querystring
        string ImageHeight = context.Request.QueryString["height"];
        if (ImageUrl != string.Empty && ImageHeight != string.Empty && ImageWidth != string.Empty && (ImageWidth != null && ImageHeight != null))
        {
            if (!System.Web.UI.WebControls.Unit.Parse(ImageWidth).IsEmpty && !System.Web.UI.WebControls.Unit.Parse(ImageHeight).IsEmpty)
            {
                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                //creat BitMap object from image path passed in querystring
                Bitmap BitmapForImage = new Bitmap(context.Server.MapPath(ImageUrl));
                //create unit object for height and width. This is to convert parameter passed in differen unit like pixel, inch into generic unit.
                System.Web.UI.WebControls.Unit WidthUnit = System.Web.UI.WebControls.Unit.Parse(ImageWidth);
                System.Web.UI.WebControls.Unit HeightUnit = System.Web.UI.WebControls.Unit.Parse(ImageHeight);
                //Resize actual image using width and height paramters passed in querystring
                Image CurrentThumbnail = BitmapForImage.GetThumbnailImage(Convert.ToInt16(WidthUnit.Value), Convert.ToInt16(HeightUnit.Value), myCallback, IntPtr.Zero);
                //Create memory stream and save resized image into memory stream
                MemoryStream objMemoryStream = new MemoryStream();
                if (ImageUrl.EndsWith(".png"))
                {
                    context.Response.ContentType = "image/png";
                    CurrentThumbnail.Save(objMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
                }
                else if (ImageUrl.EndsWith(".jpg"))
                {
                    context.Response.ContentType = "image/jpeg";
                    CurrentThumbnail.Save(objMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else if (ImageUrl.EndsWith(".gif"))
                {
                    context.Response.ContentType = "image/gif";
                    CurrentThumbnail.Save(objMemoryStream, System.Drawing.Imaging.ImageFormat.Gif);
                }

                //Declare byte array of size memory stream and read memory stream data into array
                byte[] imageData = new byte[objMemoryStream.Length];
                objMemoryStream.Position = 0;
                objMemoryStream.Read(imageData, 0, (int)objMemoryStream.Length);

                //send contents of byte array as response to client (browser)
                context.Response.BinaryWrite(imageData);
            }
        }
        else
        {
            //if width and height was not passed as querystring, then send image as it is from path provided.
            context.Response.WriteFile(context.Server.MapPath(ImageUrl));
        }
    }

    public bool ThumbnailCallback()
    {
        return false;
    } 
}
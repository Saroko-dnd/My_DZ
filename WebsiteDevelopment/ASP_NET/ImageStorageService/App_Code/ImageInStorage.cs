using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для ImageInStorage
/// </summary>
public class ImageInStorage
{
    private string fileNameWithoutExtension;
    private string imageUrl;

    public string FileNameWithoutExtension
    {
        get
        {
            return fileNameWithoutExtension;
        }

        set
        {
            fileNameWithoutExtension = value;
        }
    }

    public string ImageUrl
    {
        get
        {
            return imageUrl;
        }

        set
        {
            imageUrl = value;
        }
    }

    public ImageInStorage(string NewFileNameWithoutExtension, string NewImageUrl)
    {
        FileNameWithoutExtension = NewFileNameWithoutExtension;
        ImageUrl = NewImageUrl;
    }
}
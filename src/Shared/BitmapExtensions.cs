using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace src.Shared
{
    public static class BitmapExtensions
    {
        //
        // https://stackoverflow.com/questions/32066893/changing-datetaken-of-a-photo
        //
        public static void ModifyDateTaken( this Bitmap image, DateTime newDate)
        {
            // PropertyItem class has no public constructors. One way to work around this restriction is 
            // to obtain a PropertyItem by retrieving the PropertyItems property value or calling the 
            // GetPropertyItem method of an Image that already has property items
            var propItems = image.PropertyItems;
            var encoding = Encoding.UTF8;

            var prop9003 = propItems.Where(a => a.Id.ToString("x") == "9003").FirstOrDefault();
            if (prop9003 != null)
            {
                prop9003.Value = encoding.GetBytes(newDate.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
                image.SetPropertyItem(prop9003);
            }
        }        
    }
}

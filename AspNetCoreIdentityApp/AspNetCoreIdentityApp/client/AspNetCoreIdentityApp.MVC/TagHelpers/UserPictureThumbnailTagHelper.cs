using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace AspNetCoreIdentityApp.MVC.TagHelpers
{
    public class UserPictureThumbnailTagHelper : TagHelper
    {
        public string Picture { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";

            if (!string.IsNullOrEmpty(Picture))
                output.Attributes.SetAttribute("src", "/userpictures/"+Picture);
            
            else
                output.Attributes.SetAttribute("src", "/userpictures/defaultuserpicture.jpg");
            

            base.Process(context, output);
        }
    }
}

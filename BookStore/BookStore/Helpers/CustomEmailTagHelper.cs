using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookStore.Helpers
{
    public class CustomEmailTagHelper : TagHelper
    {
        public string Url { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.Add("id", "custom-email-id");
            output.Attributes.SetAttribute("href", $"{Url}");
            output.Content.SetContent("Githup Profile");
        }
    }
}

namespace Infrastructure.TagHelpers
{
	[Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElement
			(tag: "section-page-body",
			TagStructure = Microsoft.AspNetCore.Razor.TagHelpers.TagStructure.NormalOrSelfClosing)]
	public class SectionPageBodyTagHelper :
		Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
	{
		public SectionPageBodyTagHelper() : base()
		{
		}

		//public override void Process
		//	(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
		//	Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
		//{
		//	base.Process(context, output);
		//}

		public async override System.Threading.Tasks.Task ProcessAsync
			(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
			Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
		{
			// **************************************************
			var originalContents =
				await
				output.GetChildContentAsync();
			// **************************************************
			// **************************************************
			var body =
				new Microsoft.AspNetCore.Mvc
				.Rendering.TagBuilder(tagName: "div");

			body.AddCssClass(value: "mt-8");

			body.InnerHtml.AppendHtml(content: originalContents);
			//body.InnerHtml.AppendHtml(content: horizontalRule);
			// **************************************************
			// **************************************************
			output.TagName = null;

			output.TagMode =
				Microsoft.AspNetCore.Razor
				.TagHelpers.TagMode.StartTagAndEndTag;

			output.Content.SetHtmlContent(htmlContent: body);
			// **************************************************
		}
	}
}

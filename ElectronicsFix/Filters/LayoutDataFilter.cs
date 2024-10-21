using Microsoft.AspNetCore.Mvc.Filters;

public class LayoutDataFilter : IAsyncActionFilter
{
    // Fields
    private readonly ISettings oClsSettings;
    private readonly DepiProjectContext _Context;

    // Constructor
    public LayoutDataFilter(ISettings oClsSettings, DepiProjectContext _Context)
    {
        this.oClsSettings = oClsSettings;
        this._Context = _Context;
    }

    // Method
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {


        var Logo = await _Context.TbSettings
          .Select(n => n.Logo).FirstOrDefaultAsync();

        var item = oClsSettings.GetById(1);

        // set data in layout
        var viewModel = new LayoutViewModel
        {
            LogoUrl = item.Logo,
            Copyright = item.Copyright,
            Description = item.Description,
            Mail = item.Mail,
            Phone = item.Phone,
            Facebook_Link = item.Facebook_Link,
            Googol_Link = item.Googol_Link,
            Twitter_Link = item.Googol_Link,
            Instagram_Link = item.Instagram_Link,
            LinkedIn_Link = item.LinkedIn_Link
        };

        // Attach ViewData to the controller
        var controller = context.Controller as Controller;
        if (controller != null)
        {
            controller.ViewData["LayoutViewModel"] = viewModel;
        }

        await next();
    }
}

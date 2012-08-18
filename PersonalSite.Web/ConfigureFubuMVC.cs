using System.Linq;
using FluentValidation.Results;
using FubuMVC.Core;
using FubuMVC.Core.Runtime;
using FubuMVC.Spark;
using HtmlTags;
using PersonalSite.Extensions;
using PersonalSite.Web.Controllers;
using PersonalSite.Web.Validation;

namespace PersonalSite.Web
{
    public class ConfigureFubuMVC : FubuRegistry
    {
        public ConfigureFubuMVC()
        {
            // This line turns on the basic diagnostics and request tracing
            IncludeDiagnostics(true);

            // All public methods from concrete classes ending in "Controller"
            // in this assembly are assumed to be action methods
            Actions.IncludeClassesSuffixedWithController();

            // Policies
            Routes
                .IgnoreControllerNamesEntirely()
                .IgnoreMethodSuffix("Html")
                .RootAtAssemblyNamespace();

            Routes.HomeIs<HomeController>(h => h.Index());

            // Match views to action methods by matching
            // on model type, view name, and namespace
            //Views.TryToAttachWithDefaultConventions();

            this.UseSpark();
            Applies.ToThisAssembly();
            Output.ToJson.WhenCallMatches(action => action.Returns<AjaxResponse>());
            ApplyConvention<ValidationConfiguration>();
            ControllerStyle();
            HtmlConventionsForValidation();
        }

        private void ControllerStyle()
        {
            Actions.IncludeTypesNamed(x => x.EndsWith("Controller"));
            Views.TryToAttach(findViews => findViews.by_ViewModel());
            Routes
                .IgnoreControllerNamespaceEntirely()
                .ConstrainToHttpMethod(c => c.HasOutput && !c.OutputType().Name.ContainsAny("Output", "Input"), "Get")
                .ConstrainToHttpMethod(x => !x.HasInput || x.InputType().Name.Contains("Input"), "Get")
                .ConstrainToHttpMethod(x => x.HasInput && x.InputType().Name.Contains("Output"), "Post");
        }

        private void HtmlConventionsForValidation()
        {
            HtmlConvention(x => x.Editors.Always.Modify((request, tag) =>
            {
                var fubuRequest = request.Get<IFubuRequest>();
                var validationResult = fubuRequest.Get<ValidationResult>();
                if (validationResult.IsValid) return;
                var ul = new HtmlTag("ul");
                var liTags = validationResult.Errors.Where(error => error.PropertyName == request.Accessor.InnerProperty.Name).Select(vf => new HtmlTag("li", li => li.Text(vf.ErrorMessage)));
                ul.Append(liTags);
                tag.Append(ul);
            }));
        }
    }

    public class AjaxResponse
    {
        public bool Success { get; set; }
        public object Item { get; set; }
    }
}
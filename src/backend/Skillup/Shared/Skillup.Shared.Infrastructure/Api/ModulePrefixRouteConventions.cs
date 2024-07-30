using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Skillup.Shared.Infrastructure.Api
{
    internal class ModulePrefixRouteConventions : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var displayName = controller.DisplayName;
            var moduleName = displayName.Split("Skillup.Modules.")[1].Split(".Api")[0];

            controller.RouteValues["moduleName"] = moduleName;
            foreach (var selector in controller.Selectors)
            {
                var routeAttribute = selector.AttributeRouteModel;
                if (routeAttribute != null)
                {
                    routeAttribute.Template = $"{moduleName}/{routeAttribute.Template}";
                }
            }
        }
    }
}

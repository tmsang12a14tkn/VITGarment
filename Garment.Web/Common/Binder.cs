using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
namespace Garment.Web.Common
{
    public class HttpDateTimeBinder : System.Web.Http.ModelBinding.IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null) return false;
            var date = value.AttemptedValue;

            if (String.IsNullOrEmpty(date))
                return false;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, bindingContext.ValueProvider.GetValue(bindingContext.ModelName));
            try
            {
                bindingContext.Model = DateTime.Parse(date);
                return true;
            }
            catch (Exception)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, String.Format("\"{0}\" is invalid.", bindingContext.ModelName));
                return false;
            }
        }
    }
    //public class NullableDateTimeBinder : IModelBinder
    //{
    //    public object BindModel(HttpActionContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
    //        if (value != null)
    //        {
    //            var date = value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
    //            return date;
    //        }
    //        return null;
    //    }
    //}

    public class DateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            return value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
        }
    }

    public class NullableDateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            return value == null
                ? null
                : value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
        }
    }
}
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace DemoMVCAuth;
public class MultiContentTypeBinder<T> : IModelBinder where T : new()
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        string contentType = bindingContext.HttpContext.Request.ContentType;

        if (contentType.Contains("application/json"))
        {
            using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);
            var body = reader.ReadToEndAsync().Result;
            var model = JsonConvert.DeserializeObject<T>(body);
            bindingContext.Result = ModelBindingResult.Success(model);
        }
        else if (contentType.Contains("multipart/form-data") || contentType.Contains("application/x-www-form-urlencoded"))
        {
            var form = bindingContext.HttpContext.Request.Form;
            var model = new T();

            foreach (var property in typeof(T).GetProperties())
            {
                if (form.ContainsKey(property.Name))
                {
                    if (typeof(IConvertible).IsAssignableFrom(property.PropertyType))
                    {
                        var value = Convert.ChangeType(form[property.Name].ToString(), property.PropertyType);
                        property.SetValue(model, value);
                    }
                    else
                    {
                        property.SetValue(model, form[property.Name]);
                    }
                }
            }

            bindingContext.Result = ModelBindingResult.Success(model);
        }

        return Task.CompletedTask;
    }
}
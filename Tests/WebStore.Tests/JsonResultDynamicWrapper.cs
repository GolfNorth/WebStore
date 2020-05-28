using System;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Tests
{
    public class JsonResultDynamicWrapper : DynamicObject
    {
        private readonly object _resultObject;

        public JsonResultDynamicWrapper([NotNull] JsonResult resultObject)
        {
            if (resultObject == null) throw new ArgumentNullException(nameof(resultObject));
            _resultObject = resultObject.Value;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (string.IsNullOrEmpty(binder.Name))
            {
                result = null;
                return false;
            }

            var property = _resultObject.GetType().GetProperty(binder.Name);

            if (property == null)
            {
                result = null;
                return false;
            }

            result = property.GetValue(_resultObject, null);
            
            return true;
        }
    }
}
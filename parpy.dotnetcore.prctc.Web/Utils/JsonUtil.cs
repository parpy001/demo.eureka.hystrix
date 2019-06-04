using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace parpy.dotnetcore.prctc.Web.Utils
{
    public class JsonUtil
    {
        private static readonly string _date_format = "yyyy-MM-dd HH:mm:ss";

        public static string SerializeObject(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                ContractResolver = new NullToEmptyStringResolver(),
                DateFormatString = _date_format
            };

            return JsonConvert.SerializeObject(obj, setting);

        }


    }

    class NullToEmptyStringResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties()
                    .Select(p =>
                    {
                        var jp = base.CreateProperty(p, memberSerialization);
                        jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                        return jp;
                    }).ToList();
        }
    }

    class NullToEmptyStringValueProvider : IValueProvider
    {
        PropertyInfo _MemberInfo;
        public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
        {
            _MemberInfo = memberInfo;
        }

        public object GetValue(object target)
        {
            object result = _MemberInfo.GetValue(target);
            if (result == null && _MemberInfo.PropertyType == typeof(string))
            {
                return string.Empty;
            }
            return result;

        }

        public void SetValue(object target, object value)
        {
            _MemberInfo.SetValue(target, value);
        }
    }


}

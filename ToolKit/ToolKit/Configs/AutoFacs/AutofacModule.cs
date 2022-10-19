using Autofac;
using Autofac.Core;
using System.Reflection;

namespace ToolKit.Configs.AutoFacs
{
    public class AutofacModule : Autofac.Module
    {
        /// <summary>
        /// 添加属性注入
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .PropertiesAutowired(new AutowiredPropertySelector());

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .PropertiesAutowired(new AutowiredPropertySelector())
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }
    }
    /// <summary>
    /// 属性注入选择器
    /// </summary>
    public class AutowiredPropertySelector : IPropertySelector
    {
        /// <summary>
        /// 带有 AutowiredAttribute 特性的属性会进行属性注入
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            return propertyInfo.CustomAttributes.Any(it => it.AttributeType == typeof(AutowiredAttribute));
        }
    }
}

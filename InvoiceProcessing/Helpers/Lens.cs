using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProcessing.Helpers
{
    public static class Lens<TObject>
    {
        public static Lens<TObject, TProperty> New<TProperty>(Expression<Func<TObject, TProperty>> propertyGetter)
            => Lens<TObject, TProperty>.New(propertyGetter);
    }

    public class Lens<TObject, TProperty>
    {
        public Func<TObject, TProperty> Get { get; private set; }
        public Action<TObject, TProperty> Set { get; private set; }

        public Lens(Func<TObject, TProperty> getter, Action<TObject, TProperty> setter)
        {
            Get = getter;
            Set = setter;
        }

        public static Lens<TObject, TProperty> New(Expression<Func<TObject, TProperty>> propertyGetter)
        {
            var getter = propertyGetter.Compile();
            var setter = DeriveSetterLambda(propertyGetter);

            var lens = new Lens<TObject, TProperty>(getter, setter);
            return lens;
        }

        static Action<TObject, TProperty> DeriveSetterLambda(Expression<Func<TObject, TProperty>> propertyGetter)
        {
            /*string propName = GetPropertyName(propertyGetter);

            var paramTargetExp = Expression.Parameter(typeof(TObject));
            var paramValueExp = Expression.Parameter(typeof(TProperty));

            var propExp = Expression.PropertyOrField(paramTargetExp, propName);
            var assignExp = Expression.Assign(propExp, paramValueExp);

            var setter = Expression.Lambda<Action<TObject, TProperty>>(assignExp, paramTargetExp, paramValueExp);

            return setter.Compile();*/

            var member = (MemberExpression)propertyGetter.Body;
            var param = Expression.Parameter(typeof(string), "value");
            var set = Expression.Lambda<Action<TObject, TProperty>>(
                Expression.Assign(member, param), propertyGetter.Parameters[0], param);

            return set.Compile();
        }

        static string GetPropertyName(Expression<Func<TObject, TProperty>> exp)
        {
            var propExp = (MemberExpression)exp.Body;
            var n = propExp.Member.Name;
            return n;
        }
    }
}

using ParserCore.Abstraction;
using ParserCore.Attributes;
using ParserCore.Extensions;
using System;
using System.Globalization;

namespace ParserCore
{
    public class AttributeWorkerDecorator<TIn, TOut> : IWorker<TIn, TOut>
        where TIn : IHtmlNodeProvider
        where TOut : new()
    {
        private IWorker<TIn, TOut> worker;

        /// <summary>
        /// TOut model produced by <param name="worker"> will be updated according to <see cref="XPathAttribute"/>
        /// </summary>
        /// <param name="worker">Worker to be decorated</param>
        public AttributeWorkerDecorator(IWorker<TIn, TOut> worker)
        {
            this.worker = worker;
        }

        public bool IsExecutable(TIn model)
            => worker.IsExecutable(model);

        public TOut Parse(TIn model)
        {
            var htmlNode = model.Node;
            var tOut = worker.Parse(model) ?? new TOut();

            foreach(var property in tOut.GetType().GetProperties())
            {
                var xPath = property.GetCustomAttributeData<XPathAttribute>()
                                    ?.GetConstructorArgument<string>();
                if(xPath != null)
                {
                    var value = htmlNode.SelectSingleNode(xPath)?.InnerText;
                    var convertedValue = ConvertToType(value, property.PropertyType);
                    if(convertedValue != null)
                        property.SetValue(tOut, convertedValue);
                }
            }

            return tOut;
        }

        private object ConvertToType(string value, Type type)
        {
            try
            {
                if (type == typeof(string))
                    return value;

                if (type == typeof(bool))
                    return Convert.ToBoolean(value);

                if (type == typeof(int))
                    return Convert.ToInt32(value);

                if (type == typeof(long))
                    return Convert.ToInt64(value);

                if (type == typeof(double))
                    return Convert.ToDouble(value, CultureInfo.InvariantCulture.NumberFormat);

                if (type == typeof(decimal))
                    return Convert.ToDecimal(value, CultureInfo.InvariantCulture.NumberFormat);

                throw new NotImplementedException($"{nameof(XPathAttribute)} is not implemented for type {type.Name}");
            }
            catch (NotImplementedException)
            {
                throw;
            }
            // in case of converting exception do not set any value to property with attribute
            catch (OverflowException)
            {
                return null;
            }
            catch(FormatException)
            {
                return null;
            }
        }
    }
}

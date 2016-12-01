using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Papercut.Service.Services
{
    /// <summary>
    /// This is stripped down version of TestWebServiceHost that has only the minimum features needed to work.
    /// </summary>
    public class BasicWebServiceHost : IDisposable
    {
        private ServiceHost _host;
        private object _serviceObject;
        private Type _serviceInterface;

        public BasicWebServiceHost(object serviceObject, Type serviceInterface)
        {
            if (serviceInterface == null)
            {
                throw new ArgumentNullException("serviceInterface");
            }

            if (serviceObject == null)
            {
                throw new ArgumentNullException("serviceObject");
            }

            _serviceObject = serviceObject;
            _serviceInterface = serviceInterface;
        }

        public void Start(string serviceName, string serviceUri)
        {
            if (_host != null)
            {
                throw new InvalidOperationException("host has already been started.");
            }

            var uri = new Uri(serviceUri);
            Binding binding;
            if (uri.Scheme == Uri.UriSchemeHttp)
            {
                binding = new BasicHttpBinding();
            }
            else
            {
                throw new ArgumentException(string.Format("Scheme '{0}' is not supported.", uri.Scheme), "serviceUri");
            }

            _host = new ServiceHost(_serviceObject);
            _host.AddServiceEndpoint(_serviceInterface, binding, uri);

            var smb = new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpGetUrl = uri };
            _host.Description.Behaviors.Add(smb);

            _host.Description.Name = serviceName;

            _host.Open();
        }

        public void Close()
        {
            if (_host == null)
                return;

            if (_host.State == CommunicationState.Opened)
            {
                _host.Close();
            }
        }

        void IDisposable.Dispose()
        {
            Close();
        }
    }
}

using Papercut.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Papercut.Service.Services
{
    [ServiceContract]
    interface IPerfWebService
    {
        [OperationContract]
        void Test();

        [OperationContract]
        void TestWithParams(
            object parameter1,
            object parameter2,
            object parameter3,
            object parameter4,
            object parameter5,
            object parameter6,
            object parameter7,
            object parameter8,
            object parameter9,
            object parameter10,
            object parameter11,
            object parameter12,
            object parameter13,
            object parameter14,
            object parameter15,
            object parameter16,
            object parameter17,
            object parameter18,
            object parameter19,
            object parameter20);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class PerfWebService : IPerfWebService
    {
        private long calls;
        private PerformanceCounter _webserviceCallCountPC;
        private PerformanceCounter _webserviceCallRatePC;
        private ILogger _logger;

        public PerfWebService(ILogger logger)
        {
            this._logger = logger;
            _webserviceCallCountPC = new PerformanceCounter(PapercutPerformanceCounters.PapercutPCC, PapercutPerformanceCounters.PapercutWebserviceCallCountPC, false);
            _webserviceCallCountPC.RawValue = 0;

            _webserviceCallRatePC = new PerformanceCounter(PapercutPerformanceCounters.PapercutPCC, PapercutPerformanceCounters.PapercutWebserviceCallRatePC, false);
            _webserviceCallRatePC.RawValue = 0;
        }

        public void Test()
        {
            IncrementCounters();
        }

        public void TestWithParams(
            object parameter1,
            object parameter2,
            object parameter3,
            object parameter4,
            object parameter5,
            object parameter6,
            object parameter7,
            object parameter8,
            object parameter9,
            object parameter10,
            object parameter11,
            object parameter12,
            object parameter13,
            object parameter14,
            object parameter15,
            object parameter16,
            object parameter17,
            object parameter18,
            object parameter19,
            object parameter20)
        {
            IncrementCounters();
        }

        private void IncrementCounters()
        {
            Interlocked.Increment(ref calls);
            _webserviceCallCountPC.Increment();
            _webserviceCallRatePC.Increment();
        }

        public long Calls
        {
            get
            {
                return Interlocked.Read(ref calls);
            }
        }
    }
}

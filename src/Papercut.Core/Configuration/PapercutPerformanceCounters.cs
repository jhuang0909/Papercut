using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Papercut.Core.Configuration
{
    public class PapercutPerformanceCounters
    {
        public static readonly string PapercutPCC = "Papercut";
        public static readonly string PapercutMessageCountPC = "MessageCount";
        public static readonly string PapercutMessageRatePC = "Received Messages/sec";
        public static readonly string PapercutWebserviceCallCountPC = "WebserviceCallCount";
        public static readonly string PapercutWebserviceCallRatePC = "Received Webservice Calls/sec";

        public static bool SetupCategory()
        {
            if (!PerformanceCounterCategory.Exists(PapercutPCC))
            {

                CounterCreationDataCollection CCDC = new CounterCreationDataCollection();

                // Add the counter.
                CounterCreationData MsgCountNOI32 = new CounterCreationData();
                MsgCountNOI32.CounterType = PerformanceCounterType.NumberOfItems32;
                MsgCountNOI32.CounterName = PapercutMessageCountPC;
                MsgCountNOI32.CounterHelp = "Number of messages received since Papercut starts.";
                CCDC.Add(MsgCountNOI32);

                CounterCreationData MsgRateROC32 = new CounterCreationData();
                MsgRateROC32.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                MsgRateROC32.CounterName = PapercutMessageRatePC;
                MsgRateROC32.CounterHelp = "Rate of received messages.";
                CCDC.Add(MsgRateROC32);

                CounterCreationData WebserviceCallNOI32 = new CounterCreationData();
                WebserviceCallNOI32.CounterType = PerformanceCounterType.NumberOfItems32;
                WebserviceCallNOI32.CounterName = PapercutWebserviceCallCountPC;
                WebserviceCallNOI32.CounterHelp = "Number of web service calls received since Papercut starts.";
                CCDC.Add(WebserviceCallNOI32);

                CounterCreationData WebServicesCallRateROC32 = new CounterCreationData();
                WebServicesCallRateROC32.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                WebServicesCallRateROC32.CounterName = PapercutWebserviceCallRatePC;
                WebServicesCallRateROC32.CounterHelp = "Rate of received web service calls.";
                CCDC.Add(WebServicesCallRateROC32);

                // Create the category.
                PerformanceCounterCategory.Create(PapercutPCC,
                    "A Performance object for Papercut, a stupidly simple SMTP server for local development.",
                    PerformanceCounterCategoryType.SingleInstance, CCDC);

                return (true);
            }
            else
            {
                return (false);
            }
        }
    }
}

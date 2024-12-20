using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMicroservice.Constants
{
    public class QueueNames
    {
        public const string Stock_Order_Created_Event_Queue_Name = "stock-order-created-event-queue-name";
        public const string Order_Payment_Received_Event_Queue_Name = "order-payment-received-event-queue-name";
        public const string Order_Stock_Canceled_Event_Queue_Name = "order-stock-canceled-event-queue-name";
        public const string Payment_Stock_Accepted_Event_Queue_Name = "payment-stock-accepted-event-queue-name";
        public const string Order_Payment_Canceled_Event_Queue_Name = "order-payment-canceled-event-queue-name";
        public const string Stock_Payment_Canceled_Event_Queue_Name = "stock-payment-canceled-event-queue-name";



    }
}

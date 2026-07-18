using Common;
using UnityEngine;

namespace Core
{
    public class Tester : MonoBehaviour
    {
        public GameEvent<int> testEventWithInt = new();
        public GameEvent testEventWithoutParam = new();

        private SubscriptionList subscriptionList = new();
        private void Start()
        {
            subscriptionList.Add(testEventWithInt.RegisterListener(new(TestMethodParameterlessLastOrder, 1)));
            subscriptionList.Add(testEventWithInt.RegisterListener(new(TestMethod)));
            subscriptionList.Add(testEventWithInt.RegisterListener(new(TestMethod,true)));
            subscriptionList.Add(testEventWithInt.RegisterListener(new(TestMethodParameterless)));
            subscriptionList.Add(testEventWithInt.RegisterListener(new(TestMethodParameterless,true)));
            subscriptionList.Add(testEventWithInt.RegisterListener(new(TestMethodParameterlessFirstOrder,-1)));

            testEventWithInt.Invoke(1);

            Debug.LogWarning("Invoked 1");

            testEventWithInt.Invoke(2);

            Debug.LogWarning("Invoked 2");

            subscriptionList.Add(testEventWithoutParam.RegisterListener(new(TestMethodParameterless)));
            subscriptionList.Add(testEventWithoutParam.RegisterListener(new(TestMethodParameterless, true)));

            testEventWithoutParam.Invoke();

            Debug.LogWarning("Invoked");

            testEventWithoutParam.Invoke();

            Debug.LogWarning("Invoked");

            subscriptionList.UnsubscribeAll();
            Debug.LogWarning("Unsub");

            testEventWithInt.Invoke(1);
            testEventWithoutParam.Invoke();
        }

        private void TestMethod(int testValue)
        {
            Debug.Log($"TestMethod called with: {testValue}");
        }

        private void TestMethodParameterless()
        {
            Debug.Log($"TestMethodParameterless called");
        }

        private void TestMethodParameterlessFirstOrder()
        {
            Debug.Log($"Fist called");
        }

        private void TestMethodParameterlessLastOrder()
        {
            Debug.Log($"Last called");
        }
    }
}

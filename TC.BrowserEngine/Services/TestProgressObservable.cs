using System;
using System.Collections.Generic;
using TC.BrowserEngine.Signal;
using TC.Common.Selenium;

namespace TC.BrowserEngine.Services
{

    public struct TestProgress
    {
        public string senderConnectionId;
        public SeleniumCommand command;
        public bool IsSuccesfull;
        public string Message;
    }

    public class TestProgressEmitter : IObservable<TestProgress>
    {
        private List<IObserver<TestProgress>> observers;

        public TestProgressEmitter()
        {
            observers = new List<IObserver<TestProgress>>();
        }
        public IDisposable Subscribe(IObserver<TestProgress> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<TestProgress>> _observers;
            private IObserver<TestProgress> _observer;

            public Unsubscriber(List<IObserver<TestProgress>> observers, IObserver<TestProgress> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }            

        internal void CommandComplete(TestProgress? testProgress)
        {
            foreach(var observer in TestProgressSubscriber.Get())
            {
                if (!observers.Contains(observer.Value))
                {
                    Subscribe(observer.Value);
                }
            }


            foreach (var observer in observers)
            {
                if (testProgress == null)
                    observer.OnError(new Exception("Command is empty"));
                else
                    observer.OnNext(testProgress.Value);
            }
        }
    }

    public class TestProgressSubscriber : IObserver<TestProgress>
    {
        static private Dictionary<Guid, IObserver<TestProgress>> _observers = new Dictionary<Guid, IObserver<TestProgress>>();
        private SendTestProgressDelegate _sendTestProgressDelegate;

        public TestProgressSubscriber( SendTestProgressDelegate sendTestProgressDelegate)
        {
            _sendTestProgressDelegate = sendTestProgressDelegate;
        }

        static public void Set(Guid id, IObserver<TestProgress> observer)
        {
            if (!_observers.ContainsKey(id))
            {
                _observers.Add(id, observer);
            }
        }

        static public Dictionary<Guid, IObserver<TestProgress>> Get()
        {
            return _observers;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(TestProgress value)
        {
            _sendTestProgressDelegate(value.senderConnectionId, value.command.Guid);
        }
    }
}

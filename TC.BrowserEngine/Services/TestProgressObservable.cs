using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TC.BrowserEngine.Signal;
using TC.Common.Selenium;

namespace TC.BrowserEngine.Services
{
    public interface ITestProgress
    {
        string senderConnectionId { get; set; }
        int TestRunHistoryId { get; set; }
        SeleniumCommand command { get; set; }
        bool IsSuccesfull { get; set; }
        public string Message { get; set; }
    }
    public class TestProgress : ITestProgress
    {
        public string senderConnectionId { get; set; }
        public SeleniumCommand command { get; set; }
        public bool IsSuccesfull { get; set; }
        public string Message { get; set; }
        public int TestRunHistoryId { get; set; }
    }
    public class ScreenshotTestProgress : ITestProgress
    {
        public string senderConnectionId { get; set; }
        public SeleniumCommand command { get; set; }
        public bool IsSuccesfull { get; set; }
        public Screenshot Screenshot { get; set; }
        public string Message { get; set; }
        public int TestRunHistoryId { get; set; }
    }
    public interface ITestProgressEmitter
    {

    }

    public class TestProgressEmitter : IObservable<ITestProgress>, ITestProgressEmitter
    {
        private List<IObserver<ITestProgress>> observers;

        public TestProgressEmitter()
        {
            observers = new List<IObserver<ITestProgress>>();
        }
        public IDisposable Subscribe(IObserver<ITestProgress> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<ITestProgress>> _observers;
            private IObserver<ITestProgress> _observer;

            public Unsubscriber(List<IObserver<ITestProgress>> observers, IObserver<ITestProgress> observer)
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

        internal void CommandComplete(ITestProgress testProgress)
        {
            SubscriberValue();

            foreach (var observer in observers)
            {
                if (testProgress == null)
                    observer.OnError(new Exception("Command is empty"));
                else
                    observer.OnNext(testProgress);
            }
        }
        internal void ScreenshotComplete(ITestProgress screenshotTestProgress)
        {
            SubscriberValue();

            foreach (var observer in observers)
            {
                if (screenshotTestProgress == null)
                    observer.OnError(new Exception("Command is empty"));
                else
                    observer.OnNext(screenshotTestProgress);
            }
        }
        private void SubscriberValue()
        {
            foreach (var observer in TestProgressSubscriber.Get())
            {
                if (!observers.Contains(observer.Value))
                {
                    Subscribe(observer.Value);
                }
            }
        }
    }

    public class TestProgressSubscriber : IObserver<ITestProgress>
    {
        static private Dictionary<Guid, IObserver<ITestProgress>> _observers = new Dictionary<Guid, IObserver<ITestProgress>>();
        private SendTestProgressDelegate _sendTestProgressDelegate;
        private SendTestProgressImageDelegate _sendTestProgressImageDelegate;

        public TestProgressSubscriber(SendTestProgressDelegate sendTestProgressDelegate, SendTestProgressImageDelegate sendTestProgressImageDelegate)
        {
            _sendTestProgressDelegate = sendTestProgressDelegate;
            _sendTestProgressImageDelegate = sendTestProgressImageDelegate;
        }

        static public void Set(Guid id, IObserver<ITestProgress> observer)
        {
            if (!_observers.ContainsKey(id))
            {
                _observers.Add(id, observer);
            }
        }

        static public Dictionary<Guid, IObserver<ITestProgress>> Get()
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

        public void OnNext(ITestProgress value)
        {
            if(value is TestProgress)
            {
                _sendTestProgressDelegate(value);
                return; 
            }
            else if(value is ScreenshotTestProgress)
            {
                ScreenshotTestProgress screenshot= value as ScreenshotTestProgress;
                _sendTestProgressImageDelegate(screenshot);
                return;
            }
           
        }
    }
}

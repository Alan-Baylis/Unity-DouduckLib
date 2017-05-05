﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DouduckGame {
    public class SubscribableSubject<T> where T : struct {

        protected List<SubscribableObserver<T>> m_Observers = new List<SubscribableObserver<T>> ();
        private Dictionary<int, Action<int, T>> m_Callbacks = new Dictionary<int, Action<int, T>> ();

        public bool CheckObserver (int iEventId) {
            return m_Callbacks.ContainsKey (iEventId);
        }

        public virtual SubscribableObserver<T> RegisterObserver (int iEventId, Action<int, T> observerCallback) {
            AddCallback (iEventId, observerCallback);
            SubscribableObserver<T> newObserver_ = new SubscribableObserver<T> (iEventId, observerCallback);
            m_Observers.Add (newObserver_);
            return newObserver_;
        }

        public virtual void UnregisterObserver (int iEventId, Action<int, T> observerCallback) {
            RemoveCallback (iEventId, observerCallback);
            for (int i = m_Observers.Count - 1; i >= 0; i--) {
                if (m_Observers[i].EventId == iEventId && m_Observers[i].Callback == observerCallback) {
                    m_Observers.RemoveAt (i);
                    break; // only remove one observer
                }
            }
        }

        public virtual void Notify (int iEventId, T args) {
            if (m_Callbacks.ContainsKey (iEventId)) {
                m_Callbacks[iEventId] (iEventId, args);
            }
        }

        public virtual void KillObserverByEventId (int iEventId) {
            if (m_Callbacks.ContainsKey (iEventId)) {
                m_Callbacks.Remove (iEventId);
            }
        }

        public virtual void KillObserverById (int iId) {
            for (int i = m_Observers.Count - 1; i >= 0; i--) {
                if (m_Observers[i].Id == iId) {
                    RemoveCallback (m_Observers[i].EventId, m_Observers[i].Callback);
                    m_Observers.RemoveAt (i);
                }
            }
        }

        public virtual void KillObserverByTarget (object target) {
            for (int i = m_Observers.Count - 1; i >= 0; i--) {
                if (m_Observers[i].Callback.Target == target) {
                    RemoveCallback (m_Observers[i].EventId, m_Observers[i].Callback);
                    m_Observers.RemoveAt (i);
                }
            }
        }

        public virtual void KillAllObserver () {
            m_Callbacks.Clear ();
        }

        protected void AddCallback (int iEventId, Action<int, T> observerCallback) {
            if (m_Callbacks.ContainsKey (iEventId)) {
                m_Callbacks[iEventId] += observerCallback;
            } else {
                m_Callbacks.Add (iEventId, observerCallback);
            }
        }

        protected void RemoveCallback (int iEventId, Action<int, T> observerCallback) {
            if (m_Callbacks.ContainsKey (iEventId)) {
                m_Callbacks[iEventId] -= observerCallback;
                if (m_Callbacks[iEventId] == null) {
                    m_Callbacks.Remove (iEventId);
                }
            }
        }
    }
}
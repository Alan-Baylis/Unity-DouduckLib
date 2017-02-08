﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DouduckGame {
    public class SimpleObserver {

        private int m_iEventID;
        public int EventId {
            get {
                return m_iEventID;
            }
        }

        private int m_iID;
        public int Id {
            get {
                return m_iID;
            }
        }

        private Action<int> m_dCallback;
        public Action<int> Callback {
            get {
                return m_dCallback;
            }
        }

        public SimpleObserver (int iEventID, Action<int> callback) {
            m_iEventID = iEventID;
            m_dCallback = callback;
        }

        public SimpleObserver SetId (int id) {
            m_iID = id;
            return this;
        }

    }
}

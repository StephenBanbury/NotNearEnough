using UnityEngine;
using Normal.Realtime.Serialization;

namespace Assets.Scripts
{
    [RealtimeModel]
    public partial class DisplaySelectSyncModel
    {
        [RealtimeProperty(5, true, true)] private int _displayId;
    }

/* ----- Begin Normal Autogenerated Code ----- */
    public partial class DisplaySelectSyncModel : IModel
    {
        // Properties
        public int displayId
        {
            get { return _cache.LookForValueInCache(_displayId, entry => entry.displayIdSet, entry => entry.displayId); }
            set
            {
                if (value == displayId) return;
                _cache.UpdateLocalCache(entry =>
                {
                    entry.displayIdSet = true;
                    entry.displayId = value;
                    return entry;
                });
                FireDisplayIdDidChange(value);
            }
        }

        // Events
        public delegate void DisplayIdDidChange(DisplaySelectSyncModel model, int value);

        public event DisplayIdDidChange displayIdDidChange;

        // Delta updates
        private struct LocalCacheEntry
        {
            public bool displayIdSet;
            public int displayId;
        }

        private LocalChangeCache<LocalCacheEntry> _cache;

        public DisplaySelectSyncModel()
        {
            _cache = new LocalChangeCache<LocalCacheEntry>();
        }

        // Events
        public void FireDisplayIdDidChange(int value)
        {
            try
            {
                if (displayIdDidChange != null)
                    displayIdDidChange(this, value);
            }
            catch (System.Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        // Serialization
        enum PropertyID
        {
            DisplayId = 1,
        }

        public int WriteLength(StreamContext context)
        {
            int length = 0;

            if (context.fullModel)
            {
                // Mark unreliable properties as clean and flatten the in-flight cache.
                // TODO: Move this out of WriteLength() once we have a prepareToWrite method.
                _displayId = displayId;
                _cache.Clear();

                // Write all properties
                length += WriteStream.WriteVarint32Length((uint) PropertyID.DisplayId, (uint) _displayId);
            }
            else
            {
                // Reliable properties
                if (context.reliableChannel)
                {
                    LocalCacheEntry entry = _cache.localCache;
                    if (entry.displayIdSet)
                        length += WriteStream.WriteVarint32Length((uint) PropertyID.DisplayId, (uint) entry.displayId);
                }
            }

            return length;
        }

        public void Write(WriteStream stream, StreamContext context)
        {
            if (context.fullModel)
            {
                // Write all properties
                stream.WriteVarint32((uint) PropertyID.DisplayId, (uint) _displayId);
            }
            else
            {
                // Reliable properties
                if (context.reliableChannel)
                {
                    LocalCacheEntry entry = _cache.localCache;
                    if (entry.displayIdSet)
                        _cache.PushLocalCacheToInflight(context.updateID);

                    if (entry.displayIdSet)
                        stream.WriteVarint32((uint) PropertyID.DisplayId, (uint) entry.displayId);
                }
            }
        }

        public void Read(ReadStream stream, StreamContext context)
        {
            bool displayIdExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.displayIdSet);

            // Remove from in-flight
            if (context.deltaUpdatesOnly && context.reliableChannel)
                _cache.RemoveUpdateFromInflight(context.updateID);

            // Loop through each property and deserialize
            uint propertyID;
            while (stream.ReadNextPropertyID(out propertyID))
            {
                switch (propertyID)
                {
                    case (uint) PropertyID.DisplayId:
                    {
                        int previousValue = _displayId;

                        _displayId = (int) stream.ReadVarint32();

                        if (!displayIdExistsInChangeCache && _displayId != previousValue)
                            FireDisplayIdDidChange(_displayId);
                        break;
                    }
                    default:
                        stream.SkipProperty();
                        break;
                }
            }
        }
    }

/* ----- End Normal Autogenerated Code ----- */
}
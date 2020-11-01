using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class SceneSelectSyncModel
{
    [RealtimeProperty(1, true, true)] private Scene _scene;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class SceneSelectSyncModel : IModel {
    // Properties
    public Assets.Scripts.Enums.Scene scene {
        get { return _cache.LookForValueInCache(_scene, entry => entry.sceneSet, entry => entry.scene); }
        set { if (value == scene) return; _cache.UpdateLocalCache(entry => { entry.sceneSet = true; entry.scene = value; return entry; }); FireSceneDidChange(value); }
    }
    
    // Events
    public delegate void SceneDidChange(SceneSelectSyncModel model, Assets.Scripts.Enums.Scene value);
    public event         SceneDidChange sceneDidChange;
    
    // Delta updates
    private struct LocalCacheEntry {
        public bool                       sceneSet;
        public Assets.Scripts.Enums.Scene scene;
    }
    
    private LocalChangeCache<LocalCacheEntry> _cache;
    
    public SceneSelectSyncModel() {
        _cache = new LocalChangeCache<LocalCacheEntry>();
    }
    
    // Events
    public void FireSceneDidChange(Assets.Scripts.Enums.Scene value) {
        try {
            if (sceneDidChange != null)
                sceneDidChange(this, value);
        } catch (System.Exception exception) {
            Debug.LogException(exception);
        }
    }
    
    // Serialization
    enum PropertyID {
        Scene = 1,
    }
    
    public int WriteLength(StreamContext context) {
        int length = 0;
        
        if (context.fullModel) {
            // Mark unreliable properties as clean and flatten the in-flight cache.
            // TODO: Move this out of WriteLength() once we have a prepareToWrite method.
            _scene = scene;
            _cache.Clear();
            
            // Write all properties
            length += WriteStream.WriteVarint32Length((uint)PropertyID.Scene, (uint)_scene);
        } else {
            // Reliable properties
            if (context.reliableChannel) {
                LocalCacheEntry entry = _cache.localCache;
                if (entry.sceneSet)
                    length += WriteStream.WriteVarint32Length((uint)PropertyID.Scene, (uint)entry.scene);
            }
        }
        
        return length;
    }
    
    public void Write(WriteStream stream, StreamContext context) {
        if (context.fullModel) {
            // Write all properties
            stream.WriteVarint32((uint)PropertyID.Scene, (uint)_scene);
        } else {
            // Reliable properties
            if (context.reliableChannel) {
                LocalCacheEntry entry = _cache.localCache;
                if (entry.sceneSet)
                    _cache.PushLocalCacheToInflight(context.updateID);
                
                if (entry.sceneSet)
                    stream.WriteVarint32((uint)PropertyID.Scene, (uint)entry.scene);
            }
        }
    }
    
    public void Read(ReadStream stream, StreamContext context) {
        bool sceneExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.sceneSet);
        
        // Remove from in-flight
        if (context.deltaUpdatesOnly && context.reliableChannel)
            _cache.RemoveUpdateFromInflight(context.updateID);
        
        // Loop through each property and deserialize
        uint propertyID;
        while (stream.ReadNextPropertyID(out propertyID)) {
            switch (propertyID) {
                case (uint)PropertyID.Scene: {
                    Assets.Scripts.Enums.Scene previousValue = _scene;
                    
                    _scene = (Assets.Scripts.Enums.Scene)stream.ReadVarint32();
                    
                    if (!sceneExistsInChangeCache && _scene != previousValue)
                        FireSceneDidChange(_scene);
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
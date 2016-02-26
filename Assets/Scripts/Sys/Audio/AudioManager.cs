using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Sys.Audio
{
    public class AudioManager
        : MonoBehaviour
    {
        private class Source
        {
            public AudioSource System { get; set; }
            public bool CanUse { get; set; }
            public string Tag { get; set; }
            public int Priority { get; set; }

            public void Stop()
            {
                CanUse = true;
                System.Stop();
            }

            public void Play( string tag, AudioClip clip, int priority, float pan, float pitch )
            {
                Tag = tag;
                Priority = priority;
                CanUse = false;
                System.clip = clip;
                System.panStereo = pan;
                System.pitch = pitch;
                System.Stop();
                System.Play();
            }
        }

        [SerializeField]
        private List<AudioClip> clips_;

        private List<Source> sounds_;

        private int index_;

        void Start()
        {
            index_ = 0;
            sounds_ = new List<Source>();
            for ( int i = 0; i < 16; ++i ) {
                sounds_.Add( new Source() {
                    System = gameObject.AddComponent<AudioSource>(),
                    CanUse = true,
                    Tag = ""
                } );
            }
        }

        void Update()
        {
            foreach ( var o in sounds_ ) {
                if ( !o.CanUse && !o.System.isPlaying ) {
                    o.System.Stop();
                }
            }
        }

        public void SetVolume( float volume )
        {
            volume = Mathf.Clamp01( volume );
            foreach ( var v in sounds_ ) {
                v.System.volume = volume;
            }
        }

        public void Stop()
        {
            foreach ( var v in sounds_ ) {
                v.Stop();
            }
        }

        public void Stop( string tag )
        {
            (from x in sounds_ where x.Tag == tag select x).All( ( x ) => { x.Stop(); return true; } );
        }

        public void Play( string tag, string file, float pan = 0, float pitch = 1 )
        {
            var clip = Find( file );

            if ( index_ > 0x0fffffff ) {
                SlideIndex( 0x0fffffff );
            }

            if ( clip ) {
                int min = int.MaxValue;
                int minIndex = 0;
                for ( int i = 0; i < sounds_.Count; ++i ) {
                    if ( sounds_[i].CanUse ) {
                        sounds_[i].Play( tag, clip, ++index_, pan, pitch );
                        return;
                    }
                    if ( sounds_[i].Priority < min ) {
                        min = sounds_[i].Priority;
                        minIndex = i;
                    }
                }
                sounds_[minIndex].Play( tag, clip, ++index_, pan, pitch );
            }
        }

        private void SlideIndex( int i )
        {
            (from x in sounds_ where x.Tag == tag select x).All( ( x ) => { x.Priority -= i; return true; } );
        }

        private AudioClip Find( string file )
        {
            return (from x in clips_ where x.name == file select x).DefaultIfEmpty().Single();
        }
    }
}
using UnityEngine;
using System.Collections;

namespace Scenes.Sheep
{

    public interface IAdder
    {
        void Add();
    }

    public class AddInterval
    {
        private float begin_;

        protected AddInterval()
        {
            begin_ = Time.realtimeSinceStartup;
        }

        protected bool Interval
        {
            get
            {
                float now = Time.realtimeSinceStartup;
                bool result = (now - begin_) > 1.5;

                if ( result ) {
                    begin_ = now;
                }

                return result;
            }
        }
    }

    public class SheepAdder
        : AddInterval, IAdder
    {
        private Sheep system_;

        public SheepAdder( Sheep system )
        {
            system_ = system;
        }

        public void Add()
        {
            if( Interval ) {
                var o = Object.Instantiate( Random.Range( 0, 100 ) < 90 ? system_.Ref.SheepPrefab : system_.Ref.ZheepPrefab );
                var isLeft = system_.Add( o );
                system_.PlaySe( isLeft );
            }
        }
    }

    public class RabbitAdder
        : AddInterval, IAdder
    {
        private Sheep system_;

        public RabbitAdder( Sheep system )
        {
            system_ = system;
        }

        public void Add()
        {
            if( Interval ) {
                var o = Object.Instantiate( system_.Ref.RabbitPrefab );
                system_.Add( o );
            }
        }
    }
}
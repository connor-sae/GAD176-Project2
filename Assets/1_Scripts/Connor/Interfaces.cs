using GAD176.Connor;
using UnityEngine;

namespace GAD176.Connor
{

    public interface IAlertable
    {
        public void Alert(Vector3 alertOrigin);
    }

    public interface IKillable
    {
        public void Kill();
    }

    public interface IRagdoll : IKillable
    {
        public void Ragdoll(Vector3 origin, float force);
    }
}

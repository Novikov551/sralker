using System;
using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public float moveSpeed = 5f;
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
        }

        private void Start()
        {
            if(IsOwner)
            {
                Camera.main.GetComponent<CameraFollow>().player = gameObject.transform;
            }
        }

        public void MovePlayer(float fixedDeltaTime = 0)
        {
            transform.position = Position.Value;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            SubmitPositionRequestRpc(horizontal, vertical, fixedDeltaTime);
            /*// Optional: Rotate the player to face the movement direction
            if (movement != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(movement);
                Position.Value = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);
            }*/
        }

        public void FixedUpdate()
        {
            if (!IsOwner) return;

            MovePlayer(Time.fixedDeltaTime);
        }

        [Rpc(SendTo.Server)]
        void SubmitPositionRequestRpc(float horizontal, float vertical, float fixedDeltaTime, RpcParams rpcParams = default)
        {
            transform.position += new Vector3(horizontal, vertical) * moveSpeed * fixedDeltaTime; 
            Position.Value = transform.position;
        }
    }
}
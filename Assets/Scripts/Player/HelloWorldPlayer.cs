using Unity.Netcode;
using UnityEngine;


namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public int Health;

        public GameObject Bullet;
        public Transform ShotPoint;

        public float startTimeBtwShots;
        public float offset;
        private float timeBtwShots;

        public float moveSpeed = 5f;
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>();
        public override void OnNetworkSpawn()
        {
        }

        public void TakeDamage(int damage)
        {
            ChangeHealth(-damage);
        }

        public void Shoot(float fixedDeltaTime)
        {
            if (timeBtwShots <= 0)
            {
                if(Input.GetMouseButton(0))
                {

                    Instantiate(Bullet, ShotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                }
            }
            else
            {
                timeBtwShots -= fixedDeltaTime;
            }
        }

        [Rpc(SendTo.Server)]
        public void ShootBulletRpc()
        {
            var instance = Instantiate(Bullet, ShotPoint.position, transform.rotation);
            var instanceNetworkObject = instance.GetComponent<NetworkObject>();
            instanceNetworkObject.Spawn();
        }

        public void ChangeHealth(int healthValue)
        {
            Health += healthValue;
        }

        private void Start()
        {
            if (IsOwner)
            {
                Camera.main.GetComponent<CameraFollow>().player = gameObject.transform;
            }
        }

        public void MovePlayer(float fixedDeltaTime = 0)
        {
            Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;


            transform.rotation = Rotation.Value;
            transform.position = Position.Value;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            SubmitPositionRequestRpc(horizontal, vertical, rotateZ, fixedDeltaTime);
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
            Shoot(Time.fixedDeltaTime);
            MovePlayer(Time.fixedDeltaTime);
        }

        [Rpc(SendTo.Server)]
        void SubmitPositionRequestRpc(float horizontal, float vertical, float rotateZ, float fixedDeltaTime, RpcParams rpcParams = default)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
            transform.position += new Vector3(horizontal, vertical) * moveSpeed * fixedDeltaTime;
            Position.Value = transform.position;
            Rotation.Value = transform.rotation;
        }
    }
}
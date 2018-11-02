using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : NetworkBehaviour
    {
        [SerializeField] public bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
		private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;

		private Rigidbody rb;

		public Animator animator;
		public float FloatOfAnimator;

		public GameObject mitralleuse;

		public bool IsStatic;

		private NetworkSpawner netSpawn;
		private Transform gun;
		private Transform FAP;
		private Transform Piou;
		private Transform bazouka;
		private Transform couteau;
		private Transform Piou2;
		
        private void Start()
        {
			m_Camera = GetComponentInChildren<Camera>();
			Transform Chest;
			Transform LeftHand;
			Transform RightHand;
			if (!isLocalPlayer)
			{
				GetComponentInChildren<AudioListener>().enabled = false;
				m_Camera.enabled = false;
				transform.Find("GUI").gameObject.SetActive(false);
				/*
				Chest = transform.Find("carotteAvecAnimation").Find("Armature").Find("RootChest").Find("Chest");
				LeftHand = Chest.Find("Upper_Arm.L").Find("Lower_Arm.L").Find("Hand.L");
				RightHand = Chest.Find("Upper_Arm.R").Find("Lower_Arm.R").Find("Hand.R");
				
				gun = LeftHand.Find("Hand.L_end").Find("Gun");
				gun.localPosition = new Vector3(-0.004120004f, -0.01260581f, 0.0003499999f);
				gun.localRotation = new Quaternion(0.004f, -175.188f, 5.039f, 1);
				
				FAP = LeftHand.Find("Fusil_A_Pompe (2)");
				FAP.localPosition = new Vector3(-0.00109f, 0.00067f, 0.00196f);
				FAP.localRotation = new Quaternion(-1.991f, -91.94601f, 2.411f, 1);
				
				Piou = LeftHand.Find("Pistolet mitralleur");
				Piou.localPosition = new Vector3(-0.00112f, -0.00565f, 0.00155f);
				Piou.localRotation = new Quaternion(-5.129f, -85.29601f, 0.003f, 1);
				
				bazouka = LeftHand.Find("bazouka");
				bazouka.localPosition = new Vector3(-0.00035f, 0.00512f, -0.00538f);
				bazouka.localRotation = new Quaternion(-4.642f, -84.67001f, 0.003f, 1);
				
				couteau = LeftHand.Find("Couteau");
				couteau.localPosition = new Vector3(-0.00071f, -0.00699f, 0.00791f);
				couteau.localRotation = new Quaternion(-5.221f, -85.4f, 0.002f, 1);
				
				Piou2 = RightHand.Find("Pistolet mitralleur (1)");
				Piou2.localPosition = new Vector3(-0.00112f, -0.00565f, 0.00155f);
				Piou2.localRotation = new Quaternion(-5.129f, -85.29601f, 0.003f, 1);
				*/
				return;
			}
			
			if(isLocalPlayer){
				gameObject.tag = "localPlayer";
				m_Camera.tag = "localCamera";
				transform.Find("GUI").Find("Score").tag = "localScore";
				
				transform.Find("carotteAvecAnimation").Find("Cylinder").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
				
				Transform FPC = transform.Find("FirstPersonCharacter").Find("Spot");
				
				Chest = transform.Find("carotteAvecAnimation").Find("Armature").Find("RootChest").Find("Chest");
				LeftHand = Chest.Find("Upper_Arm.L").Find("Lower_Arm.L").Find("Hand.L");
				RightHand = Chest.Find("Upper_Arm.R").Find("Lower_Arm.R").Find("Hand.R");
				
				Quaternion o = new Quaternion(0, 180, 0, 1);
				Quaternion o2 = new Quaternion(0, -1, 0, 1);
				
				
				gun = LeftHand.Find("Hand.L_end").Find("Gun");
				gun.parent = FPC;
				gun.localPosition = new Vector3(0.0f,0.05f,0.0f);
				gun.localRotation = o;
				
				FAP = LeftHand.Find("Fusil_A_Pompe (2)");
				FAP.parent = FPC;
				FAP.localPosition = new Vector3(0.25f,0.25f,0.59f);
				FAP.localRotation = o2;
				
				Piou = LeftHand.Find("Pistolet mitralleur");
				Piou.parent = FPC;
				Piou.localPosition = new Vector3(0.0f,0.2f,0.2f);
				Piou.localRotation = o2;
				
				bazouka = LeftHand.Find("bazouka");
				bazouka.parent = FPC;
				bazouka.localPosition = new Vector3(-0.0572f, 0.7f, -0.15f);
				bazouka.localRotation = o2;
				
				couteau = LeftHand.Find("Couteau");
				couteau.parent = FPC;
				couteau.localPosition = new Vector3(-0.24f, 0.33f, 0.0f);
				couteau.localRotation = o2;
				
				Piou2 = RightHand.Find("Pistolet mitralleur (1)");
				Piou2.parent = FPC;
				Piou2.localPosition = new Vector3(-1.0f, 0.2f, 0.2f);
				Piou2.localRotation = o2;
				
				m_CharacterController = GetComponent<CharacterController>();
				m_OriginalCameraPosition = m_Camera.transform.localPosition;
				m_FovKick.Setup(m_Camera);
				m_HeadBob.Setup(m_Camera, m_StepInterval);
				m_StepCycle = 0f;
				m_NextStep = m_StepCycle/2f;
				m_Jumping = false;
				m_AudioSource = GetComponent<AudioSource>();
				m_MouseLook.Init(transform , m_Camera.transform);

				rb = GetComponent<Rigidbody> ();
				FloatOfAnimator = -2.0f;
				animator.SetFloat("Blend", FloatOfAnimator);
				IsStatic = true;
				
				netSpawn = GetComponent<NetworkSpawner>();
			}
        }


        // Update is called once per frame
        private void Update()
        {	
			//check weapons pos
			/*if(gun.position != Vector3.zero || FAP.position != Vector3.zero || Piou.position != Vector3.zero
			|| bazouka.position != Vector3.zero || couteau.position != Vector3.zero || Piou2.position != Vector3.zero){
				gun.position = Vector3.zero;
				FAP.position = Vector3.zero;
				Piou.position = Vector3.zero;
				bazouka.position = Vector3.zero;
				couteau.position = Vector3.zero;
				Piou2.position = Vector3.zero;
			}*/
			
			if (!isLocalPlayer)
			{
				return;
			}
			
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;

			if (m_Jumping == false) {
				if(IsStatic == true) {
					FloatOfAnimator = -2.0f;
				} else if (m_IsWalking == true) {
					FloatOfAnimator = 0.0f;
				} else if (m_IsWalking == false) {
					FloatOfAnimator = 0.25f;
				}
			}
			else if (m_Jumping == true) {
				FloatOfAnimator = 0.5f;
			}

			if (mitralleuse.activeInHierarchy == true) {
				FloatOfAnimator = FloatOfAnimator + 1.0f;
			}
				
			/*string id = gameObject.GetComponent<NetworkIdentity>().netId.Value.ToString("0");
			print(id + " " + FloatOfAnimator);
			netSpawn.setFloatPlayer(id, FloatOfAnimator);*/
			animator.SetFloat("Blend", FloatOfAnimator);
        }

		//********************FAIT PAR************************//
		 //********************MATTEO************************//
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Escalier +x")
            {
                gameObject.transform.position += new Vector3(0.1f,0,0);
                m_Jumping = false;
            }
            if (other.gameObject.tag == "Escalier -x")
            {
                gameObject.transform.position += new Vector3(-0.1f, 0, 0);
                m_Jumping = false;
            }
            if (other.gameObject.tag == "Escalier +y")
            {
                gameObject.transform.position += new Vector3(0, 0, 0.1f);
                m_Jumping = false;
            }
            if (other.gameObject.tag == "Escalier -y")
            {
                gameObject.transform.position += new Vector3(0, 0, -0.1f);
                m_Jumping = false;
            }
        }

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == "Trampo") {
				m_MoveDir.y = -m_MoveDir.y;
				PlayJumpSound();
				m_Jumping = true;
			}
		}
		//********************FAIT PAR************************//
		//********************MATTEO************************//

        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
			if (!isLocalPlayer)
			{
				return;
			}
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();

			if (desiredMove != Vector3.zero) {
				IsStatic = false;
			} else {
				IsStatic = true;
			}
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }

        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
			if (!isLocalPlayer)
			{
				return;
			}
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
			if(hit == null)
				return;
			
			if(hit.collider == null)
				return;
			
			if(m_CharacterController == null)
				return;
			
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}

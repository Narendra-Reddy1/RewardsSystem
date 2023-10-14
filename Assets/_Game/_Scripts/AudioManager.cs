using UnityEngine;


    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioAsset m_audioAsset;
        private AudioSource m_sfxAudioSource;
        private AudioSource m_bgmAudioSource;
        private void Awake()
        {
            _Init();
        }
        private void OnEnable()
        {
            GlobalEventHandler.AddListener(EventID.REQUEST_TO_PLAY_SFX, Callback_On_SFX_Requested);
            GlobalEventHandler.AddListener(EventID.REQUEST_TO_PLAY_BGM, Callback_On_BGM_Requested);
           
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.REQUEST_TO_PLAY_SFX, Callback_On_SFX_Requested);
            GlobalEventHandler.RemoveListener(EventID.REQUEST_TO_PLAY_BGM, Callback_On_BGM_Requested);
        
        }
        private void _Init()
        {
            m_sfxAudioSource = gameObject.AddComponent<AudioSource>();
            m_bgmAudioSource = gameObject.AddComponent<AudioSource>();
            m_sfxAudioSource.playOnAwake = false;
            m_bgmAudioSource.playOnAwake = false;
            m_bgmAudioSource.volume = 0.75f;
            m_bgmAudioSource.loop = true;
        }

        private void _PlaySFX(AudioID audioID)
        {
            m_sfxAudioSource.PlayOneShot(m_audioAsset.GetAudioClipByID(audioID));
        }
        private void _PlayBGM(AudioID audioID)
        {
            m_bgmAudioSource.clip = m_audioAsset.GetAudioClipByID(audioID);
            m_bgmAudioSource.Play();
        }

        private void Callback_On_SFX_Requested(object args)
        {
            _PlaySFX((AudioID)args);
        }
        private void Callback_On_BGM_Requested(object args)
        {
            _PlayBGM((AudioID)args);

        }
    }